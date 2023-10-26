using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RetroRecords.DataAccess;
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
        private readonly IRecordRepository<Record> _recordRepository;

        public RecordAPIController(ILogger<RecordAPIController> logger, ApiDbContext db, IRecordRepository<Record> recordRepository)
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
            if(_db.Records.FirstOrDefault(r => r.Name.ToLower() == newRecord.Name.ToLower()) != null)
            {
                ModelState.AddModelError("RecordAlreadyExistsError", "Record already exists!");
                return BadRequest(ModelState);
            }

            if (newRecord == null)
            {
                return BadRequest();
            }

            Record recordModel = new Record()
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

            _db.Records.Update(recordModel);
            _db.SaveChanges();

            var id = _db.Records.Where(r => r.Name == newRecord.Name).Select(r => new {r.Id}).FirstOrDefault();

            return CreatedAtRoute("GetRecord", id, recordModel);
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateRecord(int id, [FromBody] RecordDTO recordUpdate)
        {
            if (recordUpdate == null)
            {
                return BadRequest();
            }

            var recordInDb = _db.Records.FirstOrDefault(r => r.Id == id);

            if(recordInDb == null)
            {
                return NotFound();
            }

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
        public IActionResult PatchRecord(int id, JsonPatchDocument<RecordDTO> patch)
        {
            if (patch == null || id == 0 || patch.Operations[0].path == "/Id")
            {
                return BadRequest();
            }

            var recordInDb = _db.Records.AsNoTracking().FirstOrDefault(r => r.Id == id);

            if (recordInDb == null)
            {
                return BadRequest();
            }

            if(patch.Operations[0].path == "/RunTimeArray")
            {
                patch.Operations[0].value = JsonConvert.DeserializeObject<int[]>(patch.Operations[0].value.ToString());
            }
            else if(patch.Operations[0].path == "/ReleaseDateArray")
            {
                patch.Operations[0].value = JsonConvert.DeserializeObject<int[]>(patch.Operations[0].value.ToString());
            }

            RecordDTO recordDTO = new RecordDTO()
            {
                Name = recordInDb.Name,
                Artist = recordInDb.Artist,
                RunTimeArray = new int[3] { recordInDb.RunTime.Hours, recordInDb.RunTime.Minutes, recordInDb.RunTime.Seconds },
                Genre = recordInDb.Genre,
                ReleaseDateArray = new int[3] {recordInDb.ReleaseDate.Year, recordInDb.ReleaseDate.Month, recordInDb.ReleaseDate.Day},
                Label = recordInDb.Label
            };

            patch.ApplyTo(recordDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Record recordModel = new Record()
            {
                Id = id,
                Name = recordDTO.Name,
                Artist = recordDTO.Artist,
                UpdatedAt = DateTime.Now,
                RunTime = new TimeSpan(recordDTO.RunTimeArray[0], recordDTO.RunTimeArray[1], recordDTO.RunTimeArray[2]),
                Genre = recordDTO.Genre,
                ReleaseDate = new DateTime(recordDTO.ReleaseDateArray[0], recordDTO.ReleaseDateArray[1], recordDTO.ReleaseDateArray[2]),
                Label = recordDTO.Label
            };


            _db.Records.Update(recordModel);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
