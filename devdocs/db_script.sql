/*
Deployment script for esencialAdmin DB
*/

GO
PRINT N'Creating [dbo].[AspNetRoleClaims]...';


GO
CREATE TABLE [dbo].[AspNetRoleClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    [RoleId]     NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[AspNetRoleClaims].[IX_AspNetRoleClaims_RoleId]...';


GO
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId]
    ON [dbo].[AspNetRoleClaims]([RoleId] ASC);


GO
PRINT N'Creating [dbo].[AspNetRoles]...';


GO
CREATE TABLE [dbo].[AspNetRoles] (
    [Id]               NVARCHAR (450) NOT NULL,
    [ConcurrencyStamp] NVARCHAR (MAX) NULL,
    [Name]             NVARCHAR (256) NULL,
    [NormalizedName]   NVARCHAR (256) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[AspNetRoles].[RoleNameIndex]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([NormalizedName] ASC) WHERE ([NormalizedName] IS NOT NULL);


GO
PRINT N'Creating [dbo].[AspNetUserClaims]...';


GO
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    [UserId]     NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[AspNetUserClaims].[IX_AspNetUserClaims_UserId]...';


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId]
    ON [dbo].[AspNetUserClaims]([UserId] ASC);


GO
PRINT N'Creating [dbo].[AspNetUserLogins]...';


GO
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider]       NVARCHAR (450) NOT NULL,
    [ProviderKey]         NVARCHAR (450) NOT NULL,
    [ProviderDisplayName] NVARCHAR (MAX) NULL,
    [UserId]              NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC)
);


GO
PRINT N'Creating [dbo].[AspNetUserLogins].[IX_AspNetUserLogins_UserId]...';


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId]
    ON [dbo].[AspNetUserLogins]([UserId] ASC);


GO
PRINT N'Creating [dbo].[AspNetUserRoles]...';


GO
CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (450) NOT NULL,
    [RoleId] NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC)
);


GO
PRINT N'Creating [dbo].[AspNetUserRoles].[IX_AspNetUserRoles_RoleId]...';


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId]
    ON [dbo].[AspNetUserRoles]([RoleId] ASC);


GO
PRINT N'Creating [dbo].[AspNetUsers]...';


GO
CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (450)     NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [Email]                NVARCHAR (256)     NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [NormalizedEmail]      NVARCHAR (256)     NULL,
    [NormalizedUserName]   NVARCHAR (256)     NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [UserName]             NVARCHAR (256)     NULL,
    [FirstName]            NVARCHAR (256)     NULL,
    [LastName]             NVARCHAR (256)     NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[AspNetUsers].[EmailIndex]...';


GO
CREATE NONCLUSTERED INDEX [EmailIndex]
    ON [dbo].[AspNetUsers]([NormalizedEmail] ASC);


GO
PRINT N'Creating [dbo].[AspNetUsers].[UserNameIndex]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([NormalizedUserName] ASC) WHERE ([NormalizedUserName] IS NOT NULL);


GO
PRINT N'Creating [dbo].[AspNetUserTokens]...';


GO
CREATE TABLE [dbo].[AspNetUserTokens] (
    [UserId]        NVARCHAR (450) NOT NULL,
    [LoginProvider] NVARCHAR (450) NOT NULL,
    [Name]          NVARCHAR (450) NOT NULL,
    [Value]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [Name] ASC)
);


GO
PRINT N'Creating [dbo].[Customers]...';


GO
CREATE TABLE [dbo].[Customers] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Title]            NVARCHAR (20)  NULL,
    [FirstName]        NVARCHAR (50)  NULL,
    [LastName]         NVARCHAR (50)  NULL,
    [Street]           NVARCHAR (256) NULL,
    [Zip]              NVARCHAR (11)  NULL,
    [City]             NVARCHAR (60)  NULL,
    [Company]          NVARCHAR (256) NULL,
    [Phone]            NVARCHAR (25)  NULL,
    [EMail]            NVARCHAR (256) NULL,
    [PurchasesRemarks] NVARCHAR (MAX) NULL,
    [GeneralRemarks]   NVARCHAR (MAX) NULL,
    [DateCreated]      DATETIME2 (7)  NULL,
    [UserCreated]      NVARCHAR (450) NULL,
    [DateModified]     DATETIME2 (7)  NULL,
    [UserModified]     NVARCHAR (450) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Customers].[idx_customer_email_notnull]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_customer_email_notnull]
    ON [dbo].[Customers]([EMail] ASC) WHERE ([Email] IS NOT NULL);


GO
PRINT N'Creating [dbo].[Files]...';


GO
CREATE TABLE [dbo].[Files] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [FileName]     NVARCHAR (50)   NOT NULL,
    [Path]         NVARCHAR (1024) NOT NULL,
    [OriginalName] NVARCHAR (50)   NOT NULL,
    [ContentType]  NVARCHAR (15)   NOT NULL,
    [DateCreated]  DATETIME2 (7)   NULL,
    [UserCreated]  NVARCHAR (450)  NULL,
    [DateModified] DATETIME2 (7)   NULL,
    [UserModified] NVARCHAR (450)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[PaymentMethods]...';


GO
CREATE TABLE [dbo].[PaymentMethods] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (128) NOT NULL,
    [Fee]  FLOAT (53)     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Periodes]...';


GO
CREATE TABLE [dbo].[Periodes] (
    [Id]                      INT             IDENTITY (1, 1) NOT NULL,
    [fk_SubscriptionId]       INT             NOT NULL,
    [StartDate]               DATETIME2 (7)   NOT NULL,
    [EndDate]                 DATETIME2 (7)   NOT NULL,
    [Payed]                   BIT             NOT NULL,
    [PayedDate]               DATETIME2 (7)   NULL,
    [fk_PayedMethodId]        INT             NULL,
    [PaymentReminderSent]     BIT             NOT NULL,
    [PaymentReminderSentDate] DATETIME2 (7)   NULL,
    [Price]                   DECIMAL (19, 4) NOT NULL,
    [fk_GiftedById]           INT             NULL,
    [DateCreated]             DATETIME2 (7)   NULL,
    [UserCreated]             NVARCHAR (450)  NULL,
    [DateModified]            DATETIME2 (7)   NULL,
    [UserModified]            NVARCHAR (450)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[PeriodesGoodies]...';


GO
CREATE TABLE [dbo].[PeriodesGoodies] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [fk_PeriodesId]    INT            NOT NULL,
    [fk_PlanGoodiesId] INT            NOT NULL,
    [Received]         BIT            NOT NULL,
    [ReceivedAt]       DATETIME2 (7)  NULL,
    [SubPeriodeYear]   INT            NOT NULL,
    [DateCreated]      DATETIME2 (7)  NULL,
    [UserCreated]      NVARCHAR (450) NULL,
    [DateModified]     DATETIME2 (7)  NULL,
    [UserModified]     NVARCHAR (450) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[PlanGoodies]...';


GO
CREATE TABLE [dbo].[PlanGoodies] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (128) NOT NULL,
    [fk_templateLabel] INT            NULL,
    [Bezeichnung]      NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Plans]...';


GO
CREATE TABLE [dbo].[Plans] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (128)  NOT NULL,
    [Price]        DECIMAL (19, 4) NOT NULL,
    [Duration]     INT             NOT NULL,
    [Deadline]     DATE            NOT NULL,
    [fk_GoodyId]   INT             NOT NULL,
    [DateCreated]  DATETIME2 (7)   NULL,
    [UserCreated]  NVARCHAR (450)  NULL,
    [DateModified] DATETIME2 (7)   NULL,
    [UserModified] NVARCHAR (450)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Subscription]...';


GO
CREATE TABLE [dbo].[Subscription] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [fk_CustomerId]         INT            NOT NULL,
    [fk_PlanId]             INT            NOT NULL,
    [PlantNumber]           INT            NULL,
    [fk_SubscriptionStatus] INT            NOT NULL,
    [DateCreated]           DATETIME2 (7)  NULL,
    [UserCreated]           NVARCHAR (450) NULL,
    [DateModified]          DATETIME2 (7)  NULL,
    [UserModified]          NVARCHAR (450) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[SubscriptionPhotos]...';


GO
CREATE TABLE [dbo].[SubscriptionPhotos] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [fk_SubscriptionId] INT            NOT NULL,
    [fk_FileId]         INT            NOT NULL,
    [DateCreated]       DATETIME2 (7)  NULL,
    [UserCreated]       NVARCHAR (450) NULL,
    [DateModified]      DATETIME2 (7)  NULL,
    [UserModified]      NVARCHAR (450) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[SubscriptionStatus]...';


GO
CREATE TABLE [dbo].[SubscriptionStatus] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Label] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Templates]...';


GO
CREATE TABLE [dbo].[Templates] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (80) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating unnamed constraint on [dbo].[Periodes]...';


GO
ALTER TABLE [dbo].[Periodes]
    ADD DEFAULT (getdate()) FOR [StartDate];


GO
PRINT N'Creating unnamed constraint on [dbo].[Periodes]...';


GO
ALTER TABLE [dbo].[Periodes]
    ADD DEFAULT ((0)) FOR [Payed];


GO
PRINT N'Creating unnamed constraint on [dbo].[Periodes]...';


GO
ALTER TABLE [dbo].[Periodes]
    ADD DEFAULT ((0)) FOR [PaymentReminderSent];


GO
PRINT N'Creating unnamed constraint on [dbo].[PeriodesGoodies]...';


GO
ALTER TABLE [dbo].[PeriodesGoodies]
    ADD DEFAULT ((0)) FOR [Received];


GO
PRINT N'Creating [dbo].[FK_AspNetRoleClaims_AspNetRoles_RoleId]...';


GO
ALTER TABLE [dbo].[AspNetRoleClaims] WITH NOCHECK
    ADD CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AspNetUserClaims_AspNetUsers_UserId]...';


GO
ALTER TABLE [dbo].[AspNetUserClaims] WITH NOCHECK
    ADD CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AspNetUserLogins_AspNetUsers_UserId]...';


GO
ALTER TABLE [dbo].[AspNetUserLogins] WITH NOCHECK
    ADD CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AspNetUserRoles_AspNetUsers_UserId]...';


GO
ALTER TABLE [dbo].[AspNetUserRoles] WITH NOCHECK
    ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AspNetUserRoles_AspNetRoles_RoleId]...';


GO
ALTER TABLE [dbo].[AspNetUserRoles] WITH NOCHECK
    ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_AspNetUserTokens_AspNetUsers_UserId]...';


GO
ALTER TABLE [dbo].[AspNetUserTokens] WITH NOCHECK
    ADD CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_PeridoesGiftedBy_Customer_Id]...';


GO
ALTER TABLE [dbo].[Periodes] WITH NOCHECK
    ADD CONSTRAINT [FK_PeridoesGiftedBy_Customer_Id] FOREIGN KEY ([fk_GiftedById]) REFERENCES [dbo].[Customers] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Periodes_PaymentMethods_Id]...';


GO
ALTER TABLE [dbo].[Periodes] WITH NOCHECK
    ADD CONSTRAINT [FK_Periodes_PaymentMethods_Id] FOREIGN KEY ([fk_PayedMethodId]) REFERENCES [dbo].[PaymentMethods] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Periodes_Subscription_Id]...';


GO
ALTER TABLE [dbo].[Periodes] WITH NOCHECK
    ADD CONSTRAINT [FK_Periodes_Subscription_Id] FOREIGN KEY ([fk_SubscriptionId]) REFERENCES [dbo].[Subscription] ([Id]);


GO
PRINT N'Creating [dbo].[FK_PeriodesGoodies_Periodes_Id]...';


GO
ALTER TABLE [dbo].[PeriodesGoodies] WITH NOCHECK
    ADD CONSTRAINT [FK_PeriodesGoodies_Periodes_Id] FOREIGN KEY ([fk_PeriodesId]) REFERENCES [dbo].[Periodes] ([Id]);


GO
PRINT N'Creating [dbo].[FK_PeriodesGoodies_PlanGoodies_Id]...';


GO
ALTER TABLE [dbo].[PeriodesGoodies] WITH NOCHECK
    ADD CONSTRAINT [FK_PeriodesGoodies_PlanGoodies_Id] FOREIGN KEY ([fk_PlanGoodiesId]) REFERENCES [dbo].[PlanGoodies] ([Id]);


GO
PRINT N'Creating [dbo].[FK_PlanGoodies_Templates_Id]...';


GO
ALTER TABLE [dbo].[PlanGoodies] WITH NOCHECK
    ADD CONSTRAINT [FK_PlanGoodies_Templates_Id] FOREIGN KEY ([fk_templateLabel]) REFERENCES [dbo].[Templates] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Plans_PlanGoodies_Id]...';


GO
ALTER TABLE [dbo].[Plans] WITH NOCHECK
    ADD CONSTRAINT [FK_Plans_PlanGoodies_Id] FOREIGN KEY ([fk_GoodyId]) REFERENCES [dbo].[PlanGoodies] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Subscription_SubscriptionStatus_Id]...';


GO
ALTER TABLE [dbo].[Subscription] WITH NOCHECK
    ADD CONSTRAINT [FK_Subscription_SubscriptionStatus_Id] FOREIGN KEY ([fk_SubscriptionStatus]) REFERENCES [dbo].[SubscriptionStatus] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Subscription_Customer_Id]...';


GO
ALTER TABLE [dbo].[Subscription] WITH NOCHECK
    ADD CONSTRAINT [FK_Subscription_Customer_Id] FOREIGN KEY ([fk_CustomerId]) REFERENCES [dbo].[Customers] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Subscription_Plans_Id]...';


GO
ALTER TABLE [dbo].[Subscription] WITH NOCHECK
    ADD CONSTRAINT [FK_Subscription_Plans_Id] FOREIGN KEY ([fk_PlanId]) REFERENCES [dbo].[Plans] ([Id]);


GO
PRINT N'Creating [dbo].[FK_SubscriptionPhotos_File_Id]...';


GO
ALTER TABLE [dbo].[SubscriptionPhotos] WITH NOCHECK
    ADD CONSTRAINT [FK_SubscriptionPhotos_File_Id] FOREIGN KEY ([fk_FileId]) REFERENCES [dbo].[Files] ([Id]);


GO
PRINT N'Creating [dbo].[FK_SubscriptionPhotos_Subscription_Id]...';


GO
ALTER TABLE [dbo].[SubscriptionPhotos] WITH NOCHECK
    ADD CONSTRAINT [FK_SubscriptionPhotos_Subscription_Id] FOREIGN KEY ([fk_SubscriptionId]) REFERENCES [dbo].[Subscription] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[AspNetRoleClaims] WITH CHECK CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId];

ALTER TABLE [dbo].[AspNetUserClaims] WITH CHECK CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId];

ALTER TABLE [dbo].[AspNetUserLogins] WITH CHECK CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId];

ALTER TABLE [dbo].[AspNetUserRoles] WITH CHECK CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId];

ALTER TABLE [dbo].[AspNetUserRoles] WITH CHECK CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId];

ALTER TABLE [dbo].[AspNetUserTokens] WITH CHECK CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId];

ALTER TABLE [dbo].[Periodes] WITH CHECK CHECK CONSTRAINT [FK_PeridoesGiftedBy_Customer_Id];

ALTER TABLE [dbo].[Periodes] WITH CHECK CHECK CONSTRAINT [FK_Periodes_PaymentMethods_Id];

ALTER TABLE [dbo].[Periodes] WITH CHECK CHECK CONSTRAINT [FK_Periodes_Subscription_Id];

ALTER TABLE [dbo].[PeriodesGoodies] WITH CHECK CHECK CONSTRAINT [FK_PeriodesGoodies_Periodes_Id];

ALTER TABLE [dbo].[PeriodesGoodies] WITH CHECK CHECK CONSTRAINT [FK_PeriodesGoodies_PlanGoodies_Id];

ALTER TABLE [dbo].[PlanGoodies] WITH CHECK CHECK CONSTRAINT [FK_PlanGoodies_Templates_Id];

ALTER TABLE [dbo].[Plans] WITH CHECK CHECK CONSTRAINT [FK_Plans_PlanGoodies_Id];

ALTER TABLE [dbo].[Subscription] WITH CHECK CHECK CONSTRAINT [FK_Subscription_SubscriptionStatus_Id];

ALTER TABLE [dbo].[Subscription] WITH CHECK CHECK CONSTRAINT [FK_Subscription_Customer_Id];

ALTER TABLE [dbo].[Subscription] WITH CHECK CHECK CONSTRAINT [FK_Subscription_Plans_Id];

ALTER TABLE [dbo].[SubscriptionPhotos] WITH CHECK CHECK CONSTRAINT [FK_SubscriptionPhotos_File_Id];

ALTER TABLE [dbo].[SubscriptionPhotos] WITH CHECK CHECK CONSTRAINT [FK_SubscriptionPhotos_Subscription_Id];


GO
PRINT N'Update complete.';


GO
