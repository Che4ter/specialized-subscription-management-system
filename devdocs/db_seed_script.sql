/*
Add some default values
*/
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
/*Pointer used for text / image updates. This might not be needed, but is declared here just in case*/
DECLARE @pv binary(16)
BEGIN TRANSACTION
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
ALTER TABLE [dbo].[AspNetUserTokens] DROP CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
ALTER TABLE [dbo].[PlanGoodies] DROP CONSTRAINT [FK_PlanGoodies_Templates_Id]
SET IDENTITY_INSERT [dbo].[PaymentMethods] ON
INSERT INTO [dbo].[PaymentMethods] ([Id], [Name], [Fee]) VALUES (1, N'Barzahlung', NULL)
INSERT INTO [dbo].[PaymentMethods] ([Id], [Name], [Fee]) VALUES (2, N'Rechnung', NULL)
INSERT INTO [dbo].[PaymentMethods] ([Id], [Name], [Fee]) VALUES (3, N'Kreditkarte', NULL)
INSERT INTO [dbo].[PaymentMethods] ([Id], [Name], [Fee]) VALUES (4, N'Maestro', NULL)
INSERT INTO [dbo].[PaymentMethods] ([Id], [Name], [Fee]) VALUES (5, N'Postcard', NULL)
SET IDENTITY_INSERT [dbo].[PaymentMethods] OFF
SET IDENTITY_INSERT [dbo].[PlanGoodies] ON
INSERT INTO [dbo].[PlanGoodies] ([Id], [Name], [fk_templateLabel], [Bezeichnung]) VALUES (1, N'Weinflasche', 1, N'Rebe')
INSERT INTO [dbo].[PlanGoodies] ([Id], [Name], [fk_templateLabel], [Bezeichnung]) VALUES (2, N'Olivenöl Flasche', 2, N'Baum')
SET IDENTITY_INSERT [dbo].[PlanGoodies] OFF
SET IDENTITY_INSERT [dbo].[Templates] ON
INSERT INTO [dbo].[Templates] ([Id], [Name]) VALUES (1, N'Wein Etikette')
INSERT INTO [dbo].[Templates] ([Id], [Name]) VALUES (2, N'Olivenöl Etikette ')
SET IDENTITY_INSERT [dbo].[Templates] OFF
SET IDENTITY_INSERT [dbo].[SubscriptionStatus] ON
INSERT INTO [dbo].[SubscriptionStatus] ([Id], [Label]) VALUES (1, N'Aktiv')
INSERT INTO [dbo].[SubscriptionStatus] ([Id], [Label]) VALUES (2, N'Läuft aus')
INSERT INTO [dbo].[SubscriptionStatus] ([Id], [Label]) VALUES (3, N'Rechnung noch nicht bezahlt')
INSERT INTO [dbo].[SubscriptionStatus] ([Id], [Label]) VALUES (4, N'Ausgelaufen')
SET IDENTITY_INSERT [dbo].[SubscriptionStatus] OFF
ALTER TABLE [dbo].[AspNetUserLogins]
    ADD CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[AspNetUserClaims]
    ADD CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[AspNetUserTokens]
    ADD CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[PlanGoodies]
    ADD CONSTRAINT [FK_PlanGoodies_Templates_Id] FOREIGN KEY ([fk_templateLabel]) REFERENCES [dbo].[Templates] ([Id])
COMMIT TRANSACTION
