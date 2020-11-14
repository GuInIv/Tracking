using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tracking.DataAccessLayer
{
    public class TrackingData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string CipherPoints { get; set; }

        public User User { get; set; }

    }
}