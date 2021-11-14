using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ckeckers.DAL.Repositories
{
    public class BoardRepository : IBoardRepository
    {

        private static Dictionary<string, string> data = new Dictionary<string, string>();

        //static string DefaultData= "1p1p1p1pp1p1p1p11p1p1p1p1111111111111111P1P1P1P11P1P1P1PP1P1P1P1w";
        static string DefaultData =""+
            "1p1p1" +
            "11111" +
            "11111"+
            "11111"+
            "1Q1P1w";
        
        private static object lockOnject = new object();
        public void Save(string key, string state)
        {
            lock (lockOnject)
            {
                data[key] = state;
            }
            
        }

        public string Load(string key)
        {
            lock (lockOnject)
            {
                if (!data.ContainsKey(key) || data[key] == "")
                {
                    data[key] = DefaultData;
                }

                return data[key];
            }
        }
    }
}
