USE [QZOrgCompanyApp]
GO
/****** Object:  StoredProcedure [dbo].[Proc_CompanyTeiziTopic_Insert]    Script Date: 07/11/2016 17:28:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*×××××××××××××××××××××××××××××××××××××××
功  能：插入公司吐槽帖，增加ctt_tag字段
日  期：2015-07-31 08:57:56，2016-7-6
创建人：深圳刀客
××××××××××××××××××××××××××××××××××××××××*/
 
ALTER Procedure [dbo].[Proc_CompanyTeiziTopic_Insert]
	@ctt_id int output,
	@ctt_oc_code varchar(9),
	@ctt_oc_name varchar(128),
	@ctt_u_name nvarchar(30),
	@ctt_u_uid int,
	@ctt_content nvarchar(500),
	@ctt_date datetime,
	@ctt_oc_area varchar(8),
	@ctt_u_face varchar(100),
	@ctt_tag varchar(100)
AS
 
begin
 
INSERT INTO CompanyTeiziTopic(
	ctt_oc_code,
	ctt_oc_name,
	ctt_u_name,
	ctt_u_uid,
	ctt_content,
	ctt_date,
	ctt_oc_area,
	ctt_u_face,
	ctt_tag
) VALUES(
	@ctt_oc_code,
	@ctt_oc_name,
	@ctt_u_name,
	@ctt_u_uid,
	@ctt_content,
	@ctt_date,
	@ctt_oc_area,
	@ctt_u_face,
	@ctt_tag
)
SET @ctt_id=@@identity


UPDATE CompanyEvaluate SET 
	ce_tucaoNum=ce_tucaoNum+1
WHERE ce_oc_code=@ctt_oc_code

end