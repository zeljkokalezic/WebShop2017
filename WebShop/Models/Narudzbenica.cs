using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public enum StatusNarudzbenice
    {
        Otvorena,
        Naruceno,
        Poslato,
        Zatvorena
    }

    public class Narudzbenica
    {
        public int Id { get; set; }
        public StatusNarudzbenice Status { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Stavka> Stavke { get; set; }


        public decimal Total()
        {
            return this.Stavke.Sum(x => x.Cena * x.Kolicina);
        }
    }
}