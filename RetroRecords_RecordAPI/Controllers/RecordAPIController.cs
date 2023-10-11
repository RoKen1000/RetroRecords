using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroRecords_RecordAPI.Models;

namespace RetroRecords_RecordAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Record> GetRecords()
        {

            return new List<Record>{
                new Record{Id = 1, Name = "Aladdin Sane"},
                new Record{Id = 2, Name = "Station To Station"}
            };
        }
    }
}
