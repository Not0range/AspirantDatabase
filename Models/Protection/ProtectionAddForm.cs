using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace AspirantDatabase.Models
{
    public class ProtectionAddForm
    {
        [Required]
        public int AspirantId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(150)]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(50)]
        public string University { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Commission { get; set; }

        [Required]
        public int Result { get; set; }

        public Protection GetProtection()
        {
            return new Protection
            {
                AspirantId = AspirantId,
                DateTime = DateTime,
                City = City,
                University = University,
                Commission = Commission,
                Result = Result
            };
        }
    }
}
