using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspirantDatabase.Entities
{
    public class EntryExam
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Subject { get; set; }

        [Required, Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int? SpecialtyId { get; set; }

        [ForeignKey("SpecialtyId")]
        public Specialty Specialty { get; set; }
    }
}
