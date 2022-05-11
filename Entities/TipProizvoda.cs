using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Entities
{
    public class TipProizvoda
    {
        [Key]
        public Guid tipProizID { get; set; } = Guid.NewGuid();

        public string nazivTipa { get; set; }

        public virtual IList<Proizvod> Proizvodi { get; set; }
    }
}
