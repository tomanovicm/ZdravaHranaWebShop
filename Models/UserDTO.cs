using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Models
{

    public class LoginUserDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Lozinka je ogranicena na broj karaktera od {2} do {1}", MinimumLength = 7)]
        public string Password { get; set; }
    }

    public class UserDTO : LoginUserDTO
    {
        public string Ime { get; set; }

        public string Prezime { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string BrojTelefona { get; set; }

        
  
        public ICollection<string> Roles { get; set; }
    }

    public class UpdateUserDTO : UserDTO
    {
        
    }
}
