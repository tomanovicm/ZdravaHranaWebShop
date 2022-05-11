using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Models
{
    public class CreateProizvodjacDTO
    {
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Naziva proizvodjaca je predugacak!")]
        public string nazivProizvodjaca { get; set; }
    }

    public class ProizvodjacDTO : CreateProizvodjacDTO
    {
        public Guid proizvodjacID { get; set; }
        public IList<ProizvodDTO> Proizvodi { get; set; }
    }

    public class UpdateProizvodjacDTO : CreateProizvodjacDTO
    {

    }
}
