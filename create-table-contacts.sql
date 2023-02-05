USE [Phonebook]
GO

/****** Object: Table [dbo].[Contacts] Script Date: 05.02.2023 20:27:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contacts] (
    [ContactId]   INT           IDENTITY (1, 1) NOT NULL,
    [Lastname]    NVARCHAR (50) NULL,
    [Firstname]   NVARCHAR (50) NULL,
    [Patronymic]  NVARCHAR (50) NULL,
    [Phonenumber] NVARCHAR (50) NOT NULL
);


