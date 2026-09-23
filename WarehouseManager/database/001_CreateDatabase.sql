IF DB_ID('WarehouseManagerDb') IS NULL
BEGIN
    CREATE DATABASE WarehouseManagerDb;
END
GO


USE WarehouseManagerDb;
GO


IF OBJECT_ID('dbo.Materials', 'U') IS NULL
BEGIN

    CREATE TABLE dbo.Materials
    (
        Id BIGINT IDENTITY(1,1)
            NOT NULL
            CONSTRAINT PK_Materials PRIMARY KEY,

        Code NVARCHAR(50)
            NOT NULL,

        Name NVARCHAR(200)
            NOT NULL,

        Unit NVARCHAR(20)
            NOT NULL,

        IsActive BIT
            NOT NULL
            CONSTRAINT DF_Materials_IsActive
            DEFAULT 1,

        CreatedAt DATETIME2(3)
            NOT NULL
            CONSTRAINT DF_Materials_CreatedAt
            DEFAULT SYSDATETIME(),

        UpdatedAt DATETIME2(3)
            NULL,

        CONSTRAINT UQ_Materials_Code
            UNIQUE(Code)
    );
END
GO