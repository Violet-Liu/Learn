USE [QZOrgCompanyApp]
GO

/****** Object:  StoredProcedure [dbo].[Proc_UserAppendInf_Selectbyuid]    Script Date: 07/08/2016 15:34:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_UserAppendInf_Selectbyuid]
	-- Add the parameters for the stored procedure here
	@uid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM UserAppendInf WHERE uai_uid = @uid
END

GO