using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Models
{
    public class CreateStavkaKorpeDTO
    {
        [Required]
        public Guid korpaID { get; set; }
        [Required]
        public Guid proizvodID { get; set; }
        
        [Required]
        public int kolicina { get; set; }
    }
    public class StavkaKorpeDTO : CreateStavkaKorpeDTO
    {
        public Guid stavkaKorpeID { get; set; }
        public KorpaDTO Korpa { get; set; }
        public ProizvodDTO Proizvod { get; set; }
    }

    public class UpdateStavkaKorpeDTO : CreateStavkaKorpeDTO
    {

    }
}
