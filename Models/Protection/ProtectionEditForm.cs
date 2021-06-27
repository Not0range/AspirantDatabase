using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class ProtectionEditForm
    {
        public int? AspirantId { get; set; }

        public DateTime? DateTime { get; set; }

        [MaxLength(150)]
        public string City { get; set; }

        [MaxLength(50)]
        public string University { get; set; }

        public string Commission { get; set; }

        public int? Result { get; set; }

        public Protection GetProtection(Protection protection)
        {
            if(AspirantId.HasValue)
            protection.AspirantId = AspirantId.Value;
            if (DateTime.HasValue)
                protection.DateTime = DateTime.Value;
            if (!string.IsNullOrWhiteSpace(City))
                protection.City = City;
            if (!string.IsNullOrWhiteSpace(University))
                protection.University = University;
            if (!string.IsNullOrWhiteSpace(Commission))
                protection.Commission = Commission;
                if (Result.HasValue)
                protection.Result = Result.Value;
            return protection;
        }
    }
}
