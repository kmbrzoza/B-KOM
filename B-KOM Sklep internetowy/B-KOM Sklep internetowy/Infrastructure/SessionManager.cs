using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace B_KOM_Sklep_internetowy.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private HttpSessionState session;

        public SessionManager()
        {
            session = HttpContext.Current.Session;
        }

        public T Get<T>(string key)
        {
            return (T)session[key];
        }

        public void Set<T>(string key, T value)
        {
            session[key] = value;
        }

        public void Abandon()
        {
            session.Abandon();
        }

        public T TrytGet<T>(string key)
        {
            try
            {
                return (T)session[key];
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}