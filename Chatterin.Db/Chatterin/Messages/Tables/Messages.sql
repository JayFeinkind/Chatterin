CREATE TABLE [Messages].[Messages] (
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [MessageTxt]         NVARCHAR (1000) NOT NULL,
    [SentUtc]            DATETIME2 (7)   NOT NULL,
    [UserConversationId] INT             NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Messages_UserConversations] FOREIGN KEY ([UserConversationId]) REFERENCES [Messages].[UserConversations] ([Id])
);

