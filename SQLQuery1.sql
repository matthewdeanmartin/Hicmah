SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


	

CREATE TABLE [dbo].[hicmah_settings_applications](
	[id] [smallint] IDENTITY(1,1) NOT NULL,
	[application]  [nvarchar](50) NULL,
	[version] [nvarchar](50) NULL,
 CONSTRAINT [PK_hicmah_settings_applications] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[hicmah_settings](
	[id] [smallint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](300) NULL, -- schema
	[serialized_value] [nvarchar](max) NULL,
	[version] [nvarchar](50) NULL, --app
	[application] [nvarchar](150) NULL, -- app
	[value_type] [nvarchar](500) NULL,  -- schema
	[culture] [nvarchar](15) NULL, -- schema
 CONSTRAINT [PK_hicmah_settings] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[hicmah_settings_user](
	[id] [smallint] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](250) NULL,
	[is_anonymous] [bit] NULL,
	[name] [nvarchar](300) NULL,
	[serialized_value] [nvarchar](max) NULL,
	[version] [nvarchar](50) NULL,
	[application] [nvarchar](150) NULL,
	[value_type] [nvarchar](500) NULL,
	[culture] [nvarchar](15) NULL,
 CONSTRAINT [PK_hicmah_settings_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[hicmah_settings_schema](
	[id] [smallint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](300) NULL,
	[default_serialized_value] [nvarchar](max) NULL,
	[version] [nvarchar](50) NULL,
	[scope] [nvarchar](15) NULL,
	[application] [nvarchar](150) NULL,
	[value_type] [nvarchar](500) NULL,
	[culture] [nvarchar](15) NULL,
	is_user bit,
	is_anonymous bit,
 CONSTRAINT [PK_hicmah_settings_schema] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
