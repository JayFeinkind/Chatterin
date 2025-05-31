CREATE TABLE [Accounts].[Membership] (
    [Id]       INT         NOT NULL,
    [Password] BINARY (32) NOT NULL,
    [Salt]     BINARY (8)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Membership_Users] FOREIGN KEY ([Id]) REFERENCES [Accounts].[Users] ([Id])
);

