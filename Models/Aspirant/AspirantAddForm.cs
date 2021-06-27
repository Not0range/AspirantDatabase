﻿using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class AspirantAddForm
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле 'Изучаемый язык' должно быть заполнено"), MaxLength(50)]
        public string ForeignLanguage { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле 'Форма обучения' должно быть заполнено"), MaxLength(30)]
        public string EnducationForm { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле 'Направление обучения' должно быть заполнено"), MaxLength(50)]
        public string EnducationDirection { get; set; }

        [Required(ErrorMessage = "Поле 'Специальность' должно быть заполнено")]
        public int SpecialtyId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле 'Приказ' должно быть заполнено"), MaxLength(50)]
        public string Decree { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле 'Тема диссертации' должно быть заполнено"), MaxLength(100)]
        public string DissertationTheme { get; set; }

        [Required(ErrorMessage = "Поле 'Руководитель' должно быть заполнено")]
        public int TeacherId { get; set; }

        public Aspirant GetAspirant()
        {
            return new Aspirant
            {
                ForeignLanguage = ForeignLanguage,
                EnducationForm = EnducationForm,
                EnducationDirection = EnducationDirection,
                SpecialtyId = SpecialtyId,
                Decree = Decree,
                DissertationTheme = DissertationTheme,
                TeacherId = TeacherId
            };
        }
    }
}
