CREATE TABLE [dbo].[Author]
(
	[ID]        INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Author] PRIMARY KEY ([ID])
)
