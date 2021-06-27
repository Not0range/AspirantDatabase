using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspirantDatabase.Entities
{
    [Table("Diploms")]
    public class Diplom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AspirantId { get; set; }

        [ForeignKey("AspirantId")]
        public Aspirant Aspirant { get; set; }

        [Required, Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public int? SpecialtyId { get; set; }

        [ForeignKey("SpecialtyId")]
        public Specialty Specialty { get; set; }

        [Required]
        public int CountSatisfactoryMarks { get; set; }
    }
}
