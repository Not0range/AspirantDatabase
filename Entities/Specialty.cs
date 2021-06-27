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
    [Table("Specialties")]
    public class Specialty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CathedraId { get; set; }

        [ForeignKey("CathedraId")]
        public Cathedra Cathedra { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }
    }
}
