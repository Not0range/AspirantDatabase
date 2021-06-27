using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class PassingExamEditForm
    {
        public int? ExamId { get; set; }

        public int? Id { get; set; }

        public int? Result { get; set; }

        public PassingEntryExam GetPassingEntry(PassingEntryExam passing)
        {
            if (ExamId.HasValue)
                passing.ExamId = ExamId.Value;
            if (Id.HasValue)
                passing.AbiturientId = Id.Value;
            if (Result.HasValue)
                passing.Result = Result.Value;
            return passing;
        }

        public PassingExam GetPassingExam(PassingExam passing)
        {
            if(ExamId.HasValue)
                passing.ExamId = ExamId.Value;
            if (Id.HasValue)
                passing.AspirantId = Id.Value;
            if (Result.HasValue)
                passing.Result = Result.Value;
            return passing;
        }

        public PassingCandidateExam GetPassingCandidate(PassingCandidateExam passing)
        {
            if (ExamId.HasValue)
                passing.ExamId = ExamId.Value;
            if (Id.HasValue)
                passing.AspirantId = Id.Value;
            if (Result.HasValue)
                passing.Result = Result.Value;
            return passing;
        }
    }
}
