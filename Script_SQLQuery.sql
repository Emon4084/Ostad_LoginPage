use master 
create database Ostad

use Ostad 
create table Users(
LoginID nvarchar(30) UNIQUE,
LoginPassword nvarchar(40)
)

INSERT INTO Users (LoginID, LoginPassword)
VALUES ('12345', '12345')


