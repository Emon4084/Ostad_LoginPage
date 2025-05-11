use master 
create database Ostad

use Ostad 
create table Users(
LoginID nvarchar(30) UNIQUE,
LoginPassword nvarchar(40)
)

INSERT INTO Users (LoginID, LoginPassword)
VALUES ('12345', '12345')


use Ostad 
CREATE TABLE Registration (
    ID INT IDENTITY(100,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    UserName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL, 
    Gender NVARCHAR(10),
    Role NVARCHAR(20),  
    RegistrationDate DATETIME DEFAULT GETDATE()  
);

