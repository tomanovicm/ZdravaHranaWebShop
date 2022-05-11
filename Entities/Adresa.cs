using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Entities
{
    public class Adresa
    {
        [Key]
        public Guid adresaID { get; set; } = Guid.NewGuid();

        public string ulica { get; set; }

        public string broj { get; set; }

        public string grad { get; set; }

        public int postanskiBroj { get; set; }

        public string drzava { get; set; }

        public virtual IList<Porudzbina> Porudzbina { get; set; }

    }
}
