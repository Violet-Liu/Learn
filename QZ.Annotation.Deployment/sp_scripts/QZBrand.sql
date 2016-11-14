--
-- 199 database


USE [QZBrand]
GO

/****** Object:  StoredProcedure [dbo].[Proc_OrgCompanyBrand_SelectByCode]    Script Date: 07/19/2016 21:34:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*×××××××××××××××××××××××××××××××××××××××
功  能：更新机构商标
日  期：2015-09-21 15:36:04
创建人：CE
××××××××××××××××××××××××××××××××××××××××*/
 
CREATE Procedure [dbo].[Proc_OrgCompanyBrand_SelectByCode]
	--@ob_id int,
	@ob_oc_code varchar(9)
AS
 
--select * from OrgCompanyBrand_Temp as a left join OrgCompanyBrandExtension_Temp as b
--ON a.ob_regNo = b.oe_ob_regNo where a.ob_oc_code=@ob_oc_code

select * from OrgCompanyBrand
--as a inner join OrgCompanyBrandExtension_Temp as b
--ON a.ob_regNo = b.oe_ob_regNo 
where ob_oc_code=@ob_oc_code

GO

---------------------------------------------------------------------------------------------------------------------------------------------------

USE [QZBrand]
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
	left join OrgCompanyBrandExtension as b 
		on a.ob_regNo = b.oe_ob_regNo and a.ob_classNo = b.oe_ob_classNo
  where a.ob_id = @ob_id

GO