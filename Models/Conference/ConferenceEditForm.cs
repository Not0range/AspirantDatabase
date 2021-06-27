using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class ConferenceEditForm
    {
        public int? AspirantId { get; set; }

        [MaxLength(50)]
        public string Subject { get; set; }

        [ MaxLength(150)]
        public string Place { get; set; }

        public DateTime? DateTime { get; set; }

        public Conference GetConference(Conference conference, bool aspirant = false)
        {
            if (aspirant)
            {
                bool edit = conference.Subject == conference.SubjectEdit &&
                    conference.Place == conference.PlaceEdit &&
                    conference.DateTime == conference.DateTimeEdit && string.IsNullOrWhiteSpace(conference.Reason);
                if (!string.IsNullOrWhiteSpace(Subject))
                {
                    conference.SubjectEdit = Subject;
                    if(edit)
                        conference.Subject = Subject;
                }
                if (!string.IsNullOrWhiteSpace(Place))
                {
                    conference.PlaceEdit = Place;
                    if (edit)
                        conference.Place = Place;
                }
                if (DateTime.HasValue)
                {
                    conference.DateTimeEdit = DateTime.Value;
                    if (edit)
                        conference.DateTime = DateTime.Value;
                }
                conference.Reason = null;
            }
            else
            {
                if (AspirantId.HasValue)
                    conference.AspirantId = AspirantId.Value;
                if (!string.IsNullOrWhiteSpace(Subject))
                    conference.Subject = Subject;
                if (!string.IsNullOrWhiteSpace(Place))
                    conference.Place = Place;
                if (DateTime.HasValue)
                    conference.DateTime = DateTime.Value;

                conference.SubjectEdit = null;
                conference.PlaceEdit = null;
                conference.DateTimeEdit = null;
                conference.Reason = null;
            }
            return conference;
        }
    }
}
