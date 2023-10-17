using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RetroRecords_RecordAPI.Models.Dto
{
    public class RecordDTO
    {
        //[Required]
        //public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [MaxLength(40)]
        public string Artist { get; set; }
        public int[] RunTimeArray { get; set; } = new int[3];
        public string Genre { get; set; }
        public int[] ReleaseDateArray { get; set; } = new int[3];
        public string Label { get; set; }
    }
}
