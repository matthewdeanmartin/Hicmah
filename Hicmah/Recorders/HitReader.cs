using System;
using System.Collections.Generic;
using System.Data;

namespace Hicmah.Recorders
{

    /// <summary>
    /// Provides a reader to SqlBulkCopy
    /// </summary>
    public class HitReader : IDataReader
    {
        internal readonly List<Hit> hitList;
        internal int row = -1;

        public HitReader(List<Hit> sourceHits)
        {
            hitList = sourceHits;
        }

        public void Close()
        {

        }

        public int Depth
        {
            get { throw new NotImplementedException(); }
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public bool IsClosed
        {
            get { return false; }
        }

        public bool NextResult()
        {
            return false;
        }

        public bool Read()
        {
            row++;
            if (row<hitList.Count )
            {
                return true;
            }
            return false;
        }

        public int RecordsAffected
        {
            get { return hitList.Count; }
        }

        public void Dispose()
        {

        }


        public int FieldCount
        {
            get { return 10; }
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            throw new NotImplementedException();
        }

        public int GetOrdinal(string name)
        {
            switch (name){
                case "hit_date":
                    return 1;
                case "utc_date":
                    return 2;
                case "date_id":
                    return 3;
                case "time_id":
                    return 4;
                case "client_date_id":
                    return 5;
                case "client_time_id":
                    return 6;
                case "time_zone_id":
                    return 7;
                case "full_url_id":
                    return 8;
                case "referrer_id":
                    return 9;
                case "user_agent_id":
                    return 10;
                case "invoker_id":
                    return 11;
                case "user_name_id":
                    return 12;
                case "request_type_id":
                    return 13;
                case "server_duration":
                    return 14;
                case "client_duration":
                    return 15;
                case "client_bytes":
                    return 16;
                case "status_code":
                    return 17;
                //case "seconds_on_page":
                //    return 18;
            }
            throw new InvalidOperationException("unrecognized column name: " + name);
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int i)
        {
            return this[i];
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public object this[string name]
        {
            get
            {
                switch (name)
                {
                    case "hit_date":
                        return hitList[row].HitDate;
                    case "utc_date":
                        return hitList[row].UtcTime.DateTime;
                    case "date_id":
                        return hitList[row].DateId;
                    case "time_id":
                        return hitList[row].TimeId;
                    case "client_date_id":
                        return hitList[row].ClientDateId;
                    case "client_time_id":
                        return hitList[row].ClientTimeId;
                    case "time_zone_id":
                        return hitList[row].ClientTimeZoneId;
                    case "full_url_id":
                        return hitList[row].UrlId;
                    case "referrer_id":
                        return hitList[row].ReferrerUrlId;
                    case "user_agent_id":
                        return hitList[row].UserAgentId;
                    case "invoker_id":
                        return (int)hitList[row].Invoker;
                    case "user_name_id":
                        return hitList[row].UserId;
                    case "request_type_id":
                        return (int)hitList[row].RequestType;
                    case "client_duration":
                        return hitList[row].ClientDuration;
                    case "server_duration":
                        return hitList[row].ServerDuration;
                    case "client_bytes":
                        return hitList[row].ClientBytes;
                    case "status_code":
                        return hitList[row].StatusCode;
                }
                throw new InvalidOperationException("unrecognized column name");
            }
        }

        public object this[int i]
        {
            get
            {
                switch (i)
                {
                    case 1://"hit_date":
                        return hitList[row].HitDate;
                    case 2://"utc_date":
                        return  hitList[row].UtcTime.DateTime; //Sql can't grok DateTimeOffset
                    case 3://"date_id":
                        return hitList[row].DateId;
                    case 4://"time_id":
                        return hitList[row].TimeId;
                    case 5://"client_date_id":
                        return hitList[row].ClientDateId;
                    case 6://"client_time_id":
                        return hitList[row].ClientTimeId;
                    case 7://"time_zone_id":
                        return hitList[row].ClientTimeZoneId;
                    case 8://"full_url_id":
                        return hitList[row].UrlId;
                    case 9://"referrer_id":
                        return hitList[row].ReferrerUrlId;
                    case 10://"user_agent_id":
                        return hitList[row].UserAgentId;
                    case 11://"invoker_id":
                        return Convert.ToInt16(hitList[row].Invoker);
                    case 12://"user_name_id":
                        return hitList[row].UserId;
                    case 13://"request_type":
                        return Convert.ToInt16(hitList[row].RequestType);
                    case 14://"server_duration":
                        return hitList[row].ServerDuration;
                    case 15://"client_duration":
                        return hitList[row].ClientDuration;
                    case 16://"client_bytes":
                        return hitList[row].ClientBytes;
                    case 17://"status_code":
                        return Convert.ToInt16(hitList[row].StatusCode);
                }
                throw new InvalidOperationException("unrecognized column ordinal");
            }
        }

    }
    
}
