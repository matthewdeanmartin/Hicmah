-- Works in 3.5
-- TODO: Test in 4.0

CREATE TABLE [hicmah_users](
	[user_id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[user] [nvarchar](255) NULL,
	[first_use] [datetime] NULL,
	[last_use] [datetime] NULL,
	[current_browser] [smallint] NULL,
	[current_resolution] [smallint] NULL
)
GO
CREATE TABLE [hicmah_user_agents](
	[user_agent_id] [int] IDENTITY(0,1) NOT NULL PRIMARY KEY,
	[user_agent] [nvarchar](3800) NULL,
	[date_first_seen] [datetime] NULL
)
GO
CREATE TABLE [hicmah_urls](
	[url_id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[url] [nvarchar](2000) NULL,
	[host_name] [nvarchar](150) NULL,
	[port] [int] NULL,
	[absolute_path] [nvarchar](1000) NULL,
	[query] [nvarchar](1000) NULL
)
GO
CREATE TABLE [hicmah_resolutions](
	[resolution_id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[resolution] [nvarchar](50) NULL,
	[height] [smallint] NULL,
	[width] [smallint] NULL
)
GO
CREATE TABLE [hicmah_request_type](
	[request_type_id] [tinyint] NOT NULL PRIMARY KEY,
	[request_type] [nvarchar](100) NULL
)
GO
CREATE TABLE [hicmah_queries](
	[query_id] [int] NOT NULL PRIMARY KEY,
	[url_id] [smallint] NULL,
	[query] [nvarchar](1000) NULL
)
GO
CREATE TABLE [hicmah_invoker](
	[invoker_id] [tinyint] NOT NULL PRIMARY KEY ,
	[invoker] [nvarchar](100) NULL
)
GO
CREATE TABLE [hicmah_hits](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[full_url_id] [smallint] NULL,
	[referrer_id] [smallint] NULL,
	[user_agent_id] [smallint] NULL,
	[invoker_id] [tinyint] NULL,
	[user_name_id] [smallint] NULL,
	[hit_date] [datetime] NULL,
	[request_type_id] [tinyint] NULL,
	[server_duration] [int] NULL,
	[client_duration] [int] NULL,
	[client_bytes] [int] NULL,
	[status_code] [smallint] NULL
) 
GO