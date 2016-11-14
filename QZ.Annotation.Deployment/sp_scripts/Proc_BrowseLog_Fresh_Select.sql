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