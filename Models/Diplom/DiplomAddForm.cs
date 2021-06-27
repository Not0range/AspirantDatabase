using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace AspirantDatabase.Models
{
    public class DiplomAddForm
    {
        [Required]
        public int AspirantId { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int SpecialtyId { get; set; }

        [Required]
        public int CountSatisfactoryMarks { get; set; }

        public Diplom GetDiplom()
        {
            return new Diplom
            {
                AspirantId = AspirantId,
                EndDate = EndDate,
                SpecialtyId = SpecialtyId,
                CountSatisfactoryMarks = CountSatisfactoryMarks
            };
        }
    }
}
