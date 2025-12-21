using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Dataglot;
using Hicmah.Misc;

namespace Hicmah.Dimensions
{
    public class UpdateUserUsage:GenericCommand
    {
        private readonly TraceSourceUtil trace = new TraceSourceUtil("UpdateUserUsage");

        public UpdateUserUsage(DbConnection prexistingConnection)
            : base(ConfigUtils.CurrentDataFactory())
        {
            trace.WriteLine("Updating user first, last usage");

            con = prexistingConnection;
            needToDisposeOfConnection = false;


            ComposeSql(
@"UPDATE {$ns}_users
SET first_use = @first_use,
    last_use = @last_use 
WHERE user_id= @user_id ");
            AddParameter("@first_use", DbType.DateTime);
            AddParameter("@last_use", DbType.DateTime);
            AddParameter("@user_id", DbType.Int32);
        }

        public void SetValues(DateTime firstUse, DateTime lastUse, int userId)
        {
            command.Parameters["@first_use"].Value = firstUse;
            command.Parameters["@last_use"].Value = lastUse;
            command.Parameters["@user_id"].Value=userId;
        }

        public void Execute()
        {
            int rowsAffected = command.ExecuteNonQuery();
            if(rowsAffected==0)
                throw new InvalidOperationException("expected 1 row to be updated, but it was 0");
        }
    }
}
