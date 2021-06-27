using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace AspirantDatabase.Models
{
    public class PrelProtectionEditForm
    {
        public int? AspirantId { get; set; }
        public DateTime? DateTime { get; set; }

        public string Commission { get; set; }

        public bool? Allowmance { get; set; }

        public PrelProtection GetProtection(PrelProtection protection)
        {
            if(AspirantId.HasValue)
                protection.AspirantId = AspirantId.Value;
            if (DateTime.HasValue)
                protection.DateTime = DateTime.Value;
            if(!string.IsNullOrWhiteSpace(Commission))
                protection.Commission = Commission;
            if(Allowmance.HasValue)
                protection.Allowmance = Allowmance.Value;
            return protection;
        }
    }
}
