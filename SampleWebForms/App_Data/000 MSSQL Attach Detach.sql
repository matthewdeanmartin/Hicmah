/*
A down side to Attach/Detach is you will be bitten by a high version DB that can't be attached to a low version DB.
*/
USE [master]
GO
CREATE DATABASE [hicmah] ON 
( FILENAME = N'C:\Users\mmartin\Dropbox\code\Hicmah\SampleWebForms\App_Data\aspnetdb.mdf' ),
( FILENAME = N'C:\Users\mmartin\Dropbox\code\Hicmah\SampleWebForms\App_Data\aspnetdb_log.ldf' )
 FOR ATTACH
GO



USE [master]
GO
ALTER DATABASE [hicmah] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
USE [master]
GO
EXEC master.dbo.sp_detach_db @dbname = N'hicmah', @skipchecks = 'false'
GO