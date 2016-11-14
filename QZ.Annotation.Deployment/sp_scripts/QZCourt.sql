-- Database QZCourt
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

---------------------------------------------------------------------------------------------------------------------------

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