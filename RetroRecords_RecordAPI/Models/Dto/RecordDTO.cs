using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RetroRecords_RecordAPI.Models.Dto
{
    public class RecordDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [MaxLength(40)]
        public string Artist { get; set; }
        [HiddenInput]
        public string RunTimeString { get; set; }
        [HiddenInput]
        public string ReleaseDateString { get; set; }
        public string Genre { get; set; }
        public string Country { get; set; }
        public string Label { get; set; }
    }
}
