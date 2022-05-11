using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Models
{
    public class CreatePorudzbinaDTO
    {
        [Required]
        public Guid nacinID { get; set; }
        
        [Required]
        public Guid korpaID { get; set; }

        [Required]
        public int dodatniTroskovi { get; set; }

    }
    public class PorudzbinaDTO : CreatePorudzbinaDTO
    {
        public Guid porudzbinaID { get; set; }
        public NacinPlacanjaDTO nacinPlacanja { get; set; }
        public KorpaDTO korpa { get; set; }
        public DateTime datumPorudzbine { get; set; }
        public int ukupanIznos { get; set; }

        public Guid adresaID { get; set; }

        public AdresaDTO Adresa { get; set; }
    }
    
    public class UpdatePorudzbinaDTO : CreatePorudzbinaDTO
    {

    }
}
