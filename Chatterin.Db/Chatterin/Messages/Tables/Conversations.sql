CREATE TABLE [Messages].[Conversations] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [CreatedUtc] DATETIME2 (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

