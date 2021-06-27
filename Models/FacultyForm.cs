using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class FacultyForm
    {
        [Required, MaxLength(50)]
        public string Title { get; set; }
    }
}
