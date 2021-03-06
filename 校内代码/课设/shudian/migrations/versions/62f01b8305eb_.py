"""empty message

Revision ID: 62f01b8305eb
Revises: 42ff4ed15a4e
Create Date: 2018-01-10 20:15:12.541000

"""
from alembic import op
import sqlalchemy as sa
from sqlalchemy.dialects import mysql

# revision identifiers, used by Alembic.
revision = '62f01b8305eb'
down_revision = '42ff4ed15a4e'
branch_labels = None
depends_on = None


def upgrade():
    # ### commands auto generated by Alembic - please adjust! ###
    op.drop_column('bookinformation', 'image')
    op.drop_column('settlement', 'price')
    # ### end Alembic commands ###


def downgrade():
    # ### commands auto generated by Alembic - please adjust! ###
    op.add_column('settlement', sa.Column('price', mysql.VARCHAR(length=50), nullable=False))
    op.add_column('bookinformation', sa.Column('image', mysql.VARCHAR(length=200), server_default=sa.text(u"'../images/10.jpg'"), nullable=True))
    # ### end Alembic commands ###
