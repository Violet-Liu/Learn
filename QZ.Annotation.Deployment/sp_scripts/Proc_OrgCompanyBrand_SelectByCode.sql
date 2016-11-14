--
-- 199 database


USE [QZOrgCompanyGsxt]
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

select * from OrgCompanyBrand_Temp 
--as a inner join OrgCompanyBrandExtension_Temp as b
--ON a.ob_regNo = b.oe_ob_regNo 
where ob_oc_code=@ob_oc_code

GO