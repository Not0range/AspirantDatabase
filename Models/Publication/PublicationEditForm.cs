using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class PublicationEditForm
    {
        public int? AspirantId { get; set; }

        [MaxLength(50)]
        public string Subject { get; set; }

        [MaxLength(50)]
        public string Journal { get; set; }

        public int? Number { get; set; }

        public DateTime? Date { get; set; }

        public int? Page { get; set; }

        public Publication GetPublication(Publication publication, bool aspirant = false)
        {
            if (aspirant)
            {
                bool edit = publication.Subject == publication.SubjectEdit &&
                    publication.Journal == publication.JournalEdit &&
                    publication.Number == publication.NumberEdit &&
                    publication.Date == publication.DateEdit &&
                    publication.Page == publication.PageEdit && string.IsNullOrWhiteSpace(publication.Reason);
                if (!string.IsNullOrWhiteSpace(Subject))
                {
                    publication.SubjectEdit = Subject;
                    if (edit)
                        publication.Subject = Subject;
                }
                if (!string.IsNullOrWhiteSpace(Journal))
                {
                    publication.JournalEdit = Journal;
                    if (edit)
                        publication.Journal = Journal;
                }
                if (Number.HasValue)
                {
                    publication.NumberEdit = Number.Value;
                    if (edit)
                        publication.Number = Number.Value;
                }
                if (Date.HasValue)
                {
                    publication.DateEdit = Date.Value;
                    if (edit)
                        publication.Date = Date.Value;
                }
                if (Page.HasValue)
                {
                    publication.PageEdit = Page.Value;
                    if (edit)
                        publication.Page = Page.Value;
                }
                publication.Reason = null;
            }
            else
            {
                if (AspirantId.HasValue)
                    publication.AspirantId = AspirantId.Value;
                if (!string.IsNullOrWhiteSpace(Subject))
                    publication.Subject = Subject;
                if (!string.IsNullOrWhiteSpace(Journal))
                    publication.Journal = Journal;
                if (Number.HasValue)
                    publication.Number = Number.Value;
                if (Date.HasValue)
                    publication.Date = Date.Value;
                if (Page.HasValue)
                    publication.Page = Page.Value;

                publication.SubjectEdit = null;
                publication.JournalEdit = null;
                publication.NumberEdit = null;
                publication.DateEdit = null;
                publication.PageEdit = null;
                publication.Reason = null;
            }
            return publication;
        }
    }
}
