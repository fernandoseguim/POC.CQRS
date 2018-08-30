using System;
using System.Collections.Generic;
using POC.CQRS.Domain.Entities;

namespace POC.CQRS.Tests.Infra
{
	internal static class Scripts
	{
		public static Dictionary<Type, string> CreateTable => new Dictionary<Type, string>()
		{
			{ typeof(User), @"CREATE TABLE IF NOT EXISTS Users (UserID UNIQUEIDENTIFIER NOT NULL,
							Name NVARCHAR(120) NOT NULL,
							Email VARCHAR(50) NOT NULL UNIQUE,
							PasswordHash VARCHAR(128) NOT NULL,
							Salt VARCHAR(36) NOT NULL,
							PRIMARY KEY (UserID))" },
			{ typeof(Brand), @"CREATE TABLE Brands(BrandID UNIQUEIDENTIFIER NOT NULL,
							Name NVARCHAR(20) NOT NULL UNIQUE,
							PRIMARY KEY (BrandID))" },
		};

		public static Dictionary<Type, string> InsertData => new Dictionary<Type, string>()
		{
			{ typeof(User), @"INSERT INTO Users (UserID, Name, Email, PasswordHash, Salt) 
						VALUES ('563C0799-B5D0-4F42-846B-B76A84F134F6', 
								'User de Teste', 
								'teste@gmail.com', 
								'1A88FEB0C03D17DF329D10AC1D2481C5E53B151A84969EE4C5A3FDB2BDB0C30F566DFD889BC95F9956B46BF001B0966AC6C23448917FBC9405F598D4804D0AB1', 
								'768EBCC6-D559-4C44-BABF-1034F78F3C69')" },
			{ typeof(Brand), @"INSERT INTO Brands (BrandID, Name) VALUES ('4EA766C0-4F7A-4AC8-A137-BEEA289AEE5F', 'Brand Test');" },
		};
	}
}
