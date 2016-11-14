USE [QZOrgCompanyApp]
GO

/****** Object:  StoredProcedure [dbo].[Proc_AppTeiziReply_Count_Get]    Script Date: 07/12/2016 12:26:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_AppTeiziReply_Count_Get]
	-- Add the parameters for the stored procedure here
	@tid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT count(1) from AppTeiziReply where atr_teizi = @tid
END


GO