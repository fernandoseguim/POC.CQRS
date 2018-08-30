-- docker run -d -p 1433:1433 -e sa_password=POCCQRST3st3C4se -e ACCEPT_EULA=Y microsoft/mssql-server-windows-developer
-- docker run -d -p 1433:1433 -e SA_PASSWORD=POCCQRST3st3C4se -e ACCEPT_EULA=Y microsoft/mssql-server-linux:latest

CREATE DATABASE POCCQRSDatabase;
GO

USE POCCQRSDatabase;
GO

CREATE TABLE Users(
    UserID UNIQUEIDENTIFIER NOT NULL,
	Name NVARCHAR(120) NOT NULL,
    Email VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(128) NOT NULL,
    Salt VARCHAR(36) NOT NULL,
    PRIMARY KEY (UserID)
);
GO

CREATE TABLE Brands(
    BrandID UNIQUEIDENTIFIER NOT NULL,
	Name NVARCHAR(20) NOT NULL UNIQUE,
    PRIMARY KEY (BrandID)
);
GO

CREATE TABLE Assets(
    AssetID UNIQUEIDENTIFIER NOT NULL,
	Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(150) NULL,
    Registry VARCHAR(8) NULL,
    BrandID UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (AssetID),
    FOREIGN KEY (BrandID) REFERENCES Brands(BrandID)
);
GO

INSERT INTO Users (UserID, Name, Email, PasswordHash, Salt) VALUES (NEWID(), 'Fernando Seguim', 'fernando.seguim@gmail.com', '1a88feb0c03d17df329d10ac1d2481c5e53b151a84969ee4c5a3fdb2bdb0c30f566dfd889bc95f9956b46bf001b0966ac6c23448917fbc9405f598d4804d0ab1', '768ebcc6-d559-4c44-babf-1034f78f3c69');
INSERT INTO Brands (BrandID, Name) VALUES (NEWID(), 'Seguim IT');
INSERT INTO Assets (AssetID, Name, BrandID) VALUES (NEWID(), 'Notebook', (select BrandID from Brands WHERE Name = 'Seguim IT'));


