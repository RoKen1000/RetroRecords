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
    }
}
