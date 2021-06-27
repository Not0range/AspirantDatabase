using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class ConferenceAddForm
    {
        public int? AspirantId { get; set; }

        [Required, MaxLength(50)]
        public string Subject { get; set; }

        [Required, MaxLength(150)]
        public string Place { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public Conference GetConference(bool aspirant = false)
        {
            if (!aspirant && !AspirantId.HasValue)
                throw new InvalidOperationException();
            var conference = new Conference
            {
                Subject = Subject,
                Place = Place,
                DateTime = DateTime
            };
            if (aspirant)
            {
                conference.SubjectEdit = Subject;
                conference.PlaceEdit = Place;
                conference.DateTimeEdit = DateTime;
            }
            else
                conference.AspirantId = AspirantId.Value;
            return conference;
        }
    }
}
