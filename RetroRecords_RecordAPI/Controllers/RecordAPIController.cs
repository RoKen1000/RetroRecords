using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RetroRecords.DataAccess.DataContext;
using RetroRecords.Repository.IRepository;
using RetroRecords_RecordAPI.Models;
using RetroRecords_RecordAPI.Models.Dto;

namespace RetroRecords_RecordAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordAPIController : ControllerBase
    {
        private readonly ILogger<RecordAPIController> _logger;
        private readonly ApiDbContext _db;
        private readonly IRecordRepository _recordRepository;

        public RecordAPIController(ILogger<RecordAPIController> logger, ApiDbContext db, IRecordRepository recordRepository)
        {
            _logger = logger;
            _db = db;
            _recordRepository = recordRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Record>> GetRecords()
        {
            _logger.LogInformation("Getting all records...");

            return Ok(_recordRepository.GetAll());
        }

        [HttpGet("{id:int}", Name = "GetRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Record> GetRecord(int id)
        {
            if(id == 0)
            {
                _logger.LogError($"Error retrieving record {id}. It does not exists.");
                return BadRequest();
            }

            Record record = _recordRepository.Get(id);

            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RecordDTO> CreateRecord([FromBody]RecordDTO newRecord)
        {
            if(_recordRepository.CheckRecordExists(newRecord.Name) == true)
            {
                ModelState.AddModelError("RecordAlreadyExistsError", "Record already exists!");
                return BadRequest(ModelState);
            }

            if (newRecord == null)
            {
                return BadRequest();
            }

            Record newRecordModel = _recordRepository.Add(newRecord);
            _recordRepository.Save();

            //var id = _db.Records.Where(r => r.Name == newRecord.Name).Select(r => new {r.Id}).FirstOrDefault();

            return CreatedAtRoute("GetRecord", newRecordModel.Id, newRecordModel);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteRecord(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var recordToBeDeleted = _recordRepository.Get(id);

            if (recordToBeDeleted == null)
            {
                return NotFound();
            }

            _recordRepository.Delete(recordToBeDeleted);
            _recordRepository.Save();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateRecord(int id, [FromBody] RecordDTO recordUpdate)
        {
            if (recordUpdate == null)
            {
                return BadRequest();
            }

            var recordInDb = _recordRepository.Get(id);

            if(recordInDb == null)
            {
                return NotFound();
            }

            _recordRepository.UpdatePut(recordUpdate, recordInDb);
            _recordRepository.Save();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PatchRecord(int id, JsonPatchDocument<RecordDTO> patch)
        {
            if (patch == null || id == 0 || patch.Operations[0].path == "/Id")
            {
                return BadRequest();
            }

            var recordInDb = _recordRepository.Get(id);

            if (recordInDb == null)
            {
                return BadRequest();
            }

            if (patch.Operations[0].path == "/RunTimeArray")
            {
                patch.Operations[0].value = JsonConvert.DeserializeObject<int[]>(patch.Operations[0].value.ToString());
            }
            else if (patch.Operations[0].path == "/ReleaseDateArray")
            {
                patch.Operations[0].value = JsonConvert.DeserializeObject<int[]>(patch.Operations[0].value.ToString());
            }

            RecordDTO recordDTO = new RecordDTO()
            {
                Name = recordInDb.Name,
                Artist = recordInDb.Artist,
                RunTimeArray = new int[3] { recordInDb.RunTime.Hours, recordInDb.RunTime.Minutes, recordInDb.RunTime.Seconds },
                Genre = recordInDb.Genre,
                ReleaseDateArray = new int[3] { recordInDb.ReleaseDate.Year, recordInDb.ReleaseDate.Month, recordInDb.ReleaseDate.Day },
                Label = recordInDb.Label
            };

            patch.ApplyTo(recordDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _recordRepository.UpdatePatch(id, recordDTO);
            _recordRepository.Save();

            return NoContent();
        }
    }
}
