
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/21/2016 21:19:22
-- Generated from EDMX file: C:\Users\shial\Documents\Visual Studio 2015\Projects\RekompensatyGIT\Rekompensaty.DataAccess\dbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [dbRekompensaty];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserHuntedAnimal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HuntedAnimals] DROP CONSTRAINT [FK_UserHuntedAnimal];
GO
IF OBJECT_ID(N'[dbo].[FK_AnimalTypeHuntedAnimal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HuntedAnimals] DROP CONSTRAINT [FK_AnimalTypeHuntedAnimal];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[HuntedAnimals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HuntedAnimals];
GO
IF OBJECT_ID(N'[dbo].[AnimalTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnimalTypes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'HuntedAnimals'
CREATE TABLE [dbo].[HuntedAnimals] (
    [Id] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [HuntDate] datetime  NOT NULL,
    [Weight] float  NOT NULL,
    [PricePerKilo] decimal(18,0)  NOT NULL,
    [AnimalTypeId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'AnimalTypes'
CREATE TABLE [dbo].[AnimalTypes] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HuntedAnimals'
ALTER TABLE [dbo].[HuntedAnimals]
ADD CONSTRAINT [PK_HuntedAnimals]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AnimalTypes'
ALTER TABLE [dbo].[AnimalTypes]
ADD CONSTRAINT [PK_AnimalTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'HuntedAnimals'
ALTER TABLE [dbo].[HuntedAnimals]
ADD CONSTRAINT [FK_UserHuntedAnimal]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserHuntedAnimal'
CREATE INDEX [IX_FK_UserHuntedAnimal]
ON [dbo].[HuntedAnimals]
    ([UserId]);
GO

-- Creating foreign key on [AnimalTypeId] in table 'HuntedAnimals'
ALTER TABLE [dbo].[HuntedAnimals]
ADD CONSTRAINT [FK_AnimalTypeHuntedAnimal]
    FOREIGN KEY ([AnimalTypeId])
    REFERENCES [dbo].[AnimalTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnimalTypeHuntedAnimal'
CREATE INDEX [IX_FK_AnimalTypeHuntedAnimal]
ON [dbo].[HuntedAnimals]
    ([AnimalTypeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------