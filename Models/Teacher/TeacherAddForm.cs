using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class TeacherAddForm
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле 'Фамилия' должно быть заполнено"), MaxLength(50)]
        public string Lastname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле 'Имя' должно быть заполнено"), MaxLength(50)]
        public string Firstname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле 'Отчество' должно быть заполнено"), MaxLength(50)]
        public string Patronymic { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле 'Дата рождения' должно быть заполнено")]
        public DateTime Birthdate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле 'Научное звание' должно быть заполнено"), MaxLength(50)]
        public string Rank { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле 'Должность' должно быть заполнено"), MaxLength(50)]
        public string Position { get; set; }

        [Required(ErrorMessage = "Поле 'Кафедра' должно быть заполнено")]
        public int CathedraId { get; set; }

        public Teacher GetTeacher()
        {
            var p = new Teacher
            {
                Lastname = Lastname,
                Firstname = Firstname,
                Patronymic = Patronymic,
                BirthDate = Birthdate,
                Rank = Rank,
                Position = Position,
                CathedraId = CathedraId
            };
            return p;
        }
    }
}
