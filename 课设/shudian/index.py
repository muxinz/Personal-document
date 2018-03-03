#encoding:utf-8

from flask import Flask,render_template,request,redirect,url_for,session
from exts import db
import config
from models import User,Managers,Settlement,BookInformation,ShoppingCart
from sqlalchemy import or_
from decorators import login_required
#import MySQLdb

app=Flask(__name__)
app.config.from_object(config)

db.init_app(app)

with app.app_context():
    db.create_all()

@app.route('/')
def index():
    
    context={
        'information':BookInformation.query.order_by('-create_time').all()
        
        }
    return render_template('index.html',**context)
    
@app.route('/login',methods=['GET','POST'])
def login():
    if request.method=='GET':
        return render_template('login.html')
    else:
        username=request.form.get('username')
        password=request.form.get('password1')
        user=User.query.filter(User.username==username,User.password==password).first()
        manager=Managers.query.filter(Managers.username==username,Managers.password==password).first()
        if user:
            session['user_id']=user.id
            session.permanent=True
            return redirect(url_for('index',user=user))
        if manager:
            session['manager_id']=manager.id
            session.permanent=True
            return redirect(url_for('shopshelves'))
        else:
            return '用户名或密码输入错误'

@app.route('/regist',methods=['GET','POST'])
def regist():
    if request.method=='GET':
        return render_template('regist.html')
    else:
        
        password1=request.form.get('password1')
        password2=request.form.get('password2')
        username=request.form.get('username')

        user=User.query.filter(User.username==username).first()
        if user:
            return '用户名已存在'
        else:
            if password1!=password2:
                return '两次输入的密码不相同'
            else:
                user=User(password=password1,username=username)
                db.session.add(user)
                db.session.commit()
                return redirect(url_for('login'))

@app.route('/classification')
def classification():
    information_jxj = BookInformation.query.filter(BookInformation.classfication == 'jxj').all()
    information_gl = BookInformation.query.filter(BookInformation.classfication == 'gl').all()
    information_yy = BookInformation.query.filter(BookInformation.classfication == 'yy').all()
    information_fl = BookInformation.query.filter(BookInformation.classfication == 'fl').all()
    information_zr = BookInformation.query.filter(BookInformation.classfication == 'zr').all()
    return render_template('classification.html',jxj=information_jxj,gl=information_gl,yy=information_yy,fl=information_fl,zr=information_zr)

@app.route('/myForm')
@login_required
def myForm():
    return render_template('myForm.html')

@app.route('/myInformation')
@login_required
def myInformation():
    return render_template('myInformation.html')

@app.route('/sale')
def sale():
    return render_template('sale.html')

@app.route('/shoppingCart',methods=['GET','POST'])
@login_required
def shoppingCart():
    if request.method == 'GET':
        context={
        'shop':ShoppingCart.query.order_by('-create_time').all()
         }

        return render_template('shoppingCart.html',**context)
    else:
        name = request.form.get('name')
        #price = request.form.get('price')
        number = 1
        settlement = Settlement(name=name,number=number)
        db.session.add(settlement)
        db.session.commit()
        return redirect(url_for('myForm'))
@app.route('/shopQuery')
def shopQuery():
    q=request.args.get('q')
    #conn=MySQLdb.connect(host='localhost',user='root',passwd='root',db='bookinformation')
   # cur=conn.cursor()
    #recount=cur.execute('select * from bookinformation')
    
    return render_template('shopQuery.html',recount=recount)
    

@app.route('/shopshelves',methods=['GET','POST'])
def shopshelves():
    if request.method=='GET':
        return render_template('shopshelves.html')
    else:
        name=request.form.get('name')
        price=request.form.get('price')
        isbn=request.form.get('isbn')
        writer=request.form.get('writer')
        press=request.form.get('press')
        date=request.form.get('date')
        fenlei=request.form.get('fenlei')
        content=request.form.get('content')
        book=BookInformation(name=name,price=price,isbn=isbn,writer=writer,press=press,date=date,classfication=fenlei,information=content)
        db.session.add(book)
        db.session.commit()
        return redirect(url_for('index'))

@app.route('/order')
def order():
    return render_template('order.html')


@app.route('/bookdetails/<book_id>',methods=['GET','POST'])
def bookdetails(book_id):
    book_model=BookInformation.query.filter(BookInformation.id==book_id).first()
    if request.method=='GET':
        return render_template('bookdetails.html',book=book_model)
    else:
        book=book_model
        name=book.name
        writer=book.writer
        press=book.press
        date=book.date
        price=book.price
        book=ShoppingCart(name=name,price=price,date=date,writer=writer,press=press)
        db.session.add(book)
        db.session.commit()
        return redirect(url_for('shoppingCart'))
@app.context_processor
def my_context_processor():
    user_id=session.get('user_id')
    if user_id:
        user=User.query.filter(User.id==user_id).first()
        if user:
            return {'user':user}
    return {}

if __name__=='__main__':
    app.run(debug=True)
