CREATE TABLE [dbo].[Squares] (
    [Index]     INT            NOT NULL,
    [Text]      NVARCHAR (100) NOT NULL,
    [BackColor] NVARCHAR (20)  NOT NULL,
    [ForeColor] NVARCHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Index] ASC)
);