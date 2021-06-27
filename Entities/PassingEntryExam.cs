using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspirantDatabase.Entities
{
    [Table("PassingEntryExams")]
    public class PassingEntryExam
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ExamId { get; set; }

        [ForeignKey("EntryExamId")]
        public EntryExam Exam { get; set; }

        [Required]
        public int AbiturientId { get; set; }

        [ForeignKey("AbiturientId")]
        public Abiturient Abiturient { get; set; }

        [Required]
        public int Result { get; set; }
    }
}
