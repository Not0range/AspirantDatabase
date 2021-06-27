using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class ExamAddForm
    {
        [Required, MaxLength(50)]
        public string Subject { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public ExamType ExamType { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int SpecialtyId { get; set; }

        public Exam GetExam()
        {
            return new Exam
            {
                Subject = Subject,
                Date = Date,
                ExamType = ExamType,
                TeacherId = TeacherId,
                SpecialtyId = SpecialtyId
            };
        }
    }
}
