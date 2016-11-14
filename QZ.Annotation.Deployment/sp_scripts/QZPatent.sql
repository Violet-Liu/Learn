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

-------------------------------------------------------------------------------------------------------------------------------------

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