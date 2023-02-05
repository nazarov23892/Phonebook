USE [Phonebook]
GO

/****** Object: Table [dbo].[Tags] Script Date: 05.02.2023 20:28:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tags] (
    [TagId] INT           IDENTITY (1, 1) NOT NULL,
    [Tag]   NVARCHAR (50) NOT NULL
);


