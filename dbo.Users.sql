USE [repositoryDb]
GO

/****** Объект: Table [dbo].[Users] Дата скрипта: 18.03.2019 0:36:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Login]    NVARCHAR (255) NULL,
    [Password] NVARCHAR (255) NULL
);


