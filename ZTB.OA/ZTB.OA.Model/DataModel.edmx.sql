
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/25/2016 10:45:47
-- Generated from EDMX file: E:\测试\OA\ZTB.OA\ZTB.OA.Model\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [OA];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserInfoRoleInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoRoleInfo] DROP CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoRoleInfo_UserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoRoleInfo] DROP CONSTRAINT [FK_UserInfoRoleInfo_UserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionInfoRoleInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActionInfoRoleInfo] DROP CONSTRAINT [FK_ActionInfoRoleInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionInfoRoleInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActionInfoRoleInfo] DROP CONSTRAINT [FK_ActionInfoRoleInfo_RoleInfo];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[RUserActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RUserActionInfo];
GO
IF OBJECT_ID(N'[dbo].[RoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO
IF OBJECT_ID(N'[dbo].[UserInfoRoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoRoleInfo];
GO
IF OBJECT_ID(N'[dbo].[ActionInfoRoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionInfoRoleInfo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ActionInfo'
CREATE TABLE [dbo].[ActionInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Url] nvarchar(512)  NULL,
    [HttpMethod] nvarchar(32)  NULL,
    [DelFag] bit  NOT NULL,
    [Remark] nvarchar(500)  NULL,
    [ActionName] nvarchar(50)  NULL,
    [IsMenu] bit  NOT NULL,
    [ModifyOn] datetime  NULL
);
GO

-- Creating table 'RUserActionInfo'
CREATE TABLE [dbo].[RUserActionInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HasPermission] bit  NOT NULL,
    [DelFag] bit  NULL,
    [ModifyOn] datetime  NULL,
    [UserInfoId] int  NOT NULL,
    [ActionInfoId] int  NOT NULL
);
GO

-- Creating table 'RoleInfo'
CREATE TABLE [dbo].[RoleInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DelFag] bit  NOT NULL,
    [Remark] nvarchar(500)  NULL,
    [RoleName] nvarchar(30)  NULL,
    [ModifyOn] datetime  NULL
);
GO

-- Creating table 'UserInfo'
CREATE TABLE [dbo].[UserInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(64)  NULL,
    [Pwd] nvarchar(64)  NOT NULL,
    [DelFag] bit  NOT NULL,
    [Remark] nvarchar(500)  NULL,
    [ModifyOn] datetime  NULL
);
GO

-- Creating table 'UserInfoRoleInfo'
CREATE TABLE [dbo].[UserInfoRoleInfo] (
    [RoleInfo_Id] int  NOT NULL,
    [UserInfo_Id] int  NOT NULL
);
GO

-- Creating table 'ActionInfoRoleInfo'
CREATE TABLE [dbo].[ActionInfoRoleInfo] (
    [ActionInfo_Id] int  NOT NULL,
    [RoleInfo_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ActionInfo'
ALTER TABLE [dbo].[ActionInfo]
ADD CONSTRAINT [PK_ActionInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RUserActionInfo'
ALTER TABLE [dbo].[RUserActionInfo]
ADD CONSTRAINT [PK_RUserActionInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoleInfo'
ALTER TABLE [dbo].[RoleInfo]
ADD CONSTRAINT [PK_RoleInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [PK_UserInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [RoleInfo_Id], [UserInfo_Id] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [PK_UserInfoRoleInfo]
    PRIMARY KEY CLUSTERED ([RoleInfo_Id], [UserInfo_Id] ASC);
GO

-- Creating primary key on [ActionInfo_Id], [RoleInfo_Id] in table 'ActionInfoRoleInfo'
ALTER TABLE [dbo].[ActionInfoRoleInfo]
ADD CONSTRAINT [PK_ActionInfoRoleInfo]
    PRIMARY KEY CLUSTERED ([ActionInfo_Id], [RoleInfo_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [RoleInfo_Id] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo]
    FOREIGN KEY ([RoleInfo_Id])
    REFERENCES [dbo].[RoleInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserInfo_Id] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [FK_UserInfoRoleInfo_UserInfo]
    FOREIGN KEY ([UserInfo_Id])
    REFERENCES [dbo].[UserInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoRoleInfo_UserInfo'
CREATE INDEX [IX_FK_UserInfoRoleInfo_UserInfo]
ON [dbo].[UserInfoRoleInfo]
    ([UserInfo_Id]);
GO

-- Creating foreign key on [ActionInfo_Id] in table 'ActionInfoRoleInfo'
ALTER TABLE [dbo].[ActionInfoRoleInfo]
ADD CONSTRAINT [FK_ActionInfoRoleInfo_ActionInfo]
    FOREIGN KEY ([ActionInfo_Id])
    REFERENCES [dbo].[ActionInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RoleInfo_Id] in table 'ActionInfoRoleInfo'
ALTER TABLE [dbo].[ActionInfoRoleInfo]
ADD CONSTRAINT [FK_ActionInfoRoleInfo_RoleInfo]
    FOREIGN KEY ([RoleInfo_Id])
    REFERENCES [dbo].[RoleInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionInfoRoleInfo_RoleInfo'
CREATE INDEX [IX_FK_ActionInfoRoleInfo_RoleInfo]
ON [dbo].[ActionInfoRoleInfo]
    ([RoleInfo_Id]);
GO

-- Creating foreign key on [UserInfoId] in table 'RUserActionInfo'
ALTER TABLE [dbo].[RUserActionInfo]
ADD CONSTRAINT [FK_UserInfoRUserActionInfo]
    FOREIGN KEY ([UserInfoId])
    REFERENCES [dbo].[UserInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoRUserActionInfo'
CREATE INDEX [IX_FK_UserInfoRUserActionInfo]
ON [dbo].[RUserActionInfo]
    ([UserInfoId]);
GO

-- Creating foreign key on [ActionInfoId] in table 'RUserActionInfo'
ALTER TABLE [dbo].[RUserActionInfo]
ADD CONSTRAINT [FK_ActionInfoRUserActionInfo]
    FOREIGN KEY ([ActionInfoId])
    REFERENCES [dbo].[ActionInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionInfoRUserActionInfo'
CREATE INDEX [IX_FK_ActionInfoRUserActionInfo]
ON [dbo].[RUserActionInfo]
    ([ActionInfoId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------