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
    [Table("CandidateExams")]
    public class CandidateExam
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Subject { get; set; }

        public int? SpecialtyId { get; set; }

        [ForeignKey("SpecialtyId")]
        public Specialty Specialty { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}
