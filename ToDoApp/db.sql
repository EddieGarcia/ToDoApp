-- this is how DB looks like - nHibernate can also create DB from models automagically
CREATE TABLE TODO (
    ID int IDENTITY(1,1) PRIMARY KEY,
    NAME varchar(255) NOT NULL,
    TEXT varchar(255)
);