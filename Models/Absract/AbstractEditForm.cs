using AspirantDatabase.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Models
{
    public class AbstractEditForm
    {
        public int? AspirantId { get; set; }

        [MaxLength(50)]
        public string Subject { get; set; }

        [MaxLength(150)]
        public string Place { get; set; }

        public DateTime? DateTime { get; set; }

        public Abstract GetAbstract(Abstract @abstract, bool aspirant = false)
        {
            if (aspirant)
            {
                bool edit = @abstract.Subject == @abstract.SubjectEdit &&
                    @abstract.Place == @abstract.PlaceEdit &&
                    @abstract.DateTime == @abstract.DateTimeEdit && string.IsNullOrWhiteSpace(@abstract.Reason);
                if (!string.IsNullOrWhiteSpace(Subject))
                {
                    @abstract.SubjectEdit = Subject;
                    if (edit)
                        @abstract.Subject = Subject;
                }
                if (!string.IsNullOrWhiteSpace(Place))
                {
                    @abstract.PlaceEdit = Place;
                    if (edit)
                        @abstract.Place = Place;
                }
                if (DateTime.HasValue)
                {
                    @abstract.DateTimeEdit = DateTime.Value;
                    if (edit)
                        @abstract.DateTime = DateTime.Value;
                }
                @abstract.Reason = null;
            }
            else
            {
                if (AspirantId.HasValue)
                    @abstract.AspirantId = AspirantId.Value;
                if (!string.IsNullOrWhiteSpace(Subject))
                    @abstract.Subject = Subject;
                if (!string.IsNullOrWhiteSpace(Place))
                    @abstract.Place = Place;
                if (DateTime.HasValue)
                    @abstract.DateTime = DateTime.Value;

                @abstract.SubjectEdit = null;
                @abstract.PlaceEdit = null;
                @abstract.DateTimeEdit = null;
                @abstract.Reason = null;
            }
            return @abstract;
        }
    }
}
