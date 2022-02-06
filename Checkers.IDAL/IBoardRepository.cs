using System;
using System.Collections.Generic;
using System.Text;

namespace Ckeckers.DAL.Repositories
{
    public interface  IBoardRepository
    {
        void Save(string key, string state);


        string Load(string key);
    }
}

