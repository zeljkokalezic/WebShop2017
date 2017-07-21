using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class Adresa
    {
        public int Id { get; set; }
        public string Ulica { get; set; }
        public int Broj { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }
    }
}