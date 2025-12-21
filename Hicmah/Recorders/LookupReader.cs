using System;
using System.Collections.Generic;
using System.Data;

namespace Hicmah.Recorders
{
    public class LookupReader:IDataReader
    {
         readonly Dictionary<int,KeyValuePair<int,string>> keyValues;
        private int row = 0;

        public LookupReader(Dictionary<int, KeyValuePair<int, string>> sourceKeyValues)
        {
            keyValues = sourceKeyValues;
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
            if (keyValues.Count < row)
            {
                row++;
                return true;
            }
            return false;
        }

        public int RecordsAffected
        {
            get { return keyValues.Count; }
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
            if (name.EndsWith("_id"))
                return 0;
            else
            {
                return 1;
            }
            throw new InvalidOperationException("unrecognized column name: " + name);
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int i)
        {
            throw new NotImplementedException();
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
                if (name.EndsWith("_id"))
                    return keyValues[row].Key;
                else
                {
                    return keyValues[row].Value;
                }
                //throw new InvalidOperationException("unrecognized column name");
            }
        }

        public object this[int i]
        {
            get
            {
                if (i==0)
                    return keyValues[row].Key;
                else
                {
                    return keyValues[row].Value;
                }
                //throw new InvalidOperationException("unrecognized column ordinal");
            }
        }
    }
}
