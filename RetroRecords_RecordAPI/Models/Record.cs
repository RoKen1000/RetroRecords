using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetroRecords_RecordAPI.Models
{
    public class Record
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TimeSpan RunTime { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Country { get; set; }
        public string Label { get; set; }

    }
}
