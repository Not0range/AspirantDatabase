using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class EntryExamEditForm
    {
        [MaxLength(50)]
        public string Subject { get; set; }

        public DateTime? Date { get; set; }

        public int? SpecialtyId { get; set; }

        public EntryExam GetExam(EntryExam exam)
        {
            if (!string.IsNullOrWhiteSpace(Subject))
                exam.Subject = Subject;
            if (Date.HasValue)
                exam.Date = Date.Value;
            if (SpecialtyId.HasValue)
                exam.SpecialtyId = SpecialtyId.Value;
            return exam;
        }
    }
}
