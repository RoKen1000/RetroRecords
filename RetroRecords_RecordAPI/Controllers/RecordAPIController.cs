using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RetroRecords_RecordAPI.Data;
using RetroRecords_RecordAPI.Data.DataContext;
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

        public RecordAPIController(ILogger<RecordAPIController> logger, ApiDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Record>> GetRecords()
        {
            _logger.LogInformation("Getting all records...");

            return Ok(_db.Records.ToList());
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

            Record record = _db.Records.FirstOrDefault(r => r.Id == id);

            if(record == null)
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
            if(_db.Records.FirstOrDefault(r => r.Name.ToLower() == newRecord.Name.ToLower()) != null)
            {
                ModelState.AddModelError("RecordAlreadyExistsError", "Record already exists!");
                return BadRequest(ModelState);
            }

            if (newRecord == null || newRecord.Id == 0)
            {
                return BadRequest();
            }

            Record model = new Record()
            {
                Name = newRecord.Name,
                Artist = newRecord.Artist,
                CreatedAt = DateTime.Now,
                RunTime = new TimeSpan(newRecord.RunTimeArray[0], newRecord.RunTimeArray[1], newRecord.RunTimeArray[2]),
                Genre = newRecord.Genre,
                ReleaseDate = new DateTime(newRecord.ReleaseDateArray[0],
                newRecord.ReleaseDateArray[1],
                newRecord.ReleaseDateArray[2]),
                Label = newRecord.Label
            };

            _db.Records.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetRecord", new { id = newRecord.Id }, model);
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

            var recordToBeDeleted = _db.Records.FirstOrDefault(r => r.Id == id);

            if (recordToBeDeleted == null)
            {
                return NotFound();
            }

            _db.Records.Remove(recordToBeDeleted);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateRecord(int id, [FromBody] RecordDTO recordUpdate)
        {
            if (recordUpdate == null || id != recordUpdate.Id)
            {
                return BadRequest();
            }

            var recordInDb = _db.Records.FirstOrDefault(r => r.Id == id);

            recordInDb.Name = recordUpdate.Name;
            recordInDb.Artist = recordUpdate.Artist;
            recordInDb.UpdatedAt = DateTime.Now;
            recordInDb.RunTime = new TimeSpan(recordUpdate.RunTimeArray[0],
                recordUpdate.RunTimeArray[1],
                recordUpdate.RunTimeArray[2]);
            recordInDb.Genre = recordUpdate.Genre;
            recordInDb.ReleaseDate = new DateTime(recordUpdate.ReleaseDateArray[0],
                recordUpdate.ReleaseDateArray[1],
                recordUpdate.ReleaseDateArray[2]);
            recordInDb.Label = recordUpdate.Label;

            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PatchRecord(int id, JsonPatchDocument<Record> patch)
        {
            if (patch == null || id == 0)
            {
                return BadRequest();
            }

            var recordInDb = _db.Records.FirstOrDefault(r => r.Id == id);

            if (recordInDb == null)
            {
                return BadRequest();
            }

            patch.ApplyTo(recordInDb, ModelState);
            _db.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
