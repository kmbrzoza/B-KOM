using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class Favourite
    {
        public int FavouriteId { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}