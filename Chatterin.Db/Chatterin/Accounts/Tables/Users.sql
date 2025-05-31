CREATE TABLE [Accounts].[Users] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [EmailAddress] NVARCHAR (500) NOT NULL,
    [UserName]     NVARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

