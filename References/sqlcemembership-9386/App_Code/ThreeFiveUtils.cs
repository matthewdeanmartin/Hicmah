using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace ErikEJ
{
    public class Str
    {
        //[Pure]
        public static bool IsNullOrWhiteSpace(string s)
        {
            //Contract.Ensures(s != null || Contract.Result<bool>());
            //Contract.Ensures((s != null && !Contract.ForAll<char>(s, c => char.IsWhiteSpace(c))) || Contract.Result<bool>());

            if (s == null) return true;

            for (var i = 0; i < s.Length; i++)
            {
                if (!char.IsWhiteSpace(s, i))
                {
                    //Contract.Assume(!Contract.ForAll<char>(s, c => char.IsWhiteSpace(c)));
                    return false;
                }
            }


            return true;
        }        
    }
}
