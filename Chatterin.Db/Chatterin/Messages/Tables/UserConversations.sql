CREATE TABLE [Messages].[UserConversations] (
    [Id]             INT IDENTITY (1, 1) NOT NULL,
    [ConversationId] INT NOT NULL,
    [UserId]         INT NOT NULL,
    [Archived]       BIT DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_UserConversations] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserConversation_Conversation] FOREIGN KEY ([ConversationId]) REFERENCES [Messages].[Conversations] ([Id]),
    CONSTRAINT [FK_UserConversation_UserId] FOREIGN KEY ([UserId]) REFERENCES [Accounts].[Users] ([Id])
);

