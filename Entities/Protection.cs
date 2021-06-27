using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbelt.ComponentModel.DataAnnotations.Schema.V5;

namespace AspirantDatabase.Entities
{
    [Table("Protections")]
    public class Protection
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AspirantId { get; set; }

        [ForeignKey("AspirantId")]
        public Aspirant Aspirant { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(150)]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(50)]
        public string University { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Commission { get; set; }

        [Required]
        public int Result { get; set; }
    }
}
