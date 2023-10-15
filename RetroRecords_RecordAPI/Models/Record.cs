using System.ComponentModel.DataAnnotations;

namespace RetroRecords_RecordAPI.Models
{
    public class Record
    {
        [Key]
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
