using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Entities
{
    public class StavkaKorpe
    {
        [Key]
        public Guid stavkaKorpeID { get; set; } = Guid.NewGuid();

        [ForeignKey("Korpa")]
        public Guid korpaID { get; set; }
        public Korpa Korpa { get; set; }

        [ForeignKey("Proizvod")]
        public Guid proizvodID { get; set; }
        public Proizvod Proizvod { get; set; }

        public int kolicina { get; set; }
    }
}
