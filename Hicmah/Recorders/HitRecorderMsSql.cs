using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using Dataglot;
using Dataglot.CrossDb;
using Hicmah.Misc;
using Hicmah.Web;
using Ukadc.Diagnostics;
using Wrappers.WebContext;

namespace Hicmah.Recorders
{
    /// <summary>
    /// Native hit counter for all versions of MS SQL
    /// </summary>
    public class HitRecorderMsSql : IHitRecorder
    {
        //Trace tells the best story for a single, or two insert(s).
        private TraceSourceUtil trace=null;

        public int Capacity { get; set; }

        /// <summary>
        /// Cache used for reducing database lookups for dimension columns.
        /// </summary>
        private readonly ICacheWrapper cache;

        SqlCommand insertHit;
        private string connectionString;

        SqlConnection con = null;
        
        private SqlTransaction transaction;
        private CachedLookupSql lookup;

        //Just to get at .Compose()
        private GenericCommand composer;

        public HitRecorderMsSql(ICacheWrapper currentCache)
        {
            string s = this.GetType().Name;
            trace = new TraceSourceUtil(s);

            trace.WriteLine("Default non-asynch mode");
            connectionString = ConfigUtils.ConnectionString();
            cache = currentCache;
            TestConnection();
            composer = new GenericCommand(ConfigUtils.CurrentDataFactory());
        }

        //This hit recorder requires a SqlClient connection string.
        public void TestConnection()
        {
            //If we are this far, we have a connection string, but the db might not exist.
            con = new SqlConnection(connectionString);
            con.Open();
            con.Close();

            if(con.Database=="master")
            {
                if(cache["HicmahDb"]!=null)
                {
                    connectionString = cache["HicmahDb"].ToString();
                    con.ConnectionString = connectionString;
                    return;
                }
                if(!ConfigUtils.AutomaticConfiguration())
                {
                    throw new InvalidOperationException("Server exists, but we don't have a database installed yet/haven't discovered one.");
                }
                ConnectToMaster guesser = new ConnectToMaster();
                connectionString= guesser.CreateDatabaseIfNeeded(con, "hicmah");
            }
        }

        public void UseAsync()
        {
            trace.WriteLine("Switching to Async mode");
            connectionString = ConfigUtils.AsyncConnectionString();
        }

        public void BulkInsert(IDataReader reader)
        {
            using (trace.ProfileOperation("BulkInsert"))
            {
                CreateAndOpenConnectionIfNeeded();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con, SqlBulkCopyOptions.TableLock, transaction))
                {
                    //According to articles, 0 is best.
                    bulkCopy.BatchSize = 0;
                    if (reader.RecordsAffected < 1000)
                    {
                        bulkCopy.NotifyAfter = 500;
                    }
                    else
                    {
                        bulkCopy.NotifyAfter = reader.RecordsAffected/6;
                    }
                    bulkCopy.BulkCopyTimeout = 30;
                    bulkCopy.SqlRowsCopied += bulkCopy_SqlRowsCopied;
                    bulkCopy.DestinationTableName = composer.Compose("{$ns}_hits");

                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("hit_date", "hit_date"));
                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("utc_date", "utc_date"));
                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("date_id", "date_id"));
                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("time_id", "time_id"));

                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("client_date_id", "client_date_id"));
                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("client_time_id", "client_time_id"));
                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("time_zone_id", "time_zone_id"));

                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("full_url_id", "full_url_id"));
                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("referrer_id", "referrer_id"));
                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("user_agent_id", "user_agent_id"));
                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("invoker_id", "invoker_id"));
                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("user_name_id", "user_name_id"));

                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("request_type_id", "request_type_id"));
                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("server_duration", "server_duration"));
                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("client_duration", "client_duration"));
                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("client_bytes", "client_bytes"));
                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("status_code", "status_code"));
                    //bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("seconds_on_page", "seconds_on_page"));

                    /*
                     * Re-enable the following to debug a bulk insert that blows up on "failed to covert" data type msgs
                     * 
                     * */
                    HitReader r = (HitReader) reader;
                    foreach (Hit hit in r.hitList)
                    {
                        if(!hit.IsValid())
                        {
                            Trace.Write(string.Join(@"
", hit.ListErrors()));
                            Debug.Write(string.Join(@"
", hit.ListErrors()));      
                        }
                    }
                    
                    while(reader.Read())
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(reader[reader.GetOrdinal("time_id")]);
                        
                        sb.Append(",");
                        sb.Append(reader[reader.GetOrdinal("time_zone_id")]);
                        sb.Append(",");
                        sb.Append(reader[reader.GetOrdinal("full_url_id")]);
                        sb.Append(",");
                        sb.Append(reader[reader.GetOrdinal("referrer_id")]);
                        sb.Append(",");
                        sb.Append(reader[reader.GetOrdinal("user_agent_id")]);
                        sb.Append(",");
                        sb.Append(reader[reader.GetOrdinal("user_name_id")]);
                        sb.Append(",");
                        sb.Append(reader[reader.GetOrdinal("status_code")]);
                        
                        trace.TraceInformation(sb.ToString());
                    }
                    r.row = -1;
                    

                    bulkCopy.WriteToServer(reader);

                    //Not sure this is really needed, since the reader isn't a real reader.
                    reader.Dispose();

                    //Don't call Close because close causes dispose and I'm already doing that.
                    //bulkCopy.Close();
                }
            }//Trace
        }

        void bulkCopy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            trace.WriteLine(e.RowsCopied + " rows have been copied");
        }

        public void LookupGaps(Hit hit)
        {
            using (trace.ProfileOperation("LookupGaps"))
            {
                CreateAndOpenConnectionIfNeeded();

                if (lookup == null)
                    lookup = new CachedLookupSql(con, transaction, cache);

                if (hit.DateId == 0)
                {
                    trace.WriteLine("Looking up date " + hit.HitDate);
                    hit.DateId = DateUtils.GetDateId(hit.HitDate);
                }

                if (hit.TimeId == 0)
                {
                    trace.WriteLine("Looking up time " + hit.HitDate);
                    hit.TimeId = DateUtils.GetTimeId(hit.HitDate);
                }

                //if (hit.ClientTimeZoneId== 0)
                //{
                //hit.ClientTimeZoneId = DateUtil.GetTimeId(hit.HitDate);
                //}

                //Who is this?
                if (hit.UserId == 0)
                {
                    trace.WriteLine("Looking up UserId " + hit.User);
                    hit.UserId = lookup.CachedReferenceLookup("user", hit.User);
                }

                if (hit.UrlId == 0)
                {
                    trace.WriteLine("Looking up UserId " + hit.Url);
                    hit.UrlId = lookup.CachedReferenceLookup("url", hit.Url);
                }

                if (hit.UserAgentId == 0)
                {
                    trace.WriteLine("Looking up UserAgentId " + hit.UserAgent);
                    hit.UserAgentId = lookup.CachedReferenceLookup("user_agent", hit.UserAgent);
                }

                if (hit.ReferrerUrlId == 0)
                {
                    trace.WriteLine("Looking up ReferrerUrlId " + hit.ReferrerUrl);
                    hit.ReferrerUrlId = lookup.CachedReferenceLookup("url", hit.ReferrerUrl);
                }
            }//trace
        }

        public void InsertHit(Hit hit)
        {
            using (trace.ProfileOperation("InsertHit"))
            {
                trace.TraceData(TraceEventType.Information,0,hit.HitDate);

                CreateAndOpenConnectionIfNeeded();

                bool isAsync = con.ConnectionString.ToLower().Contains("async");

                if (insertHit == null || isAsync)
                {
                    insertHit = CreateInsertCommand();
                }

                LookupGaps(hit);

                insertHit.Parameters["@hit_date"].Value = hit.HitDate;

                insertHit.Parameters["@utc_date"].Value = hit.UtcTime.DateTime;
                insertHit.Parameters["@date_id"].Value = hit.DateId;
                insertHit.Parameters["@time_id"].Value = hit.TimeId;
                insertHit.Parameters["@client_date_id"].Value = hit.ClientDateId;
                insertHit.Parameters["@client_time_id"].Value = hit.ClientTimeId;
                insertHit.Parameters["@time_zone_id"].Value = hit.ClientTimeZoneId;

                insertHit.Parameters["@user_name_id"].Value = hit.UserId;
                insertHit.Parameters["@full_url_id"].Value = hit.UrlId;
                insertHit.Parameters["@referrer_id"].Value = hit.ReferrerUrlId;
                insertHit.Parameters["@user_agent_id"].Value = hit.UserAgentId;
                insertHit.Parameters["@request_type_id"].Value = (int) hit.RequestType;
                insertHit.Parameters["@invoker_id"].Value = (int) hit.Invoker;

                insertHit.Parameters["@server_duration"].Value = hit.ServerDuration;
                insertHit.Parameters["@client_duration"].Value = hit.ClientDuration;
                insertHit.Parameters["@client_bytes"].Value = hit.ClientBytes;
                insertHit.Parameters["@status_code"].Value = hit.StatusCode;

                insertHit.Connection = con;

                if (transaction != null)
                    insertHit.Transaction = transaction;

                //NOTE: In performance testing with 1000 records insert sequentially.
                //asynch is 25% slower for all different, 100% slower for all the same.
                if (isAsync)
                {
                    //If asynch, requires new command
                    //  can't reused command because last might still be running.
                    //  requires new connection OR MARs (multiple active resultsets)

                    //Oops. Can't reuse command for asynch.
                    insertHit.BeginExecuteNonQuery(delegate(IAsyncResult ar)
                                                       {
                                                           try
                                                           {

                                                               if (!ar.IsCompleted)
                                                               {
                                                                   insertHit.EndExecuteNonQuery(ar);
                                                               }
                                                           }
                                                               //catch (Exception e)
                                                               //{
                                                               //     /* log exception e */
                                                               //}
                                                           finally
                                                           {
                                                               if (transaction != null)
                                                                   transaction.Commit();

                                                               if (con != null)
                                                               {
                                                                   if (con.State == ConnectionState.Open)
                                                                       con.Close();
                                                                   con.Dispose();
                                                               }

                                                           }
                                                       }, null);
                }
                else
                {
                    insertHit.ExecuteNonQuery();
                }
            }
        }

        public void Flush()
        {
        }

        private void CreateAndOpenConnectionIfNeeded()
        {
            
            if (con == null)
                con = new SqlConnection(connectionString);

            if (con.State == ConnectionState.Closed)
            {
                trace.WriteLine("Opening connection to db");
                con.Open();
                //TODO: Need to make sure we're not running in serializable...
                //con.BeginTransaction(IsolationLevel.ReadCommitted)
            }


            if (transaction == null)
            {
                trace.WriteLine("Begining a transaction");
                transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            
        }

        public void Checkpoint()
        {
            using (trace.ProfileOperation("Checkpointing a transaction (commiting part of a transaction)"))
            {
                transaction.Commit();

                transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
            }
        }

        private SqlCommand CreateInsertCommand()
        {
           
                const string sql = @"INSERT INTO dbo.{$ns}_hits (
hit_date,
utc_date,            
    date_id,
time_id,
client_date_id,
client_time_id,
time_zone_id,
	user_name_id,
    full_url_id,
	referrer_id,
    user_agent_id,
    invoker_id,
	
    request_type_id,
	server_duration,
	client_duration,
	client_bytes,
    status_code
) VALUES ( 
@hit_date,
@utc_date,
    @date_id,
@time_id,
@client_date_id,
@client_time_id,
@time_zone_id,
	@user_name_id,
    @full_url_id,
	@referrer_id,
    @user_agent_id,
    @invoker_id,
	
    @request_type_id,
	@server_duration,
	@client_duration,
	@client_bytes,
    @status_code
) ";
                
                SqlCommand com = new SqlCommand(composer.Compose(sql));

                SqlParameter par = SqlDateParameter("@hit_date");
                com.Parameters.Add(par);

                par = SqlDateParameter("@utc_date");
                com.Parameters.Add(par);

                par = SqlIntParameter("@date_id");
                com.Parameters.Add(par);

                par = SqlIntParameter("@time_id");
                com.Parameters.Add(par);

                par = SqlIntParameter("@client_date_id");
                com.Parameters.Add(par);

                par = SqlIntParameter("@client_time_id");
                com.Parameters.Add(par);

                par = SqlIntParameter("@time_zone_id");
                com.Parameters.Add(par);

                par = SqlIntParameter("@user_name_id");
                com.Parameters.Add(par);

                par = SqlIntParameter("@full_url_id");
                com.Parameters.Add(par);

                par = SqlIntParameter("@referrer_id");
                com.Parameters.Add(par);

                par = SqlIntParameter("@user_agent_id");
                com.Parameters.Add(par);

                //Enums
                par = SqlIntParameter("@request_type_id");
                com.Parameters.Add(par);

                par = SqlIntParameter("@invoker_id");
                com.Parameters.Add(par);

                //Data
                par = SqlIntParameter("@server_duration");
                com.Parameters.Add(par);

                par = SqlIntParameter("@client_duration");
                com.Parameters.Add(par);

                par = SqlIntParameter("@client_bytes");
                com.Parameters.Add(par);

                par = SqlIntParameter("@status_code");
                com.Parameters.Add(par);

                return com;
            
        }


        private static SqlParameter SqlDateParameter(string name)
        {
            SqlParameter par = new SqlParameter(name, SqlDbType.DateTime);
            par.Direction = ParameterDirection.Input;
            return par;
        }

        private static SqlParameter SqlIntParameter(string name)
        {
            SqlParameter par = new SqlParameter(name, SqlDbType.Int);
            par.Direction = ParameterDirection.Input;
            return par;
        }


        public void Dispose()
        {
            using (trace.ProfileOperation("Disposing HitRecorderMsSql"))
            {
                if (transaction != null)
                {
                    if (transaction.Connection != null)
                    {
                        transaction.Commit();
                        trace.WriteLine("Committing the transaction");
                    }
                    
                }


                if (insertHit != null)
                    insertHit.Dispose();

                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                        trace.WriteLine("Closing the connection");
                    }
                    
                    con.Dispose();
                }
            }
        }
    }
}
