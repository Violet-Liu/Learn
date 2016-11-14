USE [QZOrgCompanyApp]
GO
/****** Object:  StoredProcedure [dbo].[Proc_BaseBetween_DeRedundent_Select]    Script Date: 8/19/2016 5:16:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Proc_BaseBetween_DeRedundent_Select]
	@Columns varchar(3000),
	@tableName varchar(2000),
	@Where varchar(1000),
	@Order varchar(100),
	@Page int,
	@pageSize int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @sql nvarchar(2000)
	declare @index1 int, @index2 int

	set @index1 = (@Page-1)*@pageSize+1
	set @index2 = @Page*@pageSize

	begin
		set @sql = N'select * from (
					select '+@Columns+', 
					ROW_NUMBER() OVER (Order by '+@Order+') as RowIndex 
					from '+@tableName+' '+@Where+'
					) t where t.RowIndex between '+cast(@index1 as varchar(10))+' and '+cast(@index2 as varchar(10))
		
		 exec(@sql)
	end	
END