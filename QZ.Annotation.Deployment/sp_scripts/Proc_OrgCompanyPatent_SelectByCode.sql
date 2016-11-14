--
-- 199 database



USE [QZPatent]
GO

/****** Object:  StoredProcedure [dbo].[Proc_OrgCompanyPatent_SelectByCode]    Script Date: 07/19/2016 21:33:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*×××××××××××××××××××××××××××××××××××××××
功  能：更新机构商标
日  期：2015-09-21 15:36:04
创建人：CE
××××××××××××××××××××××××××××××××××××××××*/
 
Create Procedure [dbo].[Proc_OrgCompanyPatent_SelectByCode]
	--@ob_id int,
	@oc_code varchar(9)
AS
 
select * from OrgCompanyPatent
WHERE oc_code= @oc_code



GO