using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class TeacherEditForm
    {
        [MaxLength(50)]
        public string Lastname { get; set; }

        [MaxLength(50)]
        public string Firstname { get; set; }

        [MaxLength(50)]
        public string Patronymic { get; set; }

        public DateTime? Birthdate { get; set; }

        [MaxLength(50)]
        public string Rank { get; set; }

        [MaxLength(50)]
        public string Position { get; set; }

        public int? CathedraId { get; set; }

        public Teacher GetTeacher(Teacher teacher)
        {
            if (!string.IsNullOrWhiteSpace(teacher.Lastname))
                teacher.Lastname = Lastname;
            if (!string.IsNullOrWhiteSpace(teacher.Lastname))
                teacher.Firstname = Firstname;
            if (!string.IsNullOrWhiteSpace(teacher.Lastname))
                teacher.Patronymic = Patronymic;
            if (Birthdate.HasValue)
                teacher.BirthDate = Birthdate.Value;
            if (!string.IsNullOrWhiteSpace(teacher.Lastname))
                teacher.Rank = Rank;
            if (!string.IsNullOrWhiteSpace(teacher.Lastname))
                teacher.Position = Position;
            if(CathedraId.HasValue)
                teacher.CathedraId = CathedraId.Value;
            
            return teacher;
        }
    }
}
