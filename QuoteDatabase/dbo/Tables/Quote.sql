CREATE TABLE [dbo].[Quote] (
    [ID]        INT            IDENTITY (1, 1) NOT NULL,
    [Author]    NVARCHAR (MAX) NULL,
    [QuoteText] NVARCHAR (MAX) NULL,
    [AuthorID] INT NULL, 
    CONSTRAINT [PK_Quote] PRIMARY KEY CLUSTERED ([ID] ASC), 
    CONSTRAINT [FK_Quote_Author] FOREIGN KEY (AuthorID) REFERENCES [Author]([ID])
);

