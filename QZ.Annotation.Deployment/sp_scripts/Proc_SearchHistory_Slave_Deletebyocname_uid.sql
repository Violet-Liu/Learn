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