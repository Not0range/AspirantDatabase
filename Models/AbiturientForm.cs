using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class AbiturientForm
    {
        [Required]
        public int[] Specialties { get; set; }
    }
}
