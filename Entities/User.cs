using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Entities
{
    public class User : IdentityUser
    {
        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string BrojTelefona { get; set; }



        

    }
}
