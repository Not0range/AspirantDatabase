using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class CandidateExamAddForm
    {
        [Required, MaxLength(50)]
        public string Subject { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public int SpecialtyId { get; set; }

        public CandidateExam GetExam()
        {
            return new CandidateExam
            {
                Subject = Subject,
                DateTime = DateTime,
                SpecialtyId = SpecialtyId
            };
        }
    }
}
