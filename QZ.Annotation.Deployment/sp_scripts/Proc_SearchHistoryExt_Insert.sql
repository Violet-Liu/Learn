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