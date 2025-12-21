using System;
using System.Data;
using Dataglot;

namespace Hicmah.Dimensions
{
    public class InsertEnum : GenericCommand
    {
        public InsertEnum(string table):base(ConfigUtils.CurrentDataFactory())
        {
            string sql = @"
INSERT INTO {$ns}_" + table + @"
     VALUES
           (@id
           ,@name
           ,@description)
";
            ComposeSql(sql);
            AddParameter("@id", DbType.Int32);
            AddParameter("@name", DbType.String);
            AddParameter("@description", DbType.String);

        }

        public void Insert(
            int id,
            string name,
            string description)
        {
            command.Parameters["@id"].Value = id;
            command.Parameters["@name"].Value = name;
            command.Parameters["@description"].Value = description;
            ExecuteNonQuery();
        }
    }
}
