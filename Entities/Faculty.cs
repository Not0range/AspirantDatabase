﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbelt.ComponentModel.DataAnnotations.Schema.V5;

namespace AspirantDatabase.Entities
{
    [Table("Faculties")]
    public class Faculty
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }
    }
}
