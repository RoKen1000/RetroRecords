using System.ComponentModel.DataAnnotations;

namespace RetroRecords_RecordAPI.Models.Dto
{
    public class RecordDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
    }
}
