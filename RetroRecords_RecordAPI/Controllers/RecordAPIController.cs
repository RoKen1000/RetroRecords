using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroRecords_RecordAPI.Models;
using RetroRecords_RecordAPI.Models.Dto;

namespace RetroRecords_RecordAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<RecordDTO> GetRecords()
        {

            return new List<RecordDTO>{
                new RecordDTO{Id = 1, Name = "Aladdin Sane"},
                new RecordDTO{Id = 2, Name = "Station To Station"}
            };
        }
    }
}
