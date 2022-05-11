using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Models
{
    public class KorpaDTO
    {
        public Guid korpaID { get; set; } 

        public int ukupnaCena { get; set; }

        public DateTime datum { get; set; }

        [Required]
        public string Email { get; set; }

        public IList<StavkaKorpeDTO> StavkeKorpe { get; set; }

    }
}
