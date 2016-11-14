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
 
ALTER Procedure [dbo].[Proc_AppTeiziReply_GroupByTidSelect]
 @count int
AS

declare @sql nvarchar(2000)
set @sql = '
Select Top @p_count atr_teizi from AppTeiziReply where atr_teizi > 0 group by atr_teizi'

exec SP_EXECUTESQL @sql, N'@p_count int', @p_count = @count