using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class Artikal
    {
        public int Id { get; set; }

        [Required]
        public string Ime { get; set; }
        public string Opis { get; set; }
        public decimal Cena { get; set; }

        [Required]
        public string Slika { get; set; }

        [Display(Name = "Kolicina")]
        public decimal RaspolozivaKolicina { get; set; }

        public virtual ICollection<Stavka> Stavke { get; set; }
    }
}