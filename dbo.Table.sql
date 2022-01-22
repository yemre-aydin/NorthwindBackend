CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY ,  
    [FirstName] VARCHAR(50) NULL, 
    [Email] VARCHAR(50) NULL, 
    [Status] BIT NULL, 
    [PasswordSalt] BINARY(500) NULL, 
    [PasswordHash] VARBINARY(500) NULL, 
    [LastName] VARCHAR(50) NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC)
);
