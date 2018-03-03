#encoding:utf-8

import os


SECRET_KEY=os.urandom(24)


HOST='localhost'
PORT='3306'
DATABASE='shudian'
USERNAME='root'
PASSWORD='root'
SQLALCHEMY_DATABASE_URI="mysql+mysqldb://{}:{}@{}:{}/{}".format(USERNAME,PASSWORD,HOST,PORT,DATABASE)

SQLALCHEMY_TRACK_MODIFICATIONS=False
