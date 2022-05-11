using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Entities
{
    public class Porudzbina
    {
        [Key]
        public Guid porudzbinaID { get; set; } = Guid.NewGuid();

        [ForeignKey("NacinPlacanja")]
        public Guid nacinID { get; set; }
        public NacinPlacanja nacinPlacanja { get; set; }

        [ForeignKey("Korpa")]
        public Guid korpaID { get; set; }
        public Korpa korpa { get; set; }

        public int dodatniTroskovi { get; set; }

        public DateTime datumPorudzbine { get; set; }

        public int ukupanIznos { get; set; }

        [ForeignKey("Adresa")]
        public Guid AdresaId { get; set; }
        public Adresa Adresa { get; set; }
    }
}
