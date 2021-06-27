using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspirantDatabase.Entities
{
    [Table("PassingExams")]
    public class PassingExam
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ExamId { get; set; }

        [ForeignKey("EntryExamId")]
        public Exam Exam { get; set; }

        [Required]
        public int AspirantId { get; set; }

        [ForeignKey("AspirantId")]
        public Aspirant Aspirant { get; set; }

        [Required]
        public int Result { get; set; }
    }

    public enum ExamType : int
    {
        Offset,
        OffsetWithMark,
        Exam
    }
}
