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