USE [QZCourt]
GO

/****** Object:  StoredProcedure [dbo].[JudgementDoc_SelectById]    Script Date: 07/27/2016 17:51:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_Shixin_SelectById]
	@sx_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Shixin where sx_id = @sx_id
END

GO


USE [QZPatent]
GO

/****** Object:  StoredProcedure [dbo].[CompanyPatent_SelectById]    Script Date: 07/27/2016 19:24:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_CompanyPatent_SelectById]
	@id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from OrgCompanyPatent where ID = @id
END

GO

USE [QZOrgCompanyGsxt]
GO

/****** Object:  StoredProcedure [dbo].[Proc_OrgCompanyBrand_Insert]    Script Date: 07/27/2016 19:50:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


USE [QZOrgCompanyGsxt]
GO
/****** Object:  StoredProcedure [dbo].[Proc_OrgCompanyBrandFull_SelectById]    Script Date: 07/27/2016 20:08:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*×××××××××××××××××××××××××××××××××××××××
功  能：插入机构商标
日  期：2015-09-21 15:34:17
创建人：CE
××××××××××××××××××××××××××××××××××××××××*/
 
ALTER Procedure [dbo].[Proc_OrgCompanyBrandFull_SelectById]
	@ob_id int
	
AS
 
-- select * from OrgCompanyBrand_Temp 
----as a inner join OrgCompanyBrandExtension_Temp as b
----ON a.ob_regNo = b.oe_ob_regNo 
--where ob_oc_code=@ob_oc_code

Select a.ob_dlrmc, a.ob_zyksqx, a.ob_zyjsqx, b.oe_service, b.oe_serviceCode, b.oe_brandProcess from OrgCompanyBrand as a 
	left join OrgCompanyBrandExtension_Temp as b 
		on a.ob_regNo = b.oe_ob_regNo and a.ob_classNo = b.oe_ob_classNo
  where a.ob_id = @ob_id