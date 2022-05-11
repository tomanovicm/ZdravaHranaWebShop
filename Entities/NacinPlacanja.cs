using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Entities
{
    public class NacinPlacanja
    {
        [Key]
        public Guid nacinID { get; set; } = Guid.NewGuid();

        public string nacin { get; set; }

        public virtual IList<Porudzbina> Porudzbine { get; set; }
    }
}
