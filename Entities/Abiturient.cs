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
    [Table("Abiturients")]
    public class Abiturient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        [Required(AllowEmptyStrings = false), RegularExpression(@"^(\d+ )*\d+$")]
        public string Speciaties { get; set; }

        [Required, Column(TypeName = "date")]
        public DateTime AdmissionDate { get; set; }

        [NotMapped]
        public int[] SpecialtiesIndecies
        {
            get
            {
                return Speciaties.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => int.Parse(i)).ToArray();
            }
        }
    }
}
