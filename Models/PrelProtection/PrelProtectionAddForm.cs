using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class PrelProtectionAddForm
    {
        [Required]
        public int AspirantId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Commission { get; set; }

        [Required]
        public bool Allowmance { get; set; }

        public PrelProtection GetProtection()
        {
            return new PrelProtection
            {
                AspirantId = AspirantId,
                DateTime = DateTime,
                Commission = Commission,
                Allowmance = Allowmance
            };
        }
    }
}
