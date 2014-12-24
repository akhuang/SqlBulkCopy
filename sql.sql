
/****** Object:  Table [dbo].[OutBoundCall]    Script Date: 12/24/2014 14:38:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OutBoundCall]') AND type in (N'U'))
DROP TABLE [dbo].[OutBoundCall]
GO

/****** Object:  Table [dbo].[OutBoundCall]    Script Date: 12/24/2014 14:38:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OutBoundCall](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TinetId] [bigint] NULL,
	[uniquieId] [nvarchar](200) NULL,
	[Numer_Thunk] [nvarchar](200) NULL,
	[Customer_Number] [nvarchar](50) NULL,
	[Client_Number] [nvarchar](50) NULL,
	[Client_Area_Code] [nvarchar](10) NULL,
	[Cno] [nvarchar](50) NULL,
	[Exten] [nvarchar](50) NULL,
	[Client_Name] [nvarchar](50) NULL,
	[Start_Time] [bigint] NULL,
	[Answer_Time] [bigint] NULL,
	[Bridge_Time] [bigint] NULL,
	[End_Time] [bigint] NULL,
	[Bill_Duration] [bigint] NULL,
	[Bridge_Duration] [bigint] NULL,
	[Total_Duration] [bigint] NULL,
	[Cost] [decimal](18, 3) NULL,
	[Record_File] [nvarchar](200) NULL,
	[Score] [int] NULL,
	[Status] [int] NULL,
	[Mark] [int] NULL,
	[Mark_Data] [nvarchar](50) NULL,
	[End_Reason] [nvarchar](50) NULL,
	[Create_Time] [datetime] NULL,
 CONSTRAINT [PK_OutBoundCall] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


