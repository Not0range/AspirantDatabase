using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class ExamEditForm
    {
        [MaxLength(50)]
        public string Subject { get; set; }

        public DateTime? Date { get; set; }

        public ExamType? ExamType { get; set; }

        public int? TeacherId { get; set; }

        public int? SpecialtyId { get; set; }

        public Exam GetExam(Exam exam)
        {
            if(!string.IsNullOrWhiteSpace(Subject))
                exam.Subject = Subject;
            if(Date.HasValue)
                exam.Date = Date.Value;
            if (ExamType.HasValue)
                exam.ExamType = ExamType.Value;
            if (TeacherId.HasValue)
                exam.TeacherId = TeacherId.Value;
            if (SpecialtyId.HasValue)
                exam.SpecialtyId = SpecialtyId.Value;
            return exam;
        }
    }
}
