
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/14/2016 17:13:06
-- Generated from EDMX file: E:\测试\OA\ZTB.OA\ZTB.OA.Model\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [OA];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ActionInfo'
CREATE TABLE [dbo].[ActionInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Url] nvarchar(512)  NULL,
    [HttpMethod] nvarchar(32)  NULL,
    [DelFag] nvarchar(max)  NOT NULL,
    [Remark] nvarchar(500)  NULL,
    [ModifiedOn] datetime  NULL,
    [SubTime] datetime  NULL,
    [ActionName] nvarchar(50)  NULL,
    [IsMenu] bit  NULL
);
GO

-- Creating table 'R_UserInfo_ActionInfo'
CREATE TABLE [dbo].[R_UserInfo_ActionInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HasPermission] bit  NOT NULL,
    [UserInfoId] int  NULL,
    [ActionInfoId] int  NOT NULL,
    [DelFag] nvarchar(max)  NULL,
    [ActionInfo_Id] int  NOT NULL,
    [UserInfo_Id] int  NOT NULL
);
GO

-- Creating table 'RoleInfo'
CREATE TABLE [dbo].[RoleInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DelFag] nvarchar(max)  NOT NULL,
    [Remark] nvarchar(500)  NULL,
    [ModifiedOn] datetime  NULL,
    [SubTime] datetime  NULL,
    [RoleName] nvarchar(30)  NULL
);
GO

-- Creating table 'UserInfo'
CREATE TABLE [dbo].[UserInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UName] nvarchar(64)  NULL,
    [Pwd] nvarchar(max)  NOT NULL,
    [DelFag] nvarchar(max)  NOT NULL,
    [ShowName] nvarchar(32)  NULL,
    [Remark] nvarchar(500)  NULL,
    [ModifiedOn] datetime  NULL,
    [SubTime] datetime  NULL
);
GO

-- Creating table 'WP_Temp'
CREATE TABLE [dbo].[WP_Temp] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TepName] nvarchar(32)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [TemForm] nvarchar(max)  NULL,
    [Remark] nvarchar(32)  NULL,
    [DelFag] nvarchar(max)  NOT NULL,
    [SubTime] datetime  NULL,
    [ActivyType] nvarchar(max)  NULL
);
GO

-- Creating table 'WF_Instance'
CREATE TABLE [dbo].[WF_Instance] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InstanceName] nvarchar(128)  NOT NULL,
    [StartBy] int  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [Level] smallint  NOT NULL,
    [Content] nvarchar(max)  NULL,
    [Remark] nvarchar(32)  NULL,
    [DelFag] nvarchar(max)  NOT NULL,
    [WF_InstanceId] uniqueidentifier  NULL,
    [WP_TempId] int  NOT NULL
);
GO

-- Creating table 'FileInfo'
CREATE TABLE [dbo].[FileInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FileName] nvarchar(128)  NOT NULL,
    [FileType] nvarchar(32)  NOT NULL,
    [FilePath] nvarchar(128)  NOT NULL,
    [FileSize] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'WF_Step'
CREATE TABLE [dbo].[WF_Step] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StepName] nvarchar(32)  NOT NULL,
    [ProcessBy] int  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ProcessTime] datetime  NOT NULL,
    [ProcessResult] nvarchar(32)  NULL,
    [ProcessComment] nvarchar(128)  NULL,
    [StepStatus] smallint  NULL,
    [IsStartStep] bit  NOT NULL,
    [IsEndStep] bit  NOT NULL,
    [ParentStepId] int  NULL,
    [WF_InstanceId] int  NOT NULL
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

-- Creating table 'WF_InstanceFileInfo'
CREATE TABLE [dbo].[WF_InstanceFileInfo] (
    [WF_Instance_Id] int  NOT NULL,
    [FileInfo_Id] int  NOT NULL
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

-- Creating primary key on [Id] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [PK_R_UserInfo_ActionInfo]
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

-- Creating primary key on [Id] in table 'WP_Temp'
ALTER TABLE [dbo].[WP_Temp]
ADD CONSTRAINT [PK_WP_Temp]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WF_Instance'
ALTER TABLE [dbo].[WF_Instance]
ADD CONSTRAINT [PK_WF_Instance]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FileInfo'
ALTER TABLE [dbo].[FileInfo]
ADD CONSTRAINT [PK_FileInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WF_Step'
ALTER TABLE [dbo].[WF_Step]
ADD CONSTRAINT [PK_WF_Step]
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

-- Creating primary key on [WF_Instance_Id], [FileInfo_Id] in table 'WF_InstanceFileInfo'
ALTER TABLE [dbo].[WF_InstanceFileInfo]
ADD CONSTRAINT [PK_WF_InstanceFileInfo]
    PRIMARY KEY CLUSTERED ([WF_Instance_Id], [FileInfo_Id] ASC);
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

-- Creating foreign key on [ActionInfo_Id] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [FK_ActionInfoR_UserInfo_ActionInfo]
    FOREIGN KEY ([ActionInfo_Id])
    REFERENCES [dbo].[ActionInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionInfoR_UserInfo_ActionInfo'
CREATE INDEX [IX_FK_ActionInfoR_UserInfo_ActionInfo]
ON [dbo].[R_UserInfo_ActionInfo]
    ([ActionInfo_Id]);
GO

-- Creating foreign key on [UserInfo_Id] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [FK_UserInfoR_UserInfo_ActionInfo]
    FOREIGN KEY ([UserInfo_Id])
    REFERENCES [dbo].[UserInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoR_UserInfo_ActionInfo'
CREATE INDEX [IX_FK_UserInfoR_UserInfo_ActionInfo]
ON [dbo].[R_UserInfo_ActionInfo]
    ([UserInfo_Id]);
GO

-- Creating foreign key on [WF_Instance_Id] in table 'WF_InstanceFileInfo'
ALTER TABLE [dbo].[WF_InstanceFileInfo]
ADD CONSTRAINT [FK_WF_InstanceFileInfo_WF_Instance]
    FOREIGN KEY ([WF_Instance_Id])
    REFERENCES [dbo].[WF_Instance]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FileInfo_Id] in table 'WF_InstanceFileInfo'
ALTER TABLE [dbo].[WF_InstanceFileInfo]
ADD CONSTRAINT [FK_WF_InstanceFileInfo_FileInfo]
    FOREIGN KEY ([FileInfo_Id])
    REFERENCES [dbo].[FileInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WF_InstanceFileInfo_FileInfo'
CREATE INDEX [IX_FK_WF_InstanceFileInfo_FileInfo]
ON [dbo].[WF_InstanceFileInfo]
    ([FileInfo_Id]);
GO

-- Creating foreign key on [WP_TempId] in table 'WF_Instance'
ALTER TABLE [dbo].[WF_Instance]
ADD CONSTRAINT [FK_WP_TempWF_Instance]
    FOREIGN KEY ([WP_TempId])
    REFERENCES [dbo].[WP_Temp]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WP_TempWF_Instance'
CREATE INDEX [IX_FK_WP_TempWF_Instance]
ON [dbo].[WF_Instance]
    ([WP_TempId]);
GO

-- Creating foreign key on [WF_InstanceId] in table 'WF_Step'
ALTER TABLE [dbo].[WF_Step]
ADD CONSTRAINT [FK_WF_InstanceWF_Step]
    FOREIGN KEY ([WF_InstanceId])
    REFERENCES [dbo].[WF_Instance]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WF_InstanceWF_Step'
CREATE INDEX [IX_FK_WF_InstanceWF_Step]
ON [dbo].[WF_Step]
    ([WF_InstanceId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------