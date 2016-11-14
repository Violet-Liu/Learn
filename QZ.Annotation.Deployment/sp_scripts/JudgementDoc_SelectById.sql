USE [QZCourt]
GO

/****** Object:  StoredProcedure [dbo].[JudgementDoc_SelectById]    Script Date: 07/27/2016 17:17:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_JudgementDoc_SelectById]
	@jd_id uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from JudgementDocCombine where jd_id = @jd_id
END

GO

USE [QZBase]
GO

/****** Object:  StoredProcedure [dbo].[Proc_DistrictCode_Selectbydc_a_code]    Script Date: 08/03/2016 18:59:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*×××××××××××××××××××××××××××××××××××××××
功  能：选择
日  期：2016-06-07 13:53:26
创建人：Sha Jianjian
××××××××××××××××××××××××××××××××××××××××*/
 
CREATE Procedure [dbo].[Proc_DistrictCode_Selectbydc_a_code]
	@dc_a_code varchar(10)
as
 
	SELECT * from DistrictCode where dc_a_code=@dc_a_code

GO