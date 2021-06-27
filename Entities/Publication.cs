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
    [Table("Publications")]
    public class Publication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AspirantId { get; set; }

        [ForeignKey("AspirantId")]
        public Aspirant Aspirant { get; set; }

        [Required, MaxLength(50)]
        public string Subject { get; set; }

        [Required, MaxLength(50)]
        public string Journal { get; set; }

        [Required]
        public int Number { get; set; }

        [Required, Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        public int Page { get; set; }

        [MaxLength(50)]
        public string SubjectEdit { get; set; }

        [MaxLength(50)]
        public string JournalEdit { get; set; }

        public int? NumberEdit { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateEdit { get; set; }

        public int? PageEdit { get; set; }

        [MaxLength(150)]
        public string Reason { get; set; }
    }
}
