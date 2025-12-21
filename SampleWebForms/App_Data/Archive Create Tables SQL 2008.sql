USE [hicmah]
GO
/****** Object:  Table [dbo].[hicmah_users]    Script Date: 11/29/2011 17:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hicmah_users](
	[user_id] [smallint] IDENTITY(1,1) NOT NULL,
	[user] [nvarchar](255) NULL,
	[first_use] [smalldatetime] NULL,
	[last_use] [smalldatetime] NULL,
	[current_browser] [smallint] NULL,
	[current_resolution] [smallint] NULL,
 CONSTRAINT [PK_hicmah_users] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[hicmah_user_agents]    Script Date: 11/29/2011 17:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hicmah_user_agents](
	[user_agent_id] [smallint] IDENTITY(0,1) NOT NULL,
	[user_agent] [nvarchar](3800) NULL,
	[date_first_seen] [smalldatetime] NULL,
 CONSTRAINT [PK_hicmah_user_agents] PRIMARY KEY CLUSTERED 
(
	[user_agent_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[hicmah_urls]    Script Date: 11/29/2011 17:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hicmah_urls](
	[url_id] [smallint] IDENTITY(1,1) NOT NULL,
	[url] [nvarchar](2000) NULL,
	[host_name] [nvarchar](150) NULL,
	[port] [int] NULL,
	[absolute_path] [nvarchar](1000) NULL,
	[query] [nvarchar](1000) NULL,
 CONSTRAINT [PK_hicmah_urls] PRIMARY KEY CLUSTERED 
(
	[url_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[hicmah_resolutions]    Script Date: 11/29/2011 17:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[hicmah_resolutions](
	[resolution_id] [smallint] IDENTITY(1,1) NOT NULL,
	[resolution] [varchar](50) NULL,
	[height] [smallint] NULL,
	[width] [smallint] NULL,
 CONSTRAINT [PK_himah_resolutions] PRIMARY KEY CLUSTERED 
(
	[resolution_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[hicmah_request_type]    Script Date: 11/29/2011 17:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[hicmah_request_type](
	[request_type_id] [tinyint] NOT NULL,
	[request_type] [varchar](100) NULL,
 CONSTRAINT [PK_hicmah_request_type] PRIMARY KEY CLUSTERED 
(
	[request_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[hicmah_queries]    Script Date: 11/29/2011 17:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hicmah_queries](
	[query_id] [int] NOT NULL,
	[url_id] [smallint] NULL,
	[query] [nvarchar](1000) NULL,
 CONSTRAINT [PK_hicmah_queries] PRIMARY KEY CLUSTERED 
(
	[query_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[hicmah_invoker]    Script Date: 11/29/2011 17:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[hicmah_invoker](
	[invoker_id] [tinyint] NOT NULL,
	[invoker] [varchar](100) NULL,
 CONSTRAINT [PK_hicmah_invoker] PRIMARY KEY CLUSTERED 
(
	[invoker_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[hicmah_hits]    Script Date: 11/29/2011 17:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hicmah_hits](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[full_url_id] [smallint] NULL,
	[referrer_id] [smallint] NULL,
	[user_agent_id] [smallint] NULL,
	[invoker_id] [tinyint] NULL,
	[user_name_id] [smallint] NULL,
	[hit_date] [smalldatetime] NULL,
	[request_type_id] [tinyint] NULL,
	[server_duration] [int] NULL,
	[client_duration] [int] NULL,
	[client_bytes] [int] NULL,
	[status_code] [smallint] NULL,
 CONSTRAINT [PK_hicmah_hits] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[hicmah_hit_data]    Script Date: 11/29/2011 17:26:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[hicmah_hit_data]
AS

 SELECT     hicmah_hits.id, hicmah_users.[user], hicmah_user_agents.user_agent, 
 
 
 hicmah_urls.url AS full_url,
  hicmah_urls.absolute_path,
  hicmah_urls_1.url AS referrer_url, 
                      hicmah_request_type_1.request_type, hicmah_invoker.invoker, hicmah_hits.hit_date, hicmah_hits.server_duration, hicmah_hits.client_duration, 
                      hicmah_hits.client_bytes, hicmah_hits.status_code
FROM         hicmah_hits LEFT OUTER JOIN
                      hicmah_invoker ON hicmah_hits.invoker_id = hicmah_invoker.invoker_id LEFT OUTER JOIN
                      hicmah_invoker AS hicmah_invoker_1 ON hicmah_hits.invoker_id = hicmah_invoker_1.invoker_id LEFT OUTER JOIN
                      hicmah_urls ON hicmah_hits.full_url_id = hicmah_urls.url_id LEFT OUTER JOIN
                      hicmah_request_type AS hicmah_request_type_1 ON hicmah_hits.request_type_id = hicmah_request_type_1.request_type_id LEFT OUTER JOIN
                      hicmah_user_agents ON hicmah_hits.user_agent_id = hicmah_user_agents.user_agent_id LEFT OUTER JOIN
                      hicmah_users ON hicmah_hits.user_name_id = hicmah_users.user_id LEFT OUTER JOIN
                      hicmah_urls AS hicmah_urls_1 ON hicmah_hits.referrer_id = hicmah_urls_1.url_id
GO
