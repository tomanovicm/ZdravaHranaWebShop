using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Entities
{
    public class Proizvod
    {
        [Key]
        public Guid proizvodID { get; set; } = Guid.NewGuid();
        public string naziv { get; set; }
        public int cena { get; set; }
        public string opis { get; set; }

        [ForeignKey("TipProizvoda")]
        public Guid tipProizID { get; set; }
        public TipProizvoda TipProizvoda { get; set; }

        [ForeignKey("Proizvodjac")]
        public Guid proizvodjacID { get; set; }
        public Proizvodjac Proizvodjac { get; set; }

        public int zalihe { get; set; }

        public double rejting { get; set; }
    }
}
