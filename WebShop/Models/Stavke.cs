using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class Stavka
    {
        public int Id { get; set; }
        public decimal Kolicina { get; set; }
        public decimal Cena { get; set; }

        public virtual Narudzbenica Narudzbenica { get; set; }
        public virtual Artikal Artikal { get; set; }
    }
}