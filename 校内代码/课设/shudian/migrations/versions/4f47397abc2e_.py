"""empty message

Revision ID: 4f47397abc2e
Revises: d8d8e1e83239
Create Date: 2018-01-10 15:59:29.983000

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = '4f47397abc2e'
down_revision = 'd8d8e1e83239'
branch_labels = None
depends_on = None


def upgrade():
    # ### commands auto generated by Alembic - please adjust! ###
    op.add_column('shoppingcart', sa.Column('create_time', sa.DateTime(), nullable=True))
    # ### end Alembic commands ###


def downgrade():
    # ### commands auto generated by Alembic - please adjust! ###
    op.drop_column('shoppingcart', 'create_time')
    # ### end Alembic commands ###
