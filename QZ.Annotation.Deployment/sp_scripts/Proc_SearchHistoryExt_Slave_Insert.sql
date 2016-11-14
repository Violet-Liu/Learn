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