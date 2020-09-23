using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace B_KOM_Sklep_internetowy.Infrastructure
{
    public class Helpers
    {
        public static void CallUrl(string url)
        {
            var request = HttpWebRequest.Create(url);
            request.GetResponseAsync();
        }
    }
}