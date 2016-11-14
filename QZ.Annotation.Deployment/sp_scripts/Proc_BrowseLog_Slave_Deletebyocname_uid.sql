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
