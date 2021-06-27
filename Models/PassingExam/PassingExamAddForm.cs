using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class PassingExamAddForm
    {
        [Required]
        public int ExamId { get; set; }

        [Required]
        public int Id { get; set; }

        [Required]
        public int Result { get; set; }

        public PassingEntryExam GetPassingEntry()
        {
            return new PassingEntryExam
            {
                ExamId = ExamId,
                AbiturientId = Id,
                Result = Result
            };
        }

        public PassingExam GetPassingExam()
        {
            return new PassingExam
            {
                ExamId = ExamId,
                AspirantId = Id,
                Result = Result
            };
        }

        public PassingCandidateExam GetPassingCandidate()
        {
            return new PassingCandidateExam
            {
                ExamId = ExamId,
                AspirantId = Id,
                Result = Result
            };
        }
    }
}
