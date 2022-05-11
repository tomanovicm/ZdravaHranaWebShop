using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Models
{
    public class CreateAdresaDTO
    {
        [Required]
        [StringLength(maximumLength: 80, ErrorMessage = "Naziv ulice je predugacak!")]
        public string ulica { get; set; }

        [Required]
        [StringLength(maximumLength: 5)]
        public string broj { get; set; }

        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Naziv grada je predugacak!")]
        public string grad { get; set; }

        [Required]
        public int postanskiBroj { get; set; }

        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Naziv drzave predugacak!")]
        public string drzava { get; set; }
    }
    public class AdresaDTO : CreateAdresaDTO
    {
        public Guid adresaID { get; set; }
        public IList<PorudzbinaDTO> Porudzbina { get; set; }
    }

    public class UpdateAdresaDTO : CreateAdresaDTO
    {

    }
}
