USE [QZOrgCompanyApp]
GO


DECLARE @iterator int, @tablename varchar(30), @nametail varchar(5), @sql nvarchar(3000)
set @iterator = 0
	   
while @iterator < 256
begin
	if @iterator < 10
	begin
		set @nametail = '00' + cast(@iterator as varchar(3))
	end
	else if @iterator <100
		set @nametail = '0' + cast(@iterator as varchar(3))
	else
		set @nametail = cast(@iterator as varchar(3))
		
	set @tablename = 'SearchHistoryExt_' + @nametail
set @sql = '	
CREATE TABLE ' + @tablename + ' (
	[sh_id] [int] IDENTITY(1,1) NOT NULL,
	[sh_str] [varchar](128) NOT NULL,
	[sh_type] [tinyint] NOT NULL,
	[sh_uid] [int] NULL,
	[sh_uname] [varchar](30) NULL,
 CONSTRAINT PK_' + UPPER(@tablename) + ' PRIMARY KEY CLUSTERED 
(
	[sh_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE NONCLUSTERED INDEX [IDX_Uid] ON ' + @tablename +' 
(
	[sh_uid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]

SET ANSI_PADDING OFF
'
EXEC SP_EXECUTESQL @sql
set @iterator = @iterator + 1
end
