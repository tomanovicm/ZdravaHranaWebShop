using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Entities
{
    public class Korpa
    {
        [Key]
        public Guid korpaID { get; set; } = Guid.NewGuid();

        public int ukupnaCena { get; set; }

        public DateTime datum { get; set; }

        [ForeignKey("User")]
        public string Email { get; set; }
        public User User { get; set; }

        public virtual IList<StavkaKorpe> StavkeKorpe { get; set; }



    }
}
