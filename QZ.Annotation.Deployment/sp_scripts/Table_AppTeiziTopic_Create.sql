USE [QZOrgCompanyApp]
GO

/****** Object:  Table [dbo].[AppTeiziTopic]    Script Date: 07/11/2016 17:34:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AppTeiziTopic](
	[att_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[att_u_name] [nvarchar](30) NULL,
	[att_u_uid] [int] NULL,
	[att_content] [nvarchar](500) NULL,
	[att_date] [datetime] NULL,
	[att_u_face] [varchar](100) NULL,
	[att_status] [tinyint] NOT NULL,
	[att_tag] [int] NOT NULL,
 CONSTRAINT [PK_APPTEIZITOPIC] PRIMARY KEY CLUSTERED 
(
	[att_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自动编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppTeiziTopic', @level2type=N'COLUMN',@level2name=N'att_id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppTeiziTopic', @level2type=N'COLUMN',@level2name=N'att_u_name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppTeiziTopic', @level2type=N'COLUMN',@level2name=N'att_u_uid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帖子主题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppTeiziTopic', @level2type=N'COLUMN',@level2name=N'att_content'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发表日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppTeiziTopic', @level2type=N'COLUMN',@level2name=N'att_date'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'头像' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppTeiziTopic', @level2type=N'COLUMN',@level2name=N'att_u_face'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态  0-删除 1-审核通过（默认） 2-审核不通过 3-未审核（预留）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppTeiziTopic', @level2type=N'COLUMN',@level2name=N'att_status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帖子标签，评论0，广告1，吐槽2等' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppTeiziTopic', @level2type=N'COLUMN',@level2name=N'att_tag'
GO

ALTER TABLE [dbo].[AppTeiziTopic] ADD  CONSTRAINT [DF_AppTeiziTopic_att_status]  DEFAULT ((1)) FOR [att_status]
GO

ALTER TABLE [dbo].[AppTeiziTopic] ADD  CONSTRAINT [DF_AppTeiziTopic_att_tag]  DEFAULT ((0)) FOR [att_tag]
GO
