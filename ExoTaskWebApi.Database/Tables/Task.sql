CREATE TABLE [dbo].[Task]
(
	[Id] INT NOT NULL IDENTITY, 
    [Title] NVARCHAR(100) NOT NULL, 
    [Done] BIT NOT NULL
        CONSTRAINT DF_Task_Done DEFAULT (0), 
    [Deleted] BIT NOT NULL 
        CONSTRAINT DF_Task_Deleted DEFAULT (0), 
    [Created] DATETIME NOT NULL 
        CONSTRAINT DF_Task_Created DEFAULT (GETDATE()), 
    [LastModified] DATETIME NULL, 
    CONSTRAINT [PK_Task] PRIMARY KEY ([Id]) 
)

GO

CREATE TRIGGER [dbo].[TR_OnDeleteTask]
    ON [dbo].[Task]
    INSTEAD OF DELETE
    AS
    BEGIN
        SET NoCount ON
        Update Task Set Deleted = 1, LastModified = GETDATE() WHERE Id in (select Id from deleted)
    END