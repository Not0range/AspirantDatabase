using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class PublicationAddForm
    {
        public int? AspirantId { get; set; }

        [Required, MaxLength(50)]
        public string Subject { get; set; }

        [Required, MaxLength(50)]
        public string Journal { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Page { get; set; }

        public Publication GetPublication(bool aspirant = false)
        {
            if (!aspirant && !AspirantId.HasValue)
                throw new InvalidOperationException();
            var publication = new Publication
            {
                Subject = Subject,
                Journal = Journal,
                Number = Number,
                Date = Date,
                Page = Page
            };
            if (aspirant)
            {
                publication.SubjectEdit = Subject;
                publication.JournalEdit = Journal;
                publication.NumberEdit = Number;
                publication.DateEdit = Date;
                publication.PageEdit = Page;
            }
            else
                publication.AspirantId = AspirantId.Value;
            return publication;
        }
    }
}
