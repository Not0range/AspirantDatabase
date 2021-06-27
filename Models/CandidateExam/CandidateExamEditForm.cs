using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class CandidateExamEditForm
    {
        [MaxLength(50)]
        public string Subject { get; set; }

        public DateTime? DateTime { get; set; }

        public int? SpecialtyId { get; set; }

        public CandidateExam GetExam(CandidateExam exam)
        {
            if (!string.IsNullOrWhiteSpace(Subject))
                exam.Subject = Subject;
            if (DateTime.HasValue)
                exam.DateTime = DateTime.Value;
            if (SpecialtyId.HasValue)
                exam.SpecialtyId = SpecialtyId.Value;
            return exam;
        }
    }
}
