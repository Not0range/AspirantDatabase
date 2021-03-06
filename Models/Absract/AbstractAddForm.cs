using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class AbstractAddForm
    {
        public int? AspirantId { get; set; }

        [Required, MaxLength(50)]
        public string Subject { get; set; }

        [Required, MaxLength(150)]
        public string Place { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public Abstract GetAbstract(bool aspirant = false)
        {
            if (!aspirant && !AspirantId.HasValue)
                throw new InvalidOperationException();
            var @abstract = new Abstract
            {
                Subject = Subject,
                Place = Place,
                DateTime = DateTime
            };
            if (aspirant)
            {
                @abstract.SubjectEdit = Subject;
                @abstract.PlaceEdit = Place;
                @abstract.DateTimeEdit = DateTime;
            }
            else
                @abstract.AspirantId = AspirantId.Value;
            return @abstract;
        }
    }
}
