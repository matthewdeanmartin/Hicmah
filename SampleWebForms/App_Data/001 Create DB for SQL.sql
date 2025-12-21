use master
go
DECLARE @dbname NVARCHAR(100)
SET @dbname = 'hicmah_express'
DECLARE @DATADIR NVARCHAR(500)
SET @DATADIR = (SELECT SUBSTRING(physical_name, 1, CHARINDEX(N'master.mdf', LOWER(physical_name)) - 1)
                  FROM master.sys.master_files
                  WHERE database_id = 1 AND file_id = 1)
DECLARE @datafile NVARCHAR(4000)
                  
/*
CREATE DATABASE [hicmah_express] ON  PRIMARY 
( NAME = N'hicmah_express', FILENAME = @File , SIZE = 2048KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'hicmah_express_log', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\hicmah_express_log.ldf' , SIZE = 1024KB , FILEGROWTH = 10%)
*/

declare @sql nvarchar(500)
set @sql = 'CREATE DATABASE [' + @dbname + +']  
        ON (NAME = ' + @dbname + ', 
                FILENAME = ''' + + @DATADIR + @dbname + '.mdf'', SIZE = 1024, FILEGROWTH = 10%) 
        LOG ON (NAME = ''' + @dbname + '_log'', 
                        FILENAME = ''' + @DATADIR + @dbname  + '_log.ldf'', 
                        SIZE = 1024, 
                        FILEGROWTH = 10%)'
PRINT @SQL
EXEC(@sql)
