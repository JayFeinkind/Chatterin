CREATE TABLE [Accounts].[RefreshTokens] (
    [Token]           NVARCHAR (450) NOT NULL,
    [UserId]          INT            NOT NULL,
    [TokenExpiredUtc] DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED ([Token] ASC),
    CONSTRAINT [FK_RefreshToToken_UserId] FOREIGN KEY ([UserId]) REFERENCES [Accounts].[Users] ([Id])
);

