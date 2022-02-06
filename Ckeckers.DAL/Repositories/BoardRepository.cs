using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ckeckers.DAL.Repositories
{
    public class BoardRepository : IBoardRepository
    {

        private Dictionary<string, string> _data = new Dictionary<string, string>();

        private object _lockObject = new object();

        public BoardRepository()
        {
            ;
        }
        

        string DefaultData= "1p1p1p1pp1p1p1p11p1p1p1p1111111111111111P1P1P1P11P1P1P1PP1P1P1P1w";
      /*  static string DefaultData =""+
                                   "111111" +
                                   "111111" +
                                   "111111" +
                                   "111111" +
                                   "1p1p11" +
                                   "P1P111w";*/
        
        
        public void Save(string key, string state)
        {
            lock (_lockObject)
            {
                _data[key] = state;
            }
            
        }


        public string Load(string key)
        {
            lock (_lockObject)
            {
                if (!_data.ContainsKey(key) || _data[key] == "")
                {
                    _data[key] = DefaultData;
                }

                return _data[key];
            }
        }
    }
}
