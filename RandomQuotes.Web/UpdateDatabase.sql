IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180216044422_InitialCreate')
BEGIN
    CREATE TABLE [Quote] (
        [ID] int NOT NULL IDENTITY,
        [Author] nvarchar(max) NULL,
        [QuoteText] nvarchar(max) NULL,
        CONSTRAINT [PK_Quote] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180216044422_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180216044422_InitialCreate', N'2.0.1-rtm-125');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180216063841_AddedAuthorsTable')
BEGIN
    EXEC sp_rename N'Quote.Author', N'AuthorName', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180216063841_AddedAuthorsTable')
BEGIN
    ALTER TABLE [Quote] ADD [AuthorID] int NOT NULL DEFAULT 1;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180216063841_AddedAuthorsTable')
BEGIN
    CREATE TABLE [Author] (
        [AuthorID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Author] PRIMARY KEY ([AuthorID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180216063841_AddedAuthorsTable')
BEGIN
    INSERT INTO [Author] ([Name]) VALUES ('SYSTEM')
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180216063841_AddedAuthorsTable')
BEGIN
    CREATE INDEX [IX_Quote_AuthorID] ON [Quote] ([AuthorID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180216063841_AddedAuthorsTable')
BEGIN
    ALTER TABLE [Quote] ADD CONSTRAINT [FK_Quote_Author_AuthorID] FOREIGN KEY ([AuthorID]) REFERENCES [Author] ([AuthorID]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180216063841_AddedAuthorsTable')
BEGIN
    INSERT INTO [Author] ([Name]) SELECT DISTINCT q.AuthorName AS [Name] FROM Quote q
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180216063841_AddedAuthorsTable')
BEGIN
    UPDATE Quote SET Quote.AuthorID = a.AuthorID FROM Author a WHERE Quote.AuthorName = a.[Name]
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180216063841_AddedAuthorsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180216063841_AddedAuthorsTable', N'2.0.1-rtm-125');
END;

GO

