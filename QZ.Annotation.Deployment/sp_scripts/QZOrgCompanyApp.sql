

-----------------------------------------------------------------------------------------------------------------------------
-- need to sync the `DistrictCode` table first
--USE [QZBase]
--GO

--/****** Object:  StoredProcedure [dbo].[Proc_DistrictCode_Selectbydc_a_code]    Script Date: 08/03/2016 18:59:31 ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--/*×××××××××××××××××××××××××××××××××××××××
--功  能：选择
--日  期：2016-06-07 13:53:26
--创建人：Sha Jianjian
--××××××××××××××××××××××××××××××××××××××××*/
 
--CREATE Procedure [dbo].[Proc_DistrictCode_Selectbydc_a_code]
--	@dc_a_code varchar(10)
--as
 
--	SELECT * from DistrictCode where dc_a_code=@dc_a_code

--GO

------------------------------------------------------------------------------------------------------------------------
-- Database QZOrgCompanyApp
USE [QZOrgCompanyApp]
GO

/****** Object:  StoredProcedure [dbo].[Proc_AppTeiziReply_Count_Get]    Script Date: 07/12/2016 12:26:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_AppTeiziReply_Count_Get]
	-- Add the parameters for the stored procedure here
	@tid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT count(1) from AppTeiziReply where atr_teizi = @tid
END


GO

-------------------------------------------------------------------------------------------------------------------------

USE [QZOrgCompanyApp]
GO
/****** Object:  StoredProcedure [dbo].[Proc_AppTeiziReply_GroupByTidSelect]    Script Date: 07/21/2016 11:00:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*×××××××××××××××××××××××××××××××××××××××
功  能：插入App回复帖
日  期：2015-08-11 11:01:11
创建人：深圳刀客
××××××××××××××××××××××××××××××××××××××××*/
 
CREATE Procedure [dbo].[Proc_AppTeiziReply_GroupByTidSelect]
 @count int
AS

declare @sql nvarchar(2000)
set @sql = '
Select Top @p_count atr_teizi from AppTeiziReply where atr_teizi > 0 group by atr_teizi'

exec SP_EXECUTESQL @sql, N'@p_count int', @p_count = @count

GO

--------------------------------------------------------------------------------------------------------------------------------

USE [QZOrgCompanyApp]
GO

/****** Object:  StoredProcedure [dbo].[Proc_AppTeiziReplyGroupByTidUid_PageSelect]    Script Date: 07/12/2016 14:05:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
CREATE Procedure [dbo].[Proc_AppTeiziReplyGroupByTidUid_PageSelect]
	@uid int,
	@pg_index int,
	@pg_size int
AS

declare @index1 int, @index2 int

set @index1 = (@pg_index-1)*@pg_size+1
set @index2 = @pg_index*@pg_size

	
Select t.atr_teizi from(
SELECT
      [atr_teizi],
   --Max(atr_date) as max_atr_date,
  ROW_NUMBER() OVER (order by Max(atr_date) desc) as RowIndex
  FROM [QZOrgCompanyApp].[dbo].[AppTeiziReply]
  where atr_u_uid = @uid and atr_status = 1
  
  GROUP BY atr_teizi, atr_u_uid
  --Order by max_atr_date desc
) t
where t.RowIndex between @index1 and @index2

GO

---------------------------------------------------------------------------------------------------------------------

USE [QZOrgCompanyApp]
GO
/****** Object:  StoredProcedure [dbo].[Proc_AppTeiziTopic_Insert]    Script Date: 07/11/2016 17:36:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*×××××××××××××××××××××××××××××××××××××××
功  能：插入App吐槽帖
日  期：2015-08-11 09:09:47
创建人：深圳刀客
××××××××××××××××××××××××××××××××××××××××*/
 
ALTER Procedure [dbo].[Proc_AppTeiziTopic_Insert]
	@att_id int output,
	@att_u_name nvarchar(30),
	@att_u_uid int,
	@att_content nvarchar(500),
	@att_date datetime,
	@att_u_face varchar(100),
	@att_tag int=0
AS
 
INSERT INTO AppTeiziTopic(
	att_u_name,
	att_u_uid,
	att_content,
	att_date,
	att_u_face,
	att_tag
) VALUES(
	@att_u_name,
	@att_u_uid,
	@att_content,
	@att_date,
	@att_u_face,
	@att_tag
)
SET @att_id=@@identity
GO

-------------------------------------------------------------------------------------------------------------------------

USE [QZOrgCompanyApp]
GO

/****** Object:  StoredProcedure [dbo].[Proc_BrowseLog_SelectPaged]    Script Date: 07/21/2016 17:23:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
/*×××××××××××××××××××××××××××××××××××××××
功  能：分页选择浏览记录
日  期：2015-07-29 16:57:05
创建人：深圳刀客
××××××××××××××××××××××××××××××××××××××××*/
 
Create Procedure [dbo].[Proc_BrowseLog_Fresh_Select]
	@count int
as
 declare @sql NVARCHAR(1000)
 set @sql = '
	select top @p_count bl_oc_code from BrowseLog order by bl_id desc '
	
 exec sp_executesql @sql, N'@p_count int', @p_count = @count

GO

-------------------------------------------------------------------------------------------------------------------------------

USE [QZOrgCompanyApp]
GO

/****** Object:  StoredProcedure [dbo].[Proc_BrowseLog_Slave_Deletebyocname_uid]    Script Date: 07/12/2016 19:24:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================
-- Author:		<Sha Jianjian>
-- Create date: <2016-2-4>
-- Description:	Delete a specified record from 'BrowseLog_xxxxxxxx'
-- =================================================================

CREATE Procedure [dbo].[Proc_BrowseLog_Slave_Deletebyocname_uid]
	@bl_u_uid int,
	@bl_oc_name varchar(100),
 @tableName VARCHAR(50)
as

DECLARE @objectID INT
SELECT @objectID=[object_id] FROM sys.tables WHERE [name]=@tableName
IF @objectID IS NULL
BEGIN
	--PRINT 'cant find the table ['+@tableName+']'
	RETURN -1
END 
--EXEC Proc_BrowseLog_Deletebybl_u_uid @bl_u_uid

DECLARE @sql NVARCHAR(2000)
SET @sql = 'delete from '  +@tableName + ' where bl_u_uid=@bl_u_uidp and bl_oc_name = @p_oc_name'
EXEC sp_EXECUTESQL @sql, N'@bl_u_uidp int, @p_oc_name varchar(100)', @bl_u_uidp = @bl_u_uid, @p_oc_name = @bl_oc_name


GO

---------------------------------------------------------------------------------------------------------------------------

USE [QZOrgCompanyApp]
GO
/****** Object:  StoredProcedure [dbo].[Proc_CompanyTeiziReply_Count_Get]    Script Date: 07/11/2016 16:25:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_CompanyTeiziReply_Count_Get]
	-- Add the parameters for the stored procedure here
	@tid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT count(1) from CompanyTeiziReply where ctr_teizi = @tid
END
GO

-------------------------------------------------------------------------------------------------------------------------

USE [QZOrgCompanyApp]
GO
/****** Object:  StoredProcedure [dbo].[Proc_CompanyTeiziTopic_Insert]    Script Date: 07/11/2016 17:28:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*×××××××××××××××××××××××××××××××××××××××
功  能：插入公司吐槽帖，增加ctt_tag字段
日  期：2015-07-31 08:57:56，2016-7-6
创建人：深圳刀客
××××××××××××××××××××××××××××××××××××××××*/
 
ALTER Procedure [dbo].[Proc_CompanyTeiziTopic_Insert]
	@ctt_id int output,
	@ctt_oc_code varchar(9),
	@ctt_oc_name varchar(128),
	@ctt_u_name nvarchar(30),
	@ctt_u_uid int,
	@ctt_content nvarchar(500),
	@ctt_date datetime,
	@ctt_oc_area varchar(8),
	@ctt_u_face varchar(100),
	@ctt_tag varchar(100)=''
AS
 
begin
 
INSERT INTO CompanyTeiziTopic(
	ctt_oc_code,
	ctt_oc_name,
	ctt_u_name,
	ctt_u_uid,
	ctt_content,
	ctt_date,
	ctt_oc_area,
	ctt_u_face,
	ctt_tag
) VALUES(
	@ctt_oc_code,
	@ctt_oc_name,
	@ctt_u_name,
	@ctt_u_uid,
	@ctt_content,
	@ctt_date,
	@ctt_oc_area,
	@ctt_u_face,
	@ctt_tag
)
SET @ctt_id=@@identity


UPDATE CompanyEvaluate SET 
	ce_tucaoNum=ce_tucaoNum+1
WHERE ce_oc_code=@ctt_oc_code

end
GO

-------------------------------------------------------------------------------

USE [QZOrgCompanyApp]
GO

/****** Object:  StoredProcedure [dbo].[Proc_SearchHistory_Slave_Deletebyocname_uid]    Script Date: 07/12/2016 17:47:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================
-- Author:		<Sha Jianjian>
-- Create date: <2016-2-4>
-- Description:	Delete a specified record from 'BrowseLog_xxxxxxxx'
-- =================================================================

CREATE Procedure [dbo].[Proc_SearchHistory_Slave_Deletebyocname_uid]
	@sh_u_uid int,
	@sh_oc_name varchar(100),
 @tableName VARCHAR(50)
as

DECLARE @objectID INT
SELECT @objectID=[object_id] FROM sys.tables WHERE [name]=@tableName
IF @objectID IS NULL
BEGIN
	--PRINT 'cant find the table ['+@tableName+']'
	RETURN -1
END 
--EXEC Proc_SearchHistory_Deletebysh_u_uid @sh_u_uid

DECLARE @sql NVARCHAR(2000)
SET @sql = 'delete from '  +@tableName + ' where sh_u_uid=@p_sh_u_uid and sh_oc_name=@p_sh_oc_name'
EXEC sp_EXECUTESQL @sql, N'@p_sh_u_uid int, @p_sh_oc_name varchar(100)', @p_sh_u_uid = @sh_u_uid, @p_sh_oc_name = @sh_oc_name


GO

--------------------------------------------------------------------------------------------------------------------
USE [QZOrgCompanyApp]
GO
/****** Object:  StoredProcedure [dbo].[Proc_SearchHistory_Slave_Deletebysh_u_uid]    Script Date: 07/28/2016 09:56:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =================================================================
-- Author:		<Sha Jianjian>
-- Create date: <2016-2-4>
-- Description:	Delete a specified record from 'BrowseLog_xxxxxxxx'
-- =================================================================

CREATE Procedure [dbo].[Proc_SearchHistoryExt_Deletebyuid]
	@sh_uid int,
	@sh_type tinyint,
 @tableName VARCHAR(50)
as

DECLARE @objectID INT
SELECT @objectID=[object_id] FROM sys.tables WHERE [name]=@tableName
IF @objectID IS NULL
BEGIN
	--PRINT 'cant find the table ['+@tableName+']'
	RETURN
END 

DECLARE @sql NVARCHAR(2000)
SET @sql = 'delete from '  +@tableName + ' where sh_uid=@p_u_uid and sh_type = @p_sh_type'
EXEC sp_EXECUTESQL @sql, N'@p_u_uid int, @p_sh_type tinyint', @p_u_uid = @sh_uid, @p_sh_type = @sh_type
GO

----------------------------------------------------------------------------------------------------------

USE [QZOrgCompanyApp]
GO

/****** Object:  StoredProcedure [dbo].[Proc_SearchHistoryExt_Insert]    Script Date: 07/19/2016 10:45:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*×××××××××××××××××××××××××××××××××××××××
功  能：插入搜索记录
日  期：2015-07-29 10:26:47
创建人：深圳刀客
××××××××××××××××××××××××××××××××××××××××*/
 
CREATE Procedure [dbo].[Proc_SearchHistoryExt_Insert]
	@sh_id int output
	  ,@sh_str varchar(128)
      ,@sh_type tinyint
      ,@sh_uid int
      ,@sh_uname varchar(30)
AS
 
INSERT INTO SearchHistoryExt(
	sh_str,
	sh_type,
	sh_uid,
	sh_uname
) VALUES(
	@sh_str
      ,@sh_type
      ,@sh_uid
      ,@sh_uname
)
SET @sh_id=@@identity


GO

----------------------------------------------------------------------------------------------------

USE [QZOrgCompanyApp]
GO

/****** Object:  StoredProcedure [dbo].[Proc_SearchHistory_Slave_Insert]    Script Date: 07/19/2016 10:35:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =======================================================================
-- Author:		<Sha Jianjian>
-- Create date: <2016-2-4>
-- Description:	Insert a record into 'SearchHistory_xxxxxxxx' table, where 
--				'xxxxxxxx' represent a number
--				We will hash each user's record into different table, just
--				according to modular arithmetic on bl_u_uid. 
-- =======================================================================
CREATE PROCEDURE [dbo].[Proc_SearchHistoryExt_Slave_Insert]
	   @sh_id int output
	  ,@sh_str varchar(128)
      ,@sh_type tinyint
      ,@sh_uid int
      ,@sh_uname varchar(30),
	@tableName VARCHAR(50)	-- Should pass a param to indicate on which table the operation is executed
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- insert into master table 'SearchHistory' first
	exec Proc_SearchHistoryExt_Insert @sh_id output, @sh_str, @sh_type, @sh_uid, @sh_uname
	
	
	declare @sql nvarchar(2000)
	set @sql = N'INSERT INTO ' +@tableName+ '(
	sh_str,
	sh_type,
	sh_uid,
	sh_uname
) VALUES(
 @p_sh_str,
	@p_sh_type,
	@p_sh_uid,
	@p_sh_uname)'

exec sp_executesql @sql, N'
	   @p_sh_str varchar(128),
	@p_sh_type tinyint,
	@p_sh_uid int,
	@p_sh_uname varchar(30)
'
   ,@p_sh_str = @sh_str,
	@p_sh_type = @sh_type,
	@p_sh_uid = @sh_uid,
	@p_sh_uname = @sh_uname


	SET @sh_id= @@identity 
	
	
END



GO

---------------------------------------------------------------------------------------------------------------------------------------

USE [QZOrgCompanyApp]
GO

/****** Object:  StoredProcedure [dbo].[Proc_UserAppendInf_Selectbyuid]    Script Date: 07/08/2016 15:34:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_UserAppendInf_Selectbyuid]
	-- Add the parameters for the stored procedure here
	@uid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM UserAppendInf WHERE uai_uid = @uid
END

GO

----------------------------------------------------------------------------------------------------------------------------------------

USE [QZOrgCompanyApp]
GO

/****** Object:  StoredProcedure [dbo].[Proc_UserAppendInf_Update]    Script Date: 07/08/2016 15:35:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_UserAppendInf_Update] 
	@uid int,
	@field varchar(20),
	@value varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    DECLARE @sql NVARCHAR(1000)
    DECLARE @aid int
    set @aid = -1
    select @aid = uai_uid from UserAppendInf where uai_uid = @uid
    
    if(@aid < 0)	-- not existed
		begin
		set @sql = 'insert into UserAppendInf(uai_uid
		  ,uai_company
		  ,uai_position
		  ,uai_business
		  ,uai_b_favorite
		  ,uai_p_favorite
		  ,uai_sign) 
		  values(
		  @p_uid,
		  '''',
		  '''',
		  '''',
		  '''',
		  '''',
		  '''')'
		  exec SP_EXECUTESQL @sql,
				N'@p_uid int',
				@p_uid = @uid
	      
		end

	

	
	set @sql = 'update UserAppendInf set ' + @field + '= @p_value where uai_uid = @p_uid'
    exec SP_EXECUTESQL @sql,
			N'
			@p_value varchar(100),
			@p_uid int',
			@p_value = @value,
			@p_uid = @uid		
END

GO

---------------------------------------------------------------------------------------------------------------

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
GO

----------------------------------------------------------------------------------------------------------------------------------
USE [QZOrgCompanyApp]
GO

/****** Object:  StoredProcedure [dbo].[Proc_CompanyTieziImage_Selectbycti_tiezi_id]    Script Date: 08/01/2016 11:10:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*×××××××××××××××××××××××××××××××××××××××
功  能：选择
日  期：2016-05-09 17:14:28
创建人：Sha Jianjian
××××××××××××××××××××××××××××××××××××××××*/
 
Create Procedure [dbo].[Proc_CompanyTieziImage_Selectbytidtype]
	@cti_tiezi_id int,
	@cti_tiezi_type int
as
 
	SELECT * from CompanyTieziImage where cti_tiezi_id=@cti_tiezi_id and cti_tiezi_type=@cti_tiezi_type

GO


USE [QZOrgCompanyApp]
GO
/****** Object:  StoredProcedure [dbo].[Proc_AppTieziImage_Selectbytidtype]    Script Date: 08/01/2016 13:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*×××××××××××××××××××××××××××××××××××××××
功  能：选择
日  期：2016-05-09 13:49:17
创建人：Sha Jianjian
××××××××××××××××××××××××××××××××××××××××*/
 
CREATE Procedure [dbo].[Proc_AppTieziImage_Selectbytidtype]
	@ati_tiezi_id int,
	@ati_tiezi_type int
as
 
	SELECT * from AppTieziImage where ati_tiezi_id=@ati_tiezi_id and ati_tiezi_type=@ati_tiezi_type
GO


----------------------------------------------------------------------------------------------------------------------

USE [QZOrgCompanyApp]
GO

/****** Object:  Table [dbo].[UserAppendInf]    Script Date: 08/10/2016 13:21:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[UserAppendInf](
	[uai_id] [int] IDENTITY(1,1) NOT NULL,
	[uai_uid] [int] NOT NULL,
	[uai_company] [varchar](100) NOT NULL,
	[uai_position] [varchar](50) NOT NULL,
	[uai_business] [varchar](30) NOT NULL,
	[uai_b_favorite] [varchar](100) NOT NULL,
	[uai_p_favorite] [varchar](100) NOT NULL,
	[uai_sign] [varchar](100) NOT NULL,
 CONSTRAINT [PK_UserAppendInf] PRIMARY KEY CLUSTERED 
(
	[uai_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserAppendInf', @level2type=N'COLUMN',@level2name=N'uai_uid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所在公司' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserAppendInf', @level2type=N'COLUMN',@level2name=N'uai_company'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属职业' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserAppendInf', @level2type=N'COLUMN',@level2name=N'uai_position'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属行业' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserAppendInf', @level2type=N'COLUMN',@level2name=N'uai_business'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'感兴趣的行业' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserAppendInf', @level2type=N'COLUMN',@level2name=N'uai_b_favorite'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'感兴趣的职位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserAppendInf', @level2type=N'COLUMN',@level2name=N'uai_p_favorite'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'个人签名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserAppendInf', @level2type=N'COLUMN',@level2name=N'uai_sign'
GO


USE [QZOrgCompanyApp]
GO

/****** Object:  Index [IDX_Uid]    Script Date: 08/10/2016 13:22:40 ******/
CREATE NONCLUSTERED INDEX [IDX_Uid] ON [dbo].[UserAppendInf] 
(
	[uai_uid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


