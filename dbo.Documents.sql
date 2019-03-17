USE [repositoryDb]
GO

/****** Объект: Table [dbo].[Documents] Дата скрипта: 18.03.2019 0:35:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Documents] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [DocName]     NVARCHAR (255) NULL,
    [DocDate]     DATETIME2 (7)  NULL,
    [DocAuthor]   NVARCHAR (255) NULL,
    [DocFileName] NVARCHAR (255) NULL
);


