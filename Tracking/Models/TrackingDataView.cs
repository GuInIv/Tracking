using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tracking.Models
{
    public class TrackingDataView
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public byte Age { get; set; }

        public string PatternUserFirstName { get; set; }

        [Required]
        public string CipherKey { get; set; }

        [Required]
        public IEnumerable<Point> Points { get; set; }
    }
}
