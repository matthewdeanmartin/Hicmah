using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Caching;
using Dataglot;
using Hicmah.Misc;
using Hicmah.Web;
using Wrappers.WebContext;

namespace Hicmah.Recorders
{
    //hmm, not the best interface, hides the fact that the implementation depends on a cache, and a db connection
    interface ICachedLookupSql:IDisposable
    {
        int CachedReferenceLookup(string table, string key);

    }
    public class CachedLookupSql: ICachedLookupSql
    {
        private static TraceSourceUtil trace = new TraceSourceUtil("CachedLookupSql");

        readonly Dictionary<string, SqlCommand> selectLookups = new Dictionary<string, SqlCommand>(5);
        readonly Dictionary<string, SqlCommand> insertLookups = new Dictionary<string, SqlCommand>(5);
        private readonly SqlConnection con;
        private readonly ICacheWrapper cache;
        private readonly SqlTransaction transaction;

        GenericCommand composer = new GenericCommand(ConfigUtils.CurrentDataFactory());

        public CachedLookupSql(SqlConnection parentConnection, SqlTransaction parentTransaction, ICacheWrapper currentCache)
        {
            con = parentConnection;
            transaction = parentTransaction;
            cache = currentCache;
        }

        private SqlCommand CreateOrFindInsertLookupCommand(string table)
        {
            if (insertLookups.ContainsKey(table))
            {
                return insertLookups[table];
            }

            trace.WriteLine("Creating an insert lookup command for {0}", table);
            SqlCommand command = new SqlCommand(InsertIntoHicmahTable(table));
            command.Parameters.Add(new SqlParameter("@" + table, null));
            command.Connection = con;
            insertLookups.Add(table, command);
            return command;
        }

        private string InsertIntoHicmahTable(string table)
        {
            string  sql = @"Insert into [{$ns}_" + table + "s] ([" + table + "]) VALUES (@" + table + ");" + @"; SELECT SCOPE_IDENTITY()";
            sql = composer.Compose(sql);
            return sql;
        }


        //Hmm, fast concatenating...
        //http://blog.liranchen.com/2010/07/stringformat-isnt-suitable-for.html
        private string SelectFromHimahReference(string table)
        {
            
            string result = string.Join("", new[] {"Select [", table, "_id], [", table, "] from [{$ns}_", table, "s]"});

            return composer.Compose(result);
        }

        private int InsertCachedValue(string table, string key, Dictionary<string, int> tableCached)
        {
            //TODO: decide if we only want to cache some or frequent users, if only some, then the
            //following insert could fail.

            //This person is not in cache.
            //Insert into db
            SqlCommand comInsert = CreateOrFindInsertLookupCommand(table);
            comInsert.Parameters["@" + table].Value = key;

            if (transaction != null)
                comInsert.Transaction = transaction;

            using (SqlDataReader reader = comInsert.ExecuteReader(CommandBehavior.Default))
            {
                int value;
                if (reader.HasRows)
                {
                    reader.Read();
                    if (reader.IsDBNull(0))
                    {
                        throw new InvalidOperationException("Got null back instead of a key");
                    }
                    //HACK:What datatype is coming back at us??
                    object foo = reader[0];
                    value = Convert.ToInt32(foo);
                    tableCached.Add(key, value);
                }
                else
                {
                    throw new InvalidOperationException("Failed to get ID from database.");
                }
                //Just update cache
                cache[table] = tableCached;
                return value;
            }

        }

        private static object ThisLock = new object();

        /// <summary>
        /// Uses conventions.
        /// </summary>
        public int CachedReferenceLookup(string table, string key)
        {
            using (trace.ProfileOperation("Disposing of cached-lookup commands"))
            {
                //LOCK GOES HERE
                using (TimedLock.Lock(ThisLock))
                {

                    if (string.IsNullOrEmpty(key))
                        return 0;
                    object cachedObject = cache[table];
                    if (cachedObject != null)
                    {
                        trace.WriteLine("Cache hit for table " + table);
                        Dictionary<string, int> tableCached = (Dictionary<string, int>) cachedObject;
                        //Found table in cache and it has the key
                        if (tableCached.ContainsKey(key))
                            return tableCached[key];

                        //Oops, missing key. Go add it.
                        return InsertCachedValue(table, key, tableCached);
                    }

                    //Initialize the cache.
                    int value = 0;
                    Dictionary<string, int> newDictionary = new Dictionary<string, int>();
                    SqlCommand select = CreateOrFindSelectLookupCommand(table);

                    if (transaction != null)
                        select.Transaction = transaction;

                    using (SqlDataReader reader = select.ExecuteReader(CommandBehavior.Default))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetString(1) == key)
                                    value = reader.GetInt16(0);

                                if (newDictionary.ContainsKey(reader.GetString(1)))
                                {
                                    trace.WriteLine("Cache hit for {1} - {0}", reader.GetString(1), reader.GetInt16(0));
                                    //throw new InvalidOperationException("Reference table has duplicate in 2nd column");
                                }
                                else
                                {
                                    newDictionary.Add(reader.GetString(1), reader.GetInt16(0));
                                    trace.WriteLine("Adding value for cache miss {1} - {0}" , reader.GetString(1), reader.GetInt16(0));
                                }
                            }
                        }

                        if (newDictionary.Count > 0)
                        {
                            //TODO: Config driven cache policy.
                            cache.Add(table, newDictionary, null, DateTime.Now.AddDays(1), TimeSpan.Zero, CacheItemPriority.Low, null);
                            trace.WriteLine("Adding table {0} to cache", table);
                        }
                    }


                    //Initialized the cache and we found the corresponding Id
                    if (value > 0)
                        return value;

                    //D'oh, we initialized the cache, but still need to add a value.
                    return InsertCachedValue(table, key, newDictionary);
                }
            }
        }

        private SqlCommand CreateOrFindSelectLookupCommand(string table)
        {
            if (selectLookups.ContainsKey(table))
            {
                return selectLookups[table];
            }
            trace.WriteLine("Creating a select lookup command for {0}", table);
            
            SqlCommand command = new SqlCommand(SelectFromHimahReference(table));
            command.Connection = con;
            selectLookups.Add(table, command);
            return command;
        }

        public void Dispose()
        {
            using (trace.ProfileOperation("Disposing of cached-lookup commands"))
            {
                foreach (SqlCommand command in selectLookups.Values)
                {
                    command.Dispose();
                }
                foreach (SqlCommand command in insertLookups.Values)
                {
                    command.Dispose();
                }
            }
        }
    }
}
