USE [QZOrgCompanyApp]
GO
/****** Object:  StoredProcedure [dbo].[Proc_AppTeiziTopic_Insert]    Script Date: 07/11/2016 17:36:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*×××××××××××××××××××××××××××××××××××××××
功  能：插入App吐槽帖
日  期：2015-08-11 09:09:47
创建人：深圳刀客
××××××××××××××××××××××××××××××××××××××××*/
 
ALTER Procedure [dbo].[Proc_AppTeiziTopic_Insert]
	@att_id int output,
	@att_u_name nvarchar(30),
	@att_u_uid int,
	@att_content nvarchar(500),
	@att_date datetime,
	@att_u_face varchar(100),
	@att_tag int
AS
 
INSERT INTO AppTeiziTopic(
	att_u_name,
	att_u_uid,
	att_content,
	att_date,
	att_u_face,
	att_tag
) VALUES(
	@att_u_name,
	@att_u_uid,
	@att_content,
	@att_date,
	@att_u_face,
	@att_tag
)
SET @att_id=@@identity
