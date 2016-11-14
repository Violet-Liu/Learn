USE [QZOrgCompanyApp]
GO

/****** Object:  Table [dbo].[CommentTipOff]    Script Date: 10/22/2016 12:08:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CommentTipOff](
	[cto_id] [int] IDENTITY(1,1) NOT NULL,
	[cto_tiezi_id] [int] NOT NULL,
	[cto_tiezi_type] [tinyint] NOT NULL,
	[cto_YuanGao_Uid] [int] NOT NULL,
	[cto_YuanGao_Uname] [varchar](128) NOT NULL,
	[cto_BeiGao_Uid] [int] NOT NULL,
	[cto_BeiGao_Uname] [varchar](128) NOT NULL,
	[cto_status] [tinyint] NOT NULL,
	[cto_des] [varchar](1000) NOT NULL,
	[cto_time] [datetime] NOT NULL,
	[cto_shield] [tinyint] NOT NULL,
 CONSTRAINT [PK_CommentTipOff] PRIMARY KEY CLUSTERED 
(
	[cto_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帖子id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CommentTipOff', @level2type=N'COLUMN',@level2name=N'cto_tiezi_id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帖子类型，1：公司贴，2公司贴回复，3app贴，4app贴回复' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CommentTipOff', @level2type=N'COLUMN',@level2name=N'cto_tiezi_type'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原告或屏蔽人uid' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CommentTipOff', @level2type=N'COLUMN',@level2name=N'cto_YuanGao_Uid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原告或屏蔽人名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CommentTipOff', @level2type=N'COLUMN',@level2name=N'cto_YuanGao_Uname'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'被告uid' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CommentTipOff', @level2type=N'COLUMN',@level2name=N'cto_BeiGao_Uid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'被告人名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CommentTipOff', @level2type=N'COLUMN',@level2name=N'cto_BeiGao_Uname'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1：开始举报 2：举报核实 3：处理举报 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CommentTipOff', @level2type=N'COLUMN',@level2name=N'cto_status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'举报原因，描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CommentTipOff', @level2type=N'COLUMN',@level2name=N'cto_des'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'举报时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CommentTipOff', @level2type=N'COLUMN',@level2name=N'cto_time'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1:屏蔽，2：取消屏蔽，0：未屏蔽' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CommentTipOff', @level2type=N'COLUMN',@level2name=N'cto_shield'
GO


/****** Object:  Index [IDX_YG_BG_TieZi]    Script Date: 10/22/2016 12:09:03 AM ******/
CREATE NONCLUSTERED INDEX [IDX_YG_BG_TieZi] ON [dbo].[CommentTipOff]
(
	[cto_YuanGao_Uid] ASC,
	[cto_BeiGao_Uid] ASC,
	[cto_tiezi_id] ASC,
	[cto_tiezi_type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


USE [QZOrgCompanyApp]
GO

/****** Object:  StoredProcedure [dbo].[Proc_CommentTipOff_Insert]    Script Date: 10/22/2016 12:08:06 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*×××××××××××××××××××××××××××××××××××××××
功  能：插入
日  期：2016-10-22 12:10:14
创建人：Sha Jianjian
××××××××××××××××××××××××××××××××××××××××*/
 
CREATE Procedure [dbo].[Proc_CommentTipOff_Insert]
	@cto_id int output,
	@cto_tiezi_id int,
	@cto_tiezi_type tinyint,
	@cto_YuanGao_Uid int,
	@cto_YuanGao_Uname varchar(128),
	@cto_BeiGao_Uid int,
	@cto_BeiGao_Uname varchar(128),
	@cto_status tinyint,
	@cto_des varchar(1000),
	@cto_time datetime,
	@cto_shield tinyint
AS
 
INSERT INTO CommentTipOff(
	cto_tiezi_id,
	cto_tiezi_type,
	cto_YuanGao_Uid,
	cto_YuanGao_Uname,
	cto_BeiGao_Uid,
	cto_BeiGao_Uname,
	cto_status,
	cto_des,
	cto_time,
	cto_shield
) VALUES(
	@cto_tiezi_id,
	@cto_tiezi_type,
	@cto_YuanGao_Uid,
	@cto_YuanGao_Uname,
	@cto_BeiGao_Uid,
	@cto_BeiGao_Uname,
	@cto_status,
	@cto_des,
	@cto_time,
	@cto_shield
)
SET @cto_id=@@identity

GO

/****** Object:  StoredProcedure [dbo].[Proc_CommentTipOff_SelectbyBeiGao_Type]    Script Date: 10/22/2016 12:08:07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*×××××××××××××××××××××××××××××××××××××××
功  能：选择
日  期：2016-10-22 12:10:14
创建人：Sha Jianjian
××××××××××××××××××××××××××××××××××××××××*/
 
CREATE Procedure [dbo].[Proc_CommentTipOff_SelectbyBeiGao_Type]
	@cto_BeiGao_Uid int,
	@cto_tiezi_type tinyint,
	@cto_tiezi_id int
as
 
	SELECT * from CommentTipOff where cto_BeiGao_Uid=@cto_BeiGao_Uid and cto_tiezi_type=@cto_tiezi_type and cto_tiezi_id = @cto_tiezi_id

GO

/****** Object:  StoredProcedure [dbo].[Proc_CommentTipOff_SelectbyYuanGao_Type]    Script Date: 10/22/2016 12:08:07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 

 
 
/*×××××××××××××××××××××××××××××××××××××××
功  能：选择
日  期：2016-10-22 12:10:14
创建人：Sha Jianjian
××××××××××××××××××××××××××××××××××××××××*/
 
CREATE Procedure [dbo].[Proc_CommentTipOff_SelectbyYuanGao_Type]
	@cto_YuanGao_Uid int,
	@cto_tiezi_type tinyint,
	@cto_tiezi_id int
as
 
	SELECT * from CommentTipOff where cto_YuanGao_Uid=@cto_YuanGao_Uid and cto_tiezi_type=@cto_tiezi_type and cto_tiezi_id = @cto_tiezi_id

GO

/****** Object:  StoredProcedure [dbo].[Proc_CommentTipOff_SelectPaged]    Script Date: 10/22/2016 12:08:07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
/*×××××××××××××××××××××××××××××××××××××××
功  能：分页选择
日  期：2016-10-22 12:10:14
创建人：Sha Jianjian
××××××××××××××××××××××××××××××××××××××××*/
 
Create Procedure [dbo].[Proc_CommentTipOff_SelectPaged]
	@columns varchar(300),
	@where varchar(1000),
	@order varchar(100),
	@page int,
	@pageSize int,
	@rowCount int output
as
 
	exec PROC_BaseBetween_SelectByPageIndex @columns,'CommentTipOff',@where,@order,@page,@pageSize,@rowCount output

GO

/****** Object:  StoredProcedure [dbo].[Proc_CommentTipOff_Update]    Script Date: 10/22/2016 12:08:07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 
/*×××××××××××××××××××××××××××××××××××××××
功  能：更新
日  期：2016-10-22 12:10:14
创建人：Sha Jianjian
××××××××××××××××××××××××××××××××××××××××*/
 
Create Procedure [dbo].[Proc_CommentTipOff_Update]
	@cto_id int,
	@cto_tiezi_id int,
	@cto_tiezi_type tinyint,
	@cto_YuanGao_Uid int,
	@cto_YuanGao_Uname varchar(128),
	@cto_BeiGao_Uid int,
	@cto_BeiGao_Uname varchar(128),
	@cto_status tinyint,
	@cto_des varchar(1000),
	@cto_time datetime,
	@cto_shield tinyint
AS
 
UPDATE CommentTipOff SET 
	cto_tiezi_id=@cto_tiezi_id,
	cto_tiezi_type=@cto_tiezi_type,
	cto_YuanGao_Uid=@cto_YuanGao_Uid,
	cto_YuanGao_Uname=@cto_YuanGao_Uname,
	cto_BeiGao_Uid=@cto_BeiGao_Uid,
	cto_BeiGao_Uname=@cto_BeiGao_Uname,
	cto_status=@cto_status,
	cto_des=@cto_des,
	cto_time=@cto_time,
	cto_shield=@cto_shield
WHERE cto_id=@cto_id

GO


