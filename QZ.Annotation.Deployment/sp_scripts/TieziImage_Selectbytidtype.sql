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