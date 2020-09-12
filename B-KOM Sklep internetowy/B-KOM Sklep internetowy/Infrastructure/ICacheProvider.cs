using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_KOM_Sklep_internetowy.Infrastructure
{
    public interface ICacheProvider
    {
        object Get(string key);
        void Set(string key, object data, int cacheTimeMin);
        bool IsSet(string key);
        void Invalidate(string key);
    }
}
