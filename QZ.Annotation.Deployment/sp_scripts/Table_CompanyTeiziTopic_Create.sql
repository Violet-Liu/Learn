USE [QZOrgCompanyApp]
GO

/****** Object:  Table [dbo].[CompanyTeiziTopic]    Script Date: 07/11/2016 17:27:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CompanyTeiziTopic](
	[ctt_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[ctt_oc_code] [varchar](9) NOT NULL,
	[ctt_oc_name] [varchar](128) NOT NULL,
	[ctt_u_name] [nvarchar](30) NOT NULL,
	[ctt_u_uid] [int] NOT NULL,
	[ctt_content] [nvarchar](500) NOT NULL,
	[ctt_date] [datetime] NOT NULL,
	[ctt_oc_area] [varchar](8) NOT NULL,
	[ctt_u_face] [varchar](100) NULL,
	[ctt_status] [tinyint] NOT NULL,
	[ctt_tag] [varchar](100) NULL,
 CONSTRAINT [PK_COMPANYTEIZITOPIC] PRIMARY KEY CLUSTERED 
(
	[ctt_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自动编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CompanyTeiziTopic', @level2type=N'COLUMN',@level2name=N'ctt_id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'机构代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CompanyTeiziTopic', @level2type=N'COLUMN',@level2name=N'ctt_oc_code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'机构名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CompanyTeiziTopic', @level2type=N'COLUMN',@level2name=N'ctt_oc_name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CompanyTeiziTopic', @level2type=N'COLUMN',@level2name=N'ctt_u_name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CompanyTeiziTopic', @level2type=N'COLUMN',@level2name=N'ctt_u_uid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帖子主题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CompanyTeiziTopic', @level2type=N'COLUMN',@level2name=N'ctt_content'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发表日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CompanyTeiziTopic', @level2type=N'COLUMN',@level2name=N'ctt_date'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'头像' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CompanyTeiziTopic', @level2type=N'COLUMN',@level2name=N'ctt_u_face'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态  0-删除 1-审核通过（默认） 2-审核不通过 3-未审核（预留）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CompanyTeiziTopic', @level2type=N'COLUMN',@level2name=N'ctt_status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论标签' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CompanyTeiziTopic', @level2type=N'COLUMN',@level2name=N'ctt_tag'
GO

ALTER TABLE [dbo].[CompanyTeiziTopic] ADD  CONSTRAINT [DF_CompanyTeiziTopic_ctt_status]  DEFAULT ((1)) FOR [ctt_status]
GO


