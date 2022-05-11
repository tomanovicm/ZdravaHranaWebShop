using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Entities
{
    public class Proizvodjac
    {
        [Key]
        public Guid proizvodjacID { get; set; } = Guid.NewGuid();

        public string nazivProizvodjaca { get; set; }

        public virtual IList<Proizvod> Proizvodi { get; set; }
    }
}
