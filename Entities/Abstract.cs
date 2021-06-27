using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbelt.ComponentModel.DataAnnotations.Schema.V5;

namespace AspirantDatabase.Entities
{
    [Table("Abstracts")]
    public class Abstract
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AspirantId { get; set; }

        [ForeignKey("AspirantId")]
        public Aspirant Aspirant { get; set; }

        [Required, MaxLength(50)]
        public string Subject { get; set; }

        [Required, MaxLength(150)]
        public string Place { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public string FileName { get; set; }

        public DateTime FileTime { get; set; }

        public byte[] Document { get; set; }

        [MaxLength(50)]
        public string SubjectEdit { get; set; }

        [MaxLength(150)]
        public string PlaceEdit { get; set; }

        public DateTime? DateTimeEdit { get; set; }

        public string FileNameEdit { get; set; }

        public DateTime? FileTimeEdit { get; set; }

        public byte[] DocumentEdit { get; set; }

        [MaxLength(150)]
        public string Reason { get; set; }
    }
}
