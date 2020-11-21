IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [TransactionStatuses] (
    [Id] int NOT NULL,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_TransactionStatuses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TransactionEntries] (
    [Id] bigint NOT NULL IDENTITY,
    [TransactionId] nvarchar(50) NOT NULL,
    [Amount] decimal(18,2) NOT NULL,
    [CurrencyCode] nvarchar(4) NOT NULL,
    [TransactionDate] datetime2 NOT NULL,
    [TransactionStatusId] int NOT NULL,
    CONSTRAINT [PK_TransactionEntries] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TransactionEntries_TransactionStatuses_TransactionStatusId] FOREIGN KEY ([TransactionStatusId]) REFERENCES [TransactionStatuses] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[TransactionStatuses]'))
    SET IDENTITY_INSERT [TransactionStatuses] ON;
INSERT INTO [TransactionStatuses] ([Id], [Name])
VALUES (1, N'A');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[TransactionStatuses]'))
    SET IDENTITY_INSERT [TransactionStatuses] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[TransactionStatuses]'))
    SET IDENTITY_INSERT [TransactionStatuses] ON;
INSERT INTO [TransactionStatuses] ([Id], [Name])
VALUES (2, N'R');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[TransactionStatuses]'))
    SET IDENTITY_INSERT [TransactionStatuses] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[TransactionStatuses]'))
    SET IDENTITY_INSERT [TransactionStatuses] ON;
INSERT INTO [TransactionStatuses] ([Id], [Name])
VALUES (3, N'D');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[TransactionStatuses]'))
    SET IDENTITY_INSERT [TransactionStatuses] OFF;
GO

CREATE INDEX [IX_TransactionEntries_TransactionStatusId] ON [TransactionEntries] ([TransactionStatusId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201121075618_Initial', N'5.0.0');
GO

COMMIT;
GO
