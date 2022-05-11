using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ZdravaHranaWebShop.Models
{
    public class CreateProizvodDTO
    {
        [Required]
        [StringLength(maximumLength: 60, ErrorMessage = "Naziva proizvoda je predugacak!")]
        public string naziv { get; set; }

        [Required]
        public int cena { get; set; }

        [StringLength(maximumLength: 400, ErrorMessage = "Opis proizvoda je predugacak!")]
        public string opis { get; set; }

        [Required]
        public Guid? tipProizID { get; set; }

        [Required]
        public Guid? proizvodjacID { get; set; }

        [Required]
        public int zalihe { get; set; }

        [Range(1,5)]
        public double rejting { get; set; }
    }
    public class ProizvodDTO : CreateProizvodDTO
    {
        public Guid proizvodID { get; set; }
        public TipProizvodaDTO TipProizvoda { get; set; }
        public ProizvodjacDTO Proizvodjac { get; set; }

    }

    public class UpdateProizvodDTO : CreateProizvodDTO
    {

    }
}
