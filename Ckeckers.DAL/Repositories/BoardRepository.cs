using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ckeckers.DAL.Repositories
{
    public class BoardRepository
    {

        private static string data = "";

        public void Save(string state)
        {
            data = state;
        }

        public string Load()
        {
            if (data == "")
            {
                data = "rnbqkbnrpppppppp11111111111111111111111111111111PPPPPPPPRNBQKBNR";
            }

            return data;
        }
    }
}
