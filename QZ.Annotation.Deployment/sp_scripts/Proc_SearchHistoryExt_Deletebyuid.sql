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