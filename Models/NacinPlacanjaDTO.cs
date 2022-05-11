using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Models
{
    public class CreateNacinPlacanjaDTO
    {
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Naziva nacina placanja je predugacak!")]
        public string nacin { get; set; }
    }
    public class NacinPlacanjaDTO : CreateNacinPlacanjaDTO
    {
        public Guid nacinID { get; set; }
        public IList<PorudzbinaDTO> Porudzbine { get; set; }
    }

    public class UpdateNacinPlacanjaDTO : CreateNacinPlacanjaDTO
    {

    }
}
