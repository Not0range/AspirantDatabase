using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class ApproveObject
    {
        [Required]
        public int Id { get; set; }

        public string Reason { get; set; }
    }
}
