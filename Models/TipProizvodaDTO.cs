using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZdravaHranaWebShop.Models
{
    public class CreateTipProizvodaDTO
    {
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Naziv tipa proizvoda je predugacak!")]
        public string nazivTipa { get; set; }
    }
    public class TipProizvodaDTO : CreateTipProizvodaDTO
    {
        public Guid tipProizID { get; set; }
        public IList<ProizvodDTO> Proizvodi { get; set; }
    }

    public class UpdateTipProizvodaDTO : CreateTipProizvodaDTO
    {

    }
}
