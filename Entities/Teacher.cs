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
    [Table("Teachers")]
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50), IndexColumn]
        public string Lastname { get; set; }

        [Required, MaxLength(50), IndexColumn]
        public string Firstname { get; set; }

        [Required, MaxLength(50), IndexColumn]
        public string Patronymic { get; set; }

        [Required, Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [Required, MaxLength(50)]
        public string Rank { get; set; }

        [Required, MaxLength(50)]
        public string Position { get; set; }

        [Required]
        public int CathedraId { get; set; }

        [ForeignKey("CathedraId")]
        public Cathedra Cathedra { get; set; }

        [InverseProperty("Teacher")]
        public List<Aspirant> Aspirants { get; set; }
    }
}
