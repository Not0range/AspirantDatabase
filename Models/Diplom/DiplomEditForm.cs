using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace AspirantDatabase.Models
{
    public class DiplomEditForm
    {
        public int? AspirantId { get; set; }

        public DateTime? EndDate { get; set; }

        public int? SpecialtyId { get; set; }

        public int? CountSatisfactoryMarks { get; set; }

        public Diplom GetDiplom(Diplom diplom)
        {
            if(AspirantId.HasValue)
                diplom.AspirantId = AspirantId.Value;
            if(EndDate.HasValue)
                diplom.EndDate = EndDate.Value;
            if (SpecialtyId.HasValue)
                diplom.SpecialtyId = SpecialtyId.Value;
            if (CountSatisfactoryMarks.HasValue)
                diplom.CountSatisfactoryMarks = CountSatisfactoryMarks.Value;
            return diplom;
        }
    }
}
