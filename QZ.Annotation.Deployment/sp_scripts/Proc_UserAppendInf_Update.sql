USE [QZOrgCompanyApp]
GO

/****** Object:  StoredProcedure [dbo].[Proc_UserAppendInf_Update]    Script Date: 07/08/2016 15:35:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_UserAppendInf_Update] 
	@uid int,
	@field varchar(20),
	@value varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    DECLARE @sql NVARCHAR(1000)
    DECLARE @aid int
    set @aid = -1
    select @aid = uai_uid from UserAppendInf where uai_uid = @uid
    
    if(@aid < 0)	-- not existed
		begin
		set @sql = 'insert into UserAppendInf(uai_uid
		  ,uai_company
		  ,uai_position
		  ,uai_business
		  ,uai_b_favorite
		  ,uai_p_favorite
		  ,uai_sign) 
		  values(
		  @p_uid,
		  '''',
		  '''',
		  '''',
		  '''',
		  '''',
		  '''')'
		  exec SP_EXECUTESQL @sql,
				N'@p_uid int',
				@p_uid = @uid
	      
		end

	

	
	set @sql = 'update UserAppendInf set ' + @field + '= @p_value where uai_uid = @p_uid'
    exec SP_EXECUTESQL @sql,
			N'
			@p_value varchar(100),
			@p_uid int',
			@p_value = @value,
			@p_uid = @uid		
END

GO
