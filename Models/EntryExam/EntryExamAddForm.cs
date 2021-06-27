using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class EntryExamAddForm
    {
        [Required, MaxLength(50)]
        public string Subject { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int SpecialtyId { get; set; }

        public EntryExam GetExam()
        {
            return new EntryExam
            {
                Subject = Subject,
                Date = Date,
                SpecialtyId = SpecialtyId
            };
        }
    }
}
