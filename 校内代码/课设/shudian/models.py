#encoding:utf-8

from exts import db
from datetime import datetime

class User(db.Model):
    __tablename__='user'
    id=db.Column(db.Integer,primary_key=True,autoincrement=True)
    username=db.Column(db.String(50),nullable=False)
    password=db.Column(db.String(50),nullable=False)

class Managers(db.Model):
    __tablename__='managers'
    id=db.Column(db.Integer,primary_key=True,autoincrement=True)
    username = db.Column(db.String(50), nullable=False)
    name = db.Column(db.String(50), nullable=False)
    password = db.Column(db.String(50), nullable=False)

class BookInformation(db.Model):
    __tablename__='bookinformation'
    id = db.Column(db.Integer, primary_key=True, autoincrement=True)
    name=db.Column(db.String(50),nullable=False)
    information=db.Column(db.String(100),nullable=False)
    writer=db.Column(db.String(50),nullable=False)
    price=db.Column(db.String(50),nullable=False)
    isbn=db.Column(db.String(50),nullable=False)
    press=db.Column(db.String(50),nullable=False)
    date=db.Column(db.String(50),nullable=False)
    classfication=db.Column(db.String(50),nullable=False)
    create_time = db.Column(db.DateTime, default=datetime.now())


class Settlement(db.Model):
    __tablename__='settlement'
    id = db.Column(db.Integer, primary_key=True, autoincrement=True)
    name = db.Column(db.String(50), nullable=False)
   # price = db.Column(db.String(50), nullable=False)

    number=db.Column(db.String(50),nullable=False)

class ShoppingCart(db.Model):
    __tablename__='shoppingcart'
    id = db.Column(db.Integer, primary_key=True, autoincrement=True)
    name = db.Column(db.String(50), nullable=False)
    writer = db.Column(db.String(50), nullable=False)
    press = db.Column(db.String(50), nullable=False)
    price = db.Column(db.String(50), nullable=False)
    date = db.Column(db.String(50), nullable=False)
    create_time = db.Column(db.DateTime, default=datetime.now())
    





