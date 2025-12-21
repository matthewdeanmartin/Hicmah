// Copyright (c) 2011 Matthew D Martin
// MIT or MS-PL License as you elect, please see root directory for text of licenses.
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DatabaseSchemaReader.DataSchema;
using DatabaseSchemaReader.SqlGen;
using Dataglot;
using Hicmah.Misc;

namespace Hicmah.Administration
{
    public class DatabaseAdministrationForSql : DatabaseAdministration
    {
        private readonly TraceSourceUtil trace = new TraceSourceUtil("DatabaseAdministrationForSql");

        public override void CreateTables()
        {
            GenericCommand composer = new GenericCommand(ConfigUtils.SpecificFactory(DbBrand.MsSql2005));
            string tablePrefix = composer.Compose("{$ns}");

            if(ConfigUtils.ConnectionString().Contains("=master"))
            {
                throw new InvalidOperationException("Can't create db in master");
            }
            DatabaseSchema schemaNew = new DatabaseSchema(ConfigUtils.ConnectionString(), "System.Data.SqlClient");

            
                DatabaseTable dt = schemaNew.AddTable("hicmah_cache");

                DatabaseColumn dc = new DatabaseColumn();
                dc.Name = "cache_id";
                dc.DbDataType = "int";
                dc.IsIdentity = true;
                dc.IsPrimaryKey = true;
                dc.IsUniqueKey = false;
                dc.IsIndexed = false;
                dc.Ordinal = 1;
                dc.Nullable = false;
                DatabaseConstraint constraint = new DatabaseConstraint();
                constraint.ConstraintType = ConstraintType.PrimaryKey;
                constraint.Columns.Add("cache_id");
            constraint.Name = "PK_hicmah_cache";
                dt.PrimaryKey = constraint;
                dt.AddColumn(dc);

                dc = new DatabaseColumn();
                dc.Name = "parameters";
                dc.DbDataType = "nvarchar";
                dc.Length = 1000;
                dc.IsIdentity = false;
                dc.IsPrimaryKey = false;
                dc.IsUniqueKey = false;
                dc.IsIndexed = false;
                dc.Ordinal = 2;
                dc.Nullable = true;
                dt.AddColumn(dc);

                dc = new DatabaseColumn();
                dc.Name = "cachedResults";
                dc.DbDataType = "nvarchar";
                dc.Length = -1;
                dc.IsIdentity = false;
                dc.IsPrimaryKey = false;
                dc.IsUniqueKey = false;
                dc.IsIndexed = false;
                dc.Ordinal = 3;
                dc.Nullable = true;
                dt.AddColumn(dc);

                dc = new DatabaseColumn();
                dc.Name = "expirationDate";
                dc.DbDataType = "datetime";
                dc.IsIdentity = false;
                dc.IsPrimaryKey = false;
                dc.IsUniqueKey = false;
                dc.IsIndexed = false;
                dc.Ordinal = 4;
                dc.Nullable = true;
                dt.AddColumn(dc);

                dc = new DatabaseColumn();
                dc.Name = "queryName";
                dc.DbDataType = "varchar";
                dc.Length = 100;
                dc.IsIdentity = false;
                dc.IsPrimaryKey = false;
                dc.IsUniqueKey = false;
                dc.IsIndexed = false;
                dc.Ordinal = 5;
                dc.Nullable = true;
                dt.AddColumn(dc);

                dc = new DatabaseColumn();
                dc.Name = "insertDate";
                dc.DbDataType = "datetime";
                dc.IsIdentity = false;
                dc.IsPrimaryKey = false;
                dc.IsUniqueKey = false;
                dc.IsIndexed = false;
                dc.Ordinal = 6;
                dc.Nullable = true;
                dt.AddColumn(dc);

                DatabaseIndex newIndex = new DatabaseIndex();
                newIndex.Name = "PK_hicmah_cache";
                newIndex.IsUnique = false;
                newIndex.IndexType = "CLUSTERED";
                DatabaseColumn newIndexedColumn = new DatabaseColumn();
                newIndexedColumn.Name = "cache_id";
                newIndex.Columns.Add(newIndexedColumn);
                dt.Indexes.Add(newIndex);
            
             dt = schemaNew.AddTable("hicmah_dates");



            dc = new DatabaseColumn();
            dc.Name = "date_id";
            dc.DbDataType = "int";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = true;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 1;
            dc.Nullable = false;
            constraint = new DatabaseConstraint();
            constraint.ConstraintType = ConstraintType.PrimaryKey;
            constraint.Name = "Pk_hicmah_dates";
            constraint.Columns.Add("date_id");
            
            dt.PrimaryKey = constraint;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "ticks";
            dc.DbDataType = "bigint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 2;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "date_key";
            dc.DbDataType = "smalldatetime";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 3;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "year";
            dc.DbDataType = "smallint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 4;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "month";
            dc.DbDataType = "tinyint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 5;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "day";
            dc.DbDataType = "tinyint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 6;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "first_of_month";
            dc.DbDataType = "smalldatetime";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 7;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "fiscal_year";
            dc.DbDataType = "smallint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 8;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "fiscal_quarter";
            dc.DbDataType = "tinyint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 9;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "calendar_quarter";
            dc.DbDataType = "tinyint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 10;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "week_number";
            dc.DbDataType = "tinyint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 11;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "workday_weekend";
            dc.DbDataType = "tinyint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 12;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "holiday";
            dc.DbDataType = "bit";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 13;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "short_month";
            dc.DbDataType = "nvarchar";
            dc.Length = 50;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 14;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "long_month";
            dc.DbDataType = "nvarchar";
            dc.Length = 50;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 15;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "short_year_month";
            dc.DbDataType = "nvarchar";
            dc.Length = 50;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 16;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "long_year_month";
            dc.DbDataType = "nvarchar";
            dc.Length = 50;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 17;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "short_day";
            dc.DbDataType = "nvarchar";
            dc.Length = 50;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 18;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "long_day";
            dc.DbDataType = "nvarchar";
            dc.Length = 50;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 19;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "day_of_week";
            dc.DbDataType = "nvarchar";
            dc.Length = 50;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 20;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "long_date";
            dc.DbDataType = "nvarchar";
            dc.Length = 150;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 21;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "sortable_date";
            dc.DbDataType = "nvarchar";
            dc.Length = 100;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 22;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "usortable_date";
            dc.DbDataType = "nvarchar";
            dc.Length = 100;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 23;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "rfc1123_date";
            dc.DbDataType = "nvarchar";
            dc.Length = 100;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 24;
            dc.Nullable = true;
            dt.AddColumn(dc);

            newIndex = new DatabaseIndex();
            newIndex.Name = "PK_hicmah_dates_date_id";
            newIndex.IsUnique = false;
            newIndex.IndexType = "CLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "date_id";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            dt = schemaNew.AddTable("hicmah_hits");

            dc = new DatabaseColumn();
            dc.Name = "id";
            dc.DbDataType = "int";
            dc.IsIdentity = true;
            dc.IsPrimaryKey = true;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 1;
            dc.Nullable = false;
            constraint = new DatabaseConstraint();
            constraint.ConstraintType = ConstraintType.PrimaryKey;
            constraint.Columns.Add("id");
            constraint.Name = "Pk_hicmah_hits";
            dt.PrimaryKey = constraint;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "hit_date";
            dc.DbDataType = "smalldatetime";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 2;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "utc_date";
            dc.DbDataType = "smalldatetime";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 3;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "date_id";
            dc.DbDataType = "int";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 4;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "time_id";
            dc.DbDataType = "smallint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 5;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "client_date_id";
            dc.DbDataType = "int";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 6;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "client_time_id";
            dc.DbDataType = "int";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 7;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "time_zone_id";
            dc.DbDataType = "smallint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 8;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "full_url_id";
            dc.DbDataType = "smallint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 9;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "referrer_id";
            dc.DbDataType = "smallint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 10;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "user_agent_id";
            dc.DbDataType = "smallint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 11;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "invoker_id";
            dc.DbDataType = "tinyint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 12;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "user_name_id";
            dc.DbDataType = "smallint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 13;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "request_type_id";
            dc.DbDataType = "tinyint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 14;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "server_duration";
            dc.DbDataType = "int";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 15;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "client_duration";
            dc.DbDataType = "int";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 16;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "client_bytes";
            dc.DbDataType = "int";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 17;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "status_code";
            dc.DbDataType = "smallint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 18;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "seconds_on_page";
            dc.DbDataType = "smallint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 19;
            dc.Nullable = true;
            dt.AddColumn(dc);

            newIndex = new DatabaseIndex();
            newIndex.Name = "missing_index_hicmah_hits_date_full_url";
            newIndex.IsUnique = false;
            newIndex.IndexType = "NONCLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "full_url_id";
            newIndex.Columns.Add(newIndexedColumn);
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "hit_date";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            newIndex = new DatabaseIndex();
            newIndex.Name = "missing_index_hicmah_hits_Date_invoker";
            newIndex.IsUnique = false;
            newIndex.IndexType = "NONCLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "invoker_id";
            newIndex.Columns.Add(newIndexedColumn);
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "hit_date";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            newIndex = new DatabaseIndex();
            newIndex.Name = "missing_index_hicmah_hits_Date_request_type";
            newIndex.IsUnique = false;
            newIndex.IndexType = "NONCLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "request_type_id";
            newIndex.Columns.Add(newIndexedColumn);
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "hit_date";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            newIndex = new DatabaseIndex();
            newIndex.Name = "missing_index_hicmah_hits_Date_user_agent";
            newIndex.IsUnique = false;
            newIndex.IndexType = "NONCLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "user_agent_id";
            newIndex.Columns.Add(newIndexedColumn);
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "hit_date";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            newIndex = new DatabaseIndex();
            newIndex.Name = "missing_index_hicmah_hits_date_user_name";
            newIndex.IsUnique = false;
            newIndex.IndexType = "NONCLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "user_name_id";
            newIndex.Columns.Add(newIndexedColumn);
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "hit_date";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            newIndex = new DatabaseIndex();
            newIndex.Name = "missing_index_hicmah_hits_status_code_user";
            newIndex.IsUnique = false;
            newIndex.IndexType = "NONCLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "user_name_id";
            newIndex.Columns.Add(newIndexedColumn);
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "status_code";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            newIndex = new DatabaseIndex();
            newIndex.Name = "NDX_hicmah_hits_date_full_url";
            newIndex.IsUnique = false;
            newIndex.IndexType = "NONCLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "full_url_id";
            newIndex.Columns.Add(newIndexedColumn);
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "hit_date";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            newIndex = new DatabaseIndex();
            newIndex.Name = "NDX_hicmah_hits_Date_invoker";
            newIndex.IsUnique = false;
            newIndex.IndexType = "NONCLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "invoker_id";
            newIndex.Columns.Add(newIndexedColumn);
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "hit_date";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            newIndex = new DatabaseIndex();
            newIndex.Name = "NDX_hicmah_hits_Date_request_type";
            newIndex.IsUnique = false;
            newIndex.IndexType = "NONCLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "request_type_id";
            newIndex.Columns.Add(newIndexedColumn);
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "hit_date";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            newIndex = new DatabaseIndex();
            newIndex.Name = "NDX_hicmah_hits_date_status_code";
            newIndex.IsUnique = false;
            newIndex.IndexType = "NONCLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "full_url_id";
            newIndex.Columns.Add(newIndexedColumn);
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "hit_date";
            newIndex.Columns.Add(newIndexedColumn);
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "status_code";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            newIndex = new DatabaseIndex();
            newIndex.Name = "NDX_hicmah_hits_Date_user_agent";
            newIndex.IsUnique = false;
            newIndex.IndexType = "NONCLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "user_agent_id";
            newIndex.Columns.Add(newIndexedColumn);
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "hit_date";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            newIndex = new DatabaseIndex();
            newIndex.Name = "NDX_hicmah_hits_date_user_name";
            newIndex.IsUnique = false;
            newIndex.IndexType = "NONCLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "user_name_id";
            newIndex.Columns.Add(newIndexedColumn);
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "hit_date";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            newIndex = new DatabaseIndex();
            newIndex.Name = "NDX_hicmah_hits_full_url_then_date";
            newIndex.IsUnique = false;
            newIndex.IndexType = "NONCLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "status_code";
            newIndex.Columns.Add(newIndexedColumn);
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "full_url_id";
            newIndex.Columns.Add(newIndexedColumn);
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "hit_date";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            newIndex = new DatabaseIndex();
            newIndex.Name = "NDX_hicmah_hits_status_code_user";
            newIndex.IsUnique = false;
            newIndex.IndexType = "NONCLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "user_name_id";
            newIndex.Columns.Add(newIndexedColumn);
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "status_code";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            newIndex = new DatabaseIndex();
            newIndex.Name = "PK_hicmah_hits";
            newIndex.IsUnique = false;
            newIndex.IndexType = "CLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "id";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            dt = schemaNew.AddTable("hicmah_invoker");

            dc = new DatabaseColumn();
            dc.Name = "invoker_id";
            dc.DbDataType = "tinyint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = true;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 1;
            dc.Nullable = false;
            constraint = new DatabaseConstraint();
            constraint.ConstraintType = ConstraintType.PrimaryKey;
            constraint.Columns.Add("invoker_id");
            constraint.Name = "Pk_hicmah_invoker";
            dt.PrimaryKey = constraint;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "invoker";
            dc.DbDataType = "varchar";
            dc.Length = 100;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 2;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "description";
            dc.DbDataType = "nvarchar";
            dc.Length = 200;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 3;
            dc.Nullable = true;
            dt.AddColumn(dc);

            newIndex = new DatabaseIndex();
            newIndex.Name = "PK_hicmah_invoker";
            newIndex.IsUnique = false;
            newIndex.IndexType = "CLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "invoker_id";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            dt = schemaNew.AddTable("hicmah_queries");

            dc = new DatabaseColumn();
            dc.Name = "query_id";
            dc.DbDataType = "int";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = true;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 1;
            dc.Nullable = false;
            constraint = new DatabaseConstraint();
            constraint.ConstraintType = ConstraintType.PrimaryKey;
            constraint.Columns.Add("query_id");
            constraint.Name = "Pk_hicmah_queries";
            dt.PrimaryKey = constraint;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "url_id";
            dc.DbDataType = "smallint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 2;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "query";
            dc.DbDataType = "nvarchar";
            dc.Length = 1000;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 3;
            dc.Nullable = true;
            dt.AddColumn(dc);

            newIndex = new DatabaseIndex();
            newIndex.Name = "PK_hicmah_queries";
            newIndex.IsUnique = false;
            newIndex.IndexType = "CLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "query_id";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            dt = schemaNew.AddTable("hicmah_request_types");

            dc = new DatabaseColumn();
            dc.Name = "request_type_id";
            dc.DbDataType = "tinyint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = true;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 1;
            dc.Nullable = false;
            constraint = new DatabaseConstraint();
            constraint.ConstraintType = ConstraintType.PrimaryKey;
            constraint.Columns.Add("request_type_id");
            constraint.Name = "Pk_hicmah_request_Types";
            dt.PrimaryKey = constraint;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "request_type";
            dc.DbDataType = "varchar";
            dc.Length = 100;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 2;
            dc.Nullable = true;
            dt.AddColumn(dc);

            //dc = new DatabaseColumn();
            //dc.Name = "request_type";
            //dc.DbDataType = "nvarchar";
            //dc.Length = 200;
            //dc.IsIdentity = false;
            //dc.IsPrimaryKey = false;
            //dc.IsUniqueKey = false;
            //dc.IsIndexed = false;
            //dc.Ordinal = 3;
            //dc.Nullable = true;
            //dt.AddColumn(dc);

            newIndex = new DatabaseIndex();
            newIndex.Name = "PK_hicmah_request_type";
            newIndex.IsUnique = false;
            newIndex.IndexType = "CLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "request_type_id";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            dt = schemaNew.AddTable("hicmah_servers");

            dc = new DatabaseColumn();
            dc.Name = "server_id";
            dc.DbDataType = "int";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = true;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 1;
            dc.Nullable = false;
            constraint = new DatabaseConstraint();
            constraint.ConstraintType = ConstraintType.PrimaryKey;
            constraint.Columns.Add("server_id");
            constraint.Name = "Pk_hicmah_servers";
            dt.PrimaryKey = constraint;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "server_name";
            dc.DbDataType = "nchar";
            dc.Length = 10;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 2;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "os_name";
            dc.DbDataType = "nvarchar";
            dc.Length = 100;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 3;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "os_platform";
            dc.DbDataType = "nvarchar";
            dc.Length = 100;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 4;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "os_version";
            dc.DbDataType = "nvarchar";
            dc.Length = 100;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 5;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "os_env_version";
            dc.DbDataType = "nvarchar";
            dc.Length = 150;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 6;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "server_cpu_count";
            dc.DbDataType = "smallint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 7;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "server_culture";
            dc.DbDataType = "nvarchar";
            dc.Length = 20;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 8;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "memory_bytes";
            dc.DbDataType = "int";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 9;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "app_version_by_file_date";
            dc.DbDataType = "datetime";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 10;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "app_version_by_file_size";
            dc.DbDataType = "int";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 11;
            dc.Nullable = true;
            dt.AddColumn(dc);

            newIndex = new DatabaseIndex();
            newIndex.Name = "PK_hicmah_servers";
            newIndex.IsUnique = false;
            newIndex.IndexType = "CLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "server_id";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            dt = schemaNew.AddTable("hicmah_times");

            dc = new DatabaseColumn();
            dc.Name = "time_id";
            dc.DbDataType = "smallint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = true;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 1;
            dc.Nullable = false;
            constraint = new DatabaseConstraint();
            constraint.ConstraintType = ConstraintType.PrimaryKey;
            constraint.Columns.Add("time_id");
            constraint.Name = "Pk_hicmah_times";
            dt.PrimaryKey = constraint;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "twenty_four_hour";
            dc.DbDataType = "tinyint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 2;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "hour";
            dc.DbDataType = "tinyint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 3;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "minute";
            dc.DbDataType = "tinyint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 4;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "AM_PM";
            dc.DbDataType = "nvarchar";
            dc.Length = 5;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 5;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "short_time";
            dc.DbDataType = "nvarchar";
            dc.Length = 25;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 6;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "long_time";
            dc.DbDataType = "nvarchar";
            dc.Length = 25;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 7;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "three_shift";
            dc.DbDataType = "tinyint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 8;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "is_business_hours";
            dc.DbDataType = "bit";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 9;
            dc.Nullable = true;
            dt.AddColumn(dc);

            newIndex = new DatabaseIndex();
            newIndex.Name = "PK_hicmah_times";
            newIndex.IsUnique = false;
            newIndex.IndexType = "CLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "time_id";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            dt = schemaNew.AddTable("hicmah_time_zones");

            dc = new DatabaseColumn();
            dc.Name = "time_zone_id";
            dc.DbDataType = "int";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = true;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 1;
            dc.Nullable = false;
            constraint = new DatabaseConstraint();
            constraint.ConstraintType = ConstraintType.PrimaryKey;
            constraint.Columns.Add("time_zone_id");
            constraint.Name = "Pk_hicmah_time_zones";
            dt.PrimaryKey = constraint;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "olson_zone";
            dc.DbDataType = "nvarchar";
            dc.Length = 150;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 2;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "windows_zone";
            dc.DbDataType = "nvarchar";
            dc.Length = 150;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 3;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "offset";
            dc.DbDataType = "nchar";
            dc.Length = 10;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 4;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "uses_daylight_savings";
            dc.DbDataType = "bit";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 5;
            dc.Nullable = true;
            dt.AddColumn(dc);

            newIndex = new DatabaseIndex();
            newIndex.Name = "PK_himah_time_zones";
            newIndex.IsUnique = false;
            newIndex.IndexType = "CLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "time_zone_id";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            dt = schemaNew.AddTable("hicmah_urls");

            dc = new DatabaseColumn();
            dc.Name = "url_id";
            dc.DbDataType = "smallint";
            dc.IsIdentity = true;
            dc.IsPrimaryKey = true;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 1;
            dc.Nullable = false;
            constraint = new DatabaseConstraint();
            constraint.ConstraintType = ConstraintType.PrimaryKey;
            constraint.Columns.Add("url_id");
            constraint.Name = "Pk_hicmah_urls";
            dt.PrimaryKey = constraint;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "url";
            dc.DbDataType = "nvarchar";
            dc.Length = 2000;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 2;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "host_name";
            dc.DbDataType = "nvarchar";
            dc.Length = 150;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 3;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "port";
            dc.DbDataType = "int";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 4;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "absolute_path";
            dc.DbDataType = "nvarchar";
            dc.Length = 1000;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 5;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "query";
            dc.DbDataType = "nvarchar";
            dc.Length = 1000;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 6;
            dc.Nullable = true;
            dt.AddColumn(dc);

            newIndex = new DatabaseIndex();
            newIndex.Name = "PK_hicmah_urls";
            newIndex.IsUnique = false;
            newIndex.IndexType = "CLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "url_id";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            dt = schemaNew.AddTable("hicmah_users");

            dc = new DatabaseColumn();
            dc.Name = "user_id";
            dc.DbDataType = "smallint";
            dc.IsIdentity = true;
            dc.IsPrimaryKey = true;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 1;
            dc.Nullable = false;
            constraint = new DatabaseConstraint();
            constraint.ConstraintType = ConstraintType.PrimaryKey;
            constraint.Columns.Add("user_id");
            constraint.Name = "Pk_hicmah_users";
            dt.PrimaryKey = constraint;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "user";
            dc.DbDataType = "nvarchar";
            dc.Length = 255;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 2;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "first_use";
            dc.DbDataType = "smalldatetime";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 3;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "last_use";
            dc.DbDataType = "smalldatetime";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 4;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "current_browser";
            dc.DbDataType = "smallint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 5;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "current_resolution";
            dc.DbDataType = "smallint";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 6;
            dc.Nullable = true;
            dt.AddColumn(dc);

            newIndex = new DatabaseIndex();
            newIndex.Name = "PK_hicmah_users";
            newIndex.IsUnique = false;
            newIndex.IndexType = "CLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "user_id";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);

            dt = schemaNew.AddTable("hicmah_user_agents");

            dc = new DatabaseColumn();
            dc.Name = "user_agent_id";
            dc.DbDataType = "smallint";
            dc.IsIdentity = true;
            dc.IsPrimaryKey = true;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 1;
            dc.Nullable = false;
            constraint = new DatabaseConstraint();
            constraint.ConstraintType = ConstraintType.PrimaryKey;
            constraint.Columns.Add("user_agent_id");
            constraint.Name = "Pk_hicmah_user_agents";
            dt.PrimaryKey = constraint;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "user_agent";
            dc.DbDataType = "nvarchar";
            dc.Length = 3800;
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 2;
            dc.Nullable = true;
            dt.AddColumn(dc);

            dc = new DatabaseColumn();
            dc.Name = "date_first_seen";
            dc.DbDataType = "smalldatetime";
            dc.IsIdentity = false;
            dc.IsPrimaryKey = false;
            dc.IsUniqueKey = false;
            dc.IsIndexed = false;
            dc.Ordinal = 3;
            dc.Nullable = true;
            dt.AddColumn(dc);

            newIndex = new DatabaseIndex();
            newIndex.Name = "PK_hicmah_user_agents";
            newIndex.IsUnique = false;
            newIndex.IndexType = "CLUSTERED";
            newIndexedColumn = new DatabaseColumn();
            newIndexedColumn.Name = "user_agent_id";
            newIndex.Columns.Add(newIndexedColumn);
            dt.Indexes.Add(newIndex);



            DdlGeneratorFactory gen = new DdlGeneratorFactory(SqlType.SqlServer);
            ITablesGenerator tablesGenerator = gen.AllTablesGenerator(schemaNew);
            tablesGenerator.IncludeSchema = false;
            string result2 = tablesGenerator.Write();

            ParameterlessCommand com = new ParameterlessCommand(ConfigUtils.CurrentDataFactory());
            Debug.WriteLine(result2);
            com.Execute(result2);
            CreateView();
        }

        public override void SeedData()
        {
            const string sql = @"
INSERT [dbo].[hicmah_request_types] ([request_type_id], [request_type]) VALUES (1, N'GET')
INSERT [dbo].[hicmah_request_types] ([request_type_id], [request_type]) VALUES (2, N'POST')
INSERT [dbo].[hicmah_request_types] ([request_type_id], [request_type]) VALUES (3, N'HEAD')
INSERT [dbo].[hicmah_request_types] ([request_type_id], [request_type]) VALUES (4, N'PUT')
INSERT [dbo].[hicmah_request_types] ([request_type_id], [request_type]) VALUES (5, N'DELETE')
INSERT [dbo].[hicmah_request_types] ([request_type_id], [request_type]) VALUES (6, N'TRACE')
INSERT [dbo].[hicmah_request_types] ([request_type_id], [request_type]) VALUES (7, N'OPTIONS')
INSERT [dbo].[hicmah_request_types] ([request_type_id], [request_type]) VALUES (8, N'CONNECT')
INSERT [dbo].[hicmah_request_types] ([request_type_id], [request_type]) VALUES (9, N'PATH')
";

            using (ParameterlessCommand com = new ParameterlessCommand(ConfigUtils.CurrentDataFactory()))
            {
                com.Execute(sql);
            }
        }

        private void CreateView()
        {
            string sql =
                @"CREATE VIEW [dbo].[hicmah_hit_data]
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
                      hicmah_request_types AS hicmah_request_type_1 ON hicmah_hits.request_type_id = hicmah_request_type_1.request_type_id LEFT OUTER JOIN
                      hicmah_user_agents ON hicmah_hits.user_agent_id = hicmah_user_agents.user_agent_id LEFT OUTER JOIN
                      hicmah_users ON hicmah_hits.user_name_id = hicmah_users.user_id LEFT OUTER JOIN
                      hicmah_urls AS hicmah_urls_1 ON hicmah_hits.referrer_id = hicmah_urls_1.url_id";

            using (ParameterlessCommand com = new ParameterlessCommand(ConfigUtils.CurrentDataFactory()))
            {
                com.Execute(sql);
            }
        }


        public override void ProcessAllDiminsions()
        {
            throw new NotImplementedException();
        }

        public override void ClearData()
        {
            const string sql =
           @"
truncate table {$ns}_hits;
truncate table {$ns}_urls;
truncate table {$ns}_user_agents;
truncate table {$ns}_users;
truncate table {$ns}_dates;
DBCC CHECKIDENT('{$ns}_hits', RESEED, 0);
DBCC CHECKIDENT('dbo.{$ns}_urls', RESEED, 0);
DBCC CHECKIDENT('dbo.{$ns}_user_agents', RESEED, 0);
DBCC CHECKIDENT('dbo.{$ns}_users', RESEED, 0);

SET IDENTITY_INSERT dbo.{$ns}_user_agents ON ;
INSERT INTO dbo.{$ns}_user_agents (
	user_agent_id,
	user_agent
) VALUES ( 
	0,
	 N'None provided') ;
SET IDENTITY_INSERT dbo.{$ns}_user_agents OFF;

SET IDENTITY_INSERT dbo.{$ns}_urls ON ;
INSERT INTO dbo.{$ns}_urls (
	url_id,
	url
) VALUES ( 
	0,
	 N'None provided') ;
SET IDENTITY_INSERT dbo.{$ns}_urls OFF;

SET IDENTITY_INSERT dbo.{$ns}_users ON ;
INSERT INTO dbo.{$ns}_users (
	[user_id],
	[user]
) VALUES ( 
	0,
	 N'None provided') ;
SET IDENTITY_INSERT dbo.{$ns}_users OFF;
";
            using (ParameterlessCommand com = new ParameterlessCommand(ConfigUtils.CurrentDataFactory()))
            {
                com.Execute(sql);
            }
        }

        public override void ClearCache()
        {
            string sql = @"TRUNCATE TABLE {$ns}_cache";
            using (ParameterlessCommand com = new ParameterlessCommand(ConfigUtils.CurrentDataFactory()))
            {
                trace.WriteLine("Clearing with command "+ sql);
                com.Execute(sql);
            }
        }

        
        public override void ArchiveData()
        {
            throw new NotImplementedException();
        }

        public override void DropIndexes()
        {
            string[] sql = {
@"DROP INDEX [NDX_{$ns}_hits_date_status_code] ON [{$ns}_hits] ",
@"DROP INDEX [NDX_{$ns}_hits_full_url_then_date] ON [{$ns}_hits] ",
@"DROP INDEX [NDX_{$ns}_hits_date_full_url] ON [{$ns}_hits] ",
@"DROP INDEX [NDX_{$ns}_hits_status_code_user] ON [{$ns}_hits] ",
@"DROP INDEX [NDX_{$ns}_hits_Date_invoker] ON [{$ns}_hits] ",
@"DROP INDEX [NDX_{$ns}_hits_Date_request_type] ON [{$ns}_hits]", 
@"DROP INDEX [NDX_{$ns}_hits_Date_user_agent] ON [{$ns}_hits]", 
@"DROP INDEX [NDX_{$ns}_hits_date_user_name] ON [{$ns}_hits]", 
@"DROP INDEX [NDX_{$ns}_hits_status_code_user] ON [{$ns}_hits] "
        };
            
            using (ParameterlessCommand com = new ParameterlessCommand(ConfigUtils.CurrentDataFactory()))
            {
                foreach (string s in sql)
                {
                    try
                    {
                        trace.WriteLine("Dropping index with command : " + s);
                        com.Execute(s);
                    }
                    catch (SqlException ex)
                    {
                        if(ex.Message.Contains("Cannot drop the index"))
                        {
                            trace.WriteLine("That index can't be dropped, didn't exist");
                                //Oh well
                        }
                        else
                            throw;
                    }
                  
                }
                
            }
        }

        public override void CreateIndexes()
        {
            string[] sql = { @"

CREATE NONCLUSTERED INDEX [NDX_{$ns}_hits_date_status_code] ON [dbo].[{$ns}_hits] 
(
	[hit_date] ASC,
	[status_code] ASC
)
INCLUDE ( [full_url_id]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON);
;",
@"CREATE NONCLUSTERED INDEX [NDX_{$ns}_hits_full_url_then_date] ON [dbo].[{$ns}_hits] 
(
	[full_url_id] ASC,
	[hit_date] ASC
)
INCLUDE ( [status_code]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
;",
@"CREATE NONCLUSTERED INDEX [NDX_{$ns}_hits_date_full_url] ON [dbo].[{$ns}_hits] 
(
	[hit_date] ASC
)
INCLUDE ( [full_url_id]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
;",
@"CREATE NONCLUSTERED INDEX [NDX_{$ns}_hits_Date_invoker] ON [dbo].[{$ns}_hits] 
(
	[hit_date] ASC
)
INCLUDE ( [invoker_id]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) 
;",
@"CREATE NONCLUSTERED INDEX [NDX_{$ns}_hits_Date_request_type] ON [dbo].[{$ns}_hits] 
(
	[hit_date] ASC
)
INCLUDE ( [request_type_id]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) 
;",
@"CREATE NONCLUSTERED INDEX [NDX_{$ns}_hits_Date_user_agent] ON [dbo].[{$ns}_hits] 
(
	[hit_date] ASC
)
INCLUDE ( [user_agent_id]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) 
;",
@"CREATE NONCLUSTERED INDEX [NDX_{$ns}_hits_date_user_name] ON [dbo].[{$ns}_hits] 
(
	[hit_date] ASC
)
INCLUDE ( [user_name_id]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) 
;",
@"CREATE NONCLUSTERED INDEX [NDX_{$ns}_hits_status_code_user] ON [dbo].[{$ns}_hits] 
(
	[status_code] ASC
)
INCLUDE ( [user_name_id]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) 
;

"};
            using (ParameterlessCommand com = new ParameterlessCommand(ConfigUtils.CurrentDataFactory()))
            {
                foreach (string s in sql)
                {
                    try
                    {
                        trace.WriteLine("Creating index with command : " + s);
                        com.Execute(s);
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Message.Contains("What is the message for this state?"))
                        {
                            trace.WriteLine("That index can't be created, already exists");
                            //Oh well
                        }
                        else
                            throw;
                    }

                }

            }
        }

        public override void DropKeys()
        {
            throw new NotImplementedException();
        }

        public override void CreateKeys()
        {
            throw new NotImplementedException();
        }

        public override void CreateReportingStructure()
        {
            throw new NotImplementedException();
        }

        public override void Compact()
        {
            using (ParameterlessCommand com = new ParameterlessCommand(ConfigUtils.CurrentDataFactory()))
            {
                string database=null;
                string[] parts = com.ConnectionString.Split(';');
                trace.WriteLine("Extracting database name from connection string : " + com.ConnectionString);
                foreach(string part in parts)
                {
                    if(part.ToUpper().StartsWith("Initial Catalog".ToUpper()) 
                        || part.ToUpper().StartsWith("Database".ToUpper()))
                    {
                        database = part.Split('=')[1];
                    }
                }
                if(database==null)
                {
                    throw new InvalidOperationException("Can't infer database name from connection string.");
                }
                string sql = "DBCC SHRINKDATABASE(N'"+database +"' )";
                trace.WriteLine("Shrinking database with command : " + sql);
                com.Execute(sql);
            }
        }

        public override TableInfo TableInfo()
        {
            using (TableInfoQuery query = new TableInfoQuery())
            {
                trace.WriteLine("Looking up table size, etc");
                return query.Execute();
            }
        }
    }
}
