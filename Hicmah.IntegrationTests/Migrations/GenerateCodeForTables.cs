using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using DatabaseSchemaReader;
using DatabaseSchemaReader.DataSchema;
using DatabaseSchemaReader.SqlGen;
using Dataglot;
using Hicmah;
using Hicmah.Administration;
using NUnit.Framework;

namespace Himah.IntegrationTests.Migrations
{
    [TestFixture]
    public class GenerateCodeForTables
    {
        [Test]
        public void TestCodeGent()
        {
            DatabaseSchema schemaNew = new DatabaseSchema(ConfigurationManager.ConnectionStrings["HicmahDb"].ConnectionString, "System.Data.SqlClient");
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
            constraint.Name = "PK_hicmah_cache";
            constraint.Columns.Add("cache_id");
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
            constraint.Columns.Add("date_id");
            constraint.Name = "PK_hicmah_dates";
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
            newIndex.Name = "PK_hicmah_dates";
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
            constraint.Name = "PK_hicmah_hits";
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
            constraint.Name = "PK_hicmah_invoker";
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
            constraint.Name = "PK_hicmah_queries";
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
            constraint.Name = "PK_hicmah_request_types";
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
            constraint.Name = "PK_hicmah_servers";
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
            constraint.Name = "PK_hicmah_times";
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
            constraint.Name = "PK_hicmah_time_zones";
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
            constraint.Name = "PK_hicmah_time_urls";
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
            constraint.Name = "PK_hicmah_users";
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
            constraint.Name = "PK_hicmah_user_agents";
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
            DataFactory factory = new DataFactory(DbBrand.MsSql2005,
                                                  ConfigurationManager.ConnectionStrings["HicmahDb"].ConnectionString);
            ParameterlessCommand com = new ParameterlessCommand(factory);
            Debug.WriteLine(result2);
            com.Execute(result2);
        }

        [Test]
        public void TestIt()
        {
            //OK, we can pull the hicmah tables and generate sql. Can we generate C# that will generate db specific sql on demand?
            DatabaseInstaller di = new DatabaseInstaller();
            string result = di.Test2();
            Assert.IsNotNull(result);
        
        }

        [Test]
        public void GenerateCsForIndexes()
        {

            //It might work... depends on if it depends on the tables

            DatabaseReader dr = new DatabaseReader(ConfigurationManager.ConnectionStrings["HicmahDb"].ConnectionString, "System.Data.SqlClient");
            DatabaseSchema schema = dr.ReadAll();

            StringBuilder sb = new StringBuilder();

            DatabaseSchema schemaNew = new DatabaseSchema(ConfigurationManager.ConnectionStrings["HicmahDb"].ConnectionString, "System.Data.SqlClient");
            sb.Append("DatabaseSchema schemaNew = new DatabaseSchema(ConfigurationManager.ConnectionStrings[\"ConnectionString\"].ConnectionString, \"System.Data.SqlClient\");");
            //Tables
            //bool isDtDeclared = false;
            //bool isDcDeclared = false;
            //bool isnewConstraintDeclared = false;
            //bool isConstraintDeclared = false;
            bool isnewIndexDeclared = false;
            bool isNewIndexedColumnDeclared = false;

            foreach (DatabaseTable t in schema.Tables)
            {
                if (!t.Name.StartsWith("hicmah_")) continue;

                //Debug.WriteLine("D-Table " + t.Name);
                //DatabaseTable dt = new DatabaseTable();
                //dt.Name = t.Name;
                DatabaseTable dt = schemaNew.AddTable(t.Name);

                foreach (DatabaseIndex currentDbIndex in t.Indexes)
                {
                    DatabaseIndex newIndex = new DatabaseIndex();

                    if (!isnewIndexDeclared)
                    {
                        isnewIndexDeclared = true;
                        sb.Append("\nDatabaseIndex newIndex = new DatabaseIndex();\n");
                    }
                    else
                    {
                        sb.Append("\nnewIndex = new DatabaseIndex();\n");
                    }

                    newIndex.Name = currentDbIndex.Name;
                    sb.Append(string.Format("newIndex.Name = \"{0}\";\n", currentDbIndex.Name));
                    newIndex.IsUnique = currentDbIndex.IsUnique;
                    sb.Append(string.Format("newIndex.IsUnique = {0};\n", currentDbIndex.IsUnique.ToString().ToLower()));
                    newIndex.IndexType = currentDbIndex.IndexType;
                    sb.Append(string.Format("newIndex.IndexType = \"{0}\";\n", currentDbIndex.IndexType));

                    //Does this override the given name?
                    //newIndex.TableName = currentDbIndex.TableName;
                    //sb.Append(string.Format("newIndex.TableName = \"{0}\";\n",  currentDbIndex.TableName));
                    foreach (DatabaseColumn indexedColumn in currentDbIndex.Columns)
                    {
                        DatabaseColumn newIndexedColumn = new DatabaseColumn();

                        if (!isNewIndexedColumnDeclared)
                        {
                            isNewIndexedColumnDeclared = true;
                            sb.Append("DatabaseColumn newIndexedColumn = new DatabaseColumn();\n");
                        }
                        else
                        {
                            sb.Append("newIndexedColumn = new DatabaseColumn();\n");
                        }
                        newIndexedColumn.Name = indexedColumn.Name;
                        sb.Append(string.Format("newIndexedColumn.Name = \"{0}\";\n", indexedColumn.Name));
                        newIndex.Columns.Add(newIndexedColumn);
                        sb.Append("newIndex.Columns.Add(newIndexedColumn);\n");
                    }
                    dt.Indexes.Add(newIndex);
                    sb.Append("dt.Indexes.Add(newIndex);\n");
                }
                
            }

            Debug.WriteLine(sb.ToString());

        }

        [Test]
        public void GenerateCsForTables()
        {
            //SchemaReader sr = new SchemaReader(ConfigurationManager.ConnectionStrings["OledbSql"].ConnectionString, "System.Data.SqlClient");
            DatabaseReader dr = new DatabaseReader(ConfigurationManager.ConnectionStrings["HicmahDb"].ConnectionString, "System.Data.SqlClient");
            DatabaseSchema schema = dr.ReadAll();

            StringBuilder sb = new StringBuilder();
            StringBuilder indexBuilder = new StringBuilder();

            DatabaseSchema schemaNew = new DatabaseSchema(ConfigurationManager.ConnectionStrings["HicmahDb"].ConnectionString, "System.Data.SqlClient");
            
            //Tables
            bool isDtDeclared = false;
            bool isDcDeclared = false;
            bool isnewConstraintDeclared = false;
            bool isConstraintDeclared = false;
            //bool isnewIndexDeclared = false;
            //bool isNewIndexedColumnDeclared = false;

            string prefix = "hicmah_settings";

            sb.Append("\n\nprivate static void CreateAllTables()");

            sb.Append("\n{\n");

            sb.Append("DatabaseSchema schemaNew = new DatabaseSchema(ConfigurationManager.ConnectionStrings[\"ConnectionString\"].ConnectionString, \"System.Data.SqlClient\");");

            foreach (DatabaseTable t in schema.Tables)
            {
                if (!t.Name.StartsWith(prefix + "_")) continue;
                string csStyleName = CreateCsStyleName(prefix, t);
                sb.Append("\nCreate" + csStyleName + "Table(schemaNew);");
            }
            sb.Append("\n}\n");
            

            foreach(DatabaseTable t in schema.Tables)
            {
                if (!t.Name.StartsWith(prefix + "_")) continue;

                //Debug.WriteLine("D-Table " + t.Name);
                //DatabaseTable dt = new DatabaseTable();
                //dt.Name = t.Name;
                DatabaseTable dt = schemaNew.AddTable(t.Name);

                
                string csStyleName = CreateCsStyleName(prefix, t);
                sb.Append("\n\nprivate static void Create" + csStyleName + "Table(DatabaseSchema schemaNew)");

                sb.Append("\n{\n");

                if (!isDtDeclared)
                {
                    //isDtDeclared = true;
                    sb.Append(string.Format("\nDatabaseTable dt = schemaNew.AddTable(\"{0}\");\n", t.Name));
                }
                else
                {
                    sb.Append(string.Format("\ndt = schemaNew.AddTable(\"{0}\");\n", t.Name));
                }


                //Trace.Write("T-Table " + t.Name);
                foreach(DatabaseColumn c in t.Columns)
                {
                    DatabaseColumn dc = new DatabaseColumn();
                    if (!isDcDeclared)
                    {
                        //isDcDeclared = true;
                        sb.Append("\nDatabaseColumn dc = new DatabaseColumn();\n");
                    }
                    else
                    {
                        sb.Append("\ndc = new DatabaseColumn();\n");
                    }

                    dc.Name = c.Name;
                    sb.Append(string.Format("dc.Name =\"{0}\";\n", c.Name));

                    //Why aren't the next two the same thing?
                    //dc.DataType = c.DataType;
                    //sb.Append(string.Format("dc.DataType = {0};\n", c.DataType));

                    dc.DbDataType = c.DbDataType;
                    sb.Append(string.Format("dc.DbDataType =\"{0}\";\n", c.DbDataType)); //This is db specific.

                    if(c.Length!=null)
                    {
                        dc.Length = c.Length;
                        sb.Append(string.Format("dc.Length = {0};\n", c.Length));
                    }

                    dc.IsIdentity=c.IsIdentity;
                    sb.Append(string.Format("dc.IsIdentity = {0};\n", c.IsIdentity.ToString().ToLower()));

                    dc.IsPrimaryKey = c.IsPrimaryKey;
                    sb.Append(string.Format("dc.IsPrimaryKey = {0};\n", c.IsPrimaryKey.ToString().ToLower()));

                    dc.IsUniqueKey = c.IsUniqueKey;
                    sb.Append(string.Format("dc.IsUniqueKey = {0};\n", c.IsUniqueKey.ToString().ToLower()));

                    dc.IsIndexed = c.IsIndexed;
                    sb.Append(string.Format("dc.IsIndexed = {0};\n", c.IsIndexed.ToString().ToLower()));

                    dc.Ordinal = c.Ordinal;
                    sb.Append(string.Format("dc.Ordinal = {0};\n", c.Ordinal));

                    dc.Nullable = c.Nullable;
                    sb.Append(string.Format("dc.Nullable = {0};\n", c.Nullable.ToString().ToLower()));

                    //Trace.Write("T-     Col: " + c.Name);
                    foreach(DatabaseConstraint currentConstraint  in  t.CheckConstraints)
                    {
                        DatabaseConstraint newConstraint = new DatabaseConstraint();
                        
                        if (!isnewConstraintDeclared)
                        {
                            isnewConstraintDeclared = true;
                            sb.Append("\nDatabaseConstraint newConstraint = new DatabaseConstraint();\n");
                        }
                        else
                        {
                            sb.Append("\nnewConstraint = new DatabaseConstraint();\n");
                        }

                        newConstraint.ConstraintType = currentConstraint.ConstraintType;
                        sb.Append(string.Format("newConstraint.ConstraintType = {0};\n",currentConstraint.ConstraintType));

                        foreach (string constraintColumn in currentConstraint.Columns)
                        {
                            sb.Append(string.Format("newConstraint.Columns.Add(\"{0}\");\n", constraintColumn));
                            newConstraint.Columns.Add(constraintColumn);
                        }
                        if (currentConstraint.ConstraintType == ConstraintType.Check)
                        {
                            newConstraint.Expression = currentConstraint.Expression;
                            sb.Append(string.Format("newConstraint.Expression = {0};\n", currentConstraint.Expression));
                        }
                        newConstraint.Name = currentConstraint.Name;
                        sb.Append(string.Format("newConstraint.Name = {0};\n", currentConstraint.Name));

                        if (currentConstraint.ConstraintType == ConstraintType.ForeignKey)
                        {
                            newConstraint.RefersToTable = currentConstraint.RefersToTable;
                            sb.Append(string.Format("newConstraint.RefersToTable = \"{0}\";\n", currentConstraint.RefersToTable));
                        }
                        dt.AddConstraint(newConstraint);
                        sb.Append("dt.AddConstraint(newConstraint);\n");
                    }
                    
                    if (t.PrimaryKeyColumn!=null)
                    {
                        if (t.PrimaryKeyColumn.Name == c.Name)
                        {
                            DatabaseConstraint constraint = new DatabaseConstraint();
                            if (!isConstraintDeclared)
                            {
                                isConstraintDeclared = true;
                                sb.Append("DatabaseConstraint constraint = new DatabaseConstraint();\n");
                            }
                            else
                            {
                                sb.Append("constraint = new DatabaseConstraint();\n");
                            }
                            constraint.Name = "PK_" + t.Name;
                            constraint.ConstraintType = ConstraintType.PrimaryKey;
                            sb.Append("constraint.ConstraintType = ConstraintType.PrimaryKey;\n");
                            constraint.Columns.Add(c.Name);
                            sb.Append(string.Format("constraint.Columns.Add(\"{0}\");\n",c.Name));

                            dt.PrimaryKey = constraint;
                            sb.Append("dt.PrimaryKey = constraint;\n");
                        }
                    }
                    dt.AddColumn(dc);
                    sb.Append("dt.AddColumn(dc);\n");
                }

/*
                if (false)
                {
                    foreach (DatabaseIndex currentDbIndex in t.Indexes)
                    {
                        DatabaseIndex newIndex = new DatabaseIndex();

                        if (!isnewIndexDeclared)
                        {
                            isnewIndexDeclared = true;
                            sb.Append("\nDatabaseIndex newIndex = new DatabaseIndex();\n");
                        }
                        else
                        {
                            sb.Append("\nnewIndex = new DatabaseIndex();\n");
                        }

                        newIndex.Name = currentDbIndex.Name;
                        sb.Append(string.Format("newIndex.Name = \"{0}\";\n", currentDbIndex.Name));
                        newIndex.IsUnique = currentDbIndex.IsUnique;
                        sb.Append(string.Format("newIndex.IsUnique = {0};\n", currentDbIndex.IsUnique.ToString().ToLower()));
                        newIndex.IndexType = currentDbIndex.IndexType;
                        sb.Append(string.Format("newIndex.IndexType = \"{0}\";\n", currentDbIndex.IndexType));

                        //Does this override the given name?
                        //newIndex.TableName = currentDbIndex.TableName;
                        //sb.Append(string.Format("newIndex.TableName = \"{0}\";\n",  currentDbIndex.TableName));
                        foreach (DatabaseColumn indexedColumn in currentDbIndex.Columns)
                        {
                            DatabaseColumn newIndexedColumn = new DatabaseColumn();

                            if (!isNewIndexedColumnDeclared)
                            {
                                isNewIndexedColumnDeclared = true;
                                sb.Append("DatabaseColumn newIndexedColumn = new DatabaseColumn();\n");
                            }
                            else
                            {
                                sb.Append("newIndexedColumn = new DatabaseColumn();\n");
                            }
                            newIndexedColumn.Name = indexedColumn.Name;
                            sb.Append(string.Format("newIndexedColumn.Name = \"{0}\";\n", indexedColumn.Name));
                            newIndex.Columns.Add(newIndexedColumn);
                            sb.Append("newIndex.Columns.Add(newIndexedColumn);\n");
                        }
                        dt.Indexes.Add(newIndex);
                        sb.Append("dt.Indexes.Add(newIndex);\n");
                    }
                }
*/
                sb.Append("} //End table");
            }

            string resultsSoFar = sb.ToString();
            Debug.WriteLine(sb.ToString());

            DdlGeneratorFactory gen = new DdlGeneratorFactory(SqlType.SqlServer);
            ITablesGenerator tablesGenerator = gen.AllTablesGenerator(schemaNew);
            tablesGenerator.IncludeSchema = false;
            
            string result2 = tablesGenerator.Write();
            Debug.WriteLine(result2);
            Assert.IsNotNull(result2);
            //OK, we can pull the hicmah tables and generate sql. Can we generate C# that will generate db specific sql on demand?
            DatabaseInstaller di = new DatabaseInstaller();
            string result = di.Test2();

            //Assert.IsNotNull(result);

        }

        private static string CreateCsStyleName(string prefix,  DatabaseTable t)
        {
            CultureInfo ci = new CultureInfo("en-US", false);
            TextInfo textInfo = ci.TextInfo;
                
            string csStyleName = textInfo.ToTitleCase(t.Name.Replace("_", " ")).Replace(" ", "");
            csStyleName = csStyleName.Substring(prefix.Length, csStyleName.Length - prefix.Length);
            return csStyleName;
        }
    }
}

