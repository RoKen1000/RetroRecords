using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroRecords_RecordAPI.Data;
using RetroRecords_RecordAPI.Models;
using RetroRecords_RecordAPI.Models.Dto;

namespace RetroRecords_RecordAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordAPIController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<RecordDTO>> GetRecords()
        {
            return Ok(RecordTempDb.RecordList);
        }

        [HttpGet("{id:int}", Name = "GetRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RecordDTO> GetRecord(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            RecordDTO record = RecordTempDb.RecordList.FirstOrDefault(r => r.Id == id);

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
            if(RecordTempDb.RecordList.FirstOrDefault(r => r.Name.ToLower() == newRecord.Name.ToLower()) != null)
            {
                ModelState.AddModelError("RecordAlreadyExistsError", "Record already exists!");
                return BadRequest(ModelState);
            }

            if(newRecord == null || newRecord.Id == 0)
            {
                return BadRequest();
            }

            newRecord.Id = RecordTempDb.RecordList.OrderByDescending(r => r.Id).FirstOrDefault().Id + 1;

            RecordTempDb.RecordList.Add(newRecord);

            return CreatedAtRoute("GetRecord", new {id = newRecord.Id},newRecord);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteRecord(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var recordToBeDeleted = RecordTempDb.RecordList.FirstOrDefault(r => r.Id == id);

            if(recordToBeDeleted == null)
            {
                return NotFound();
            }

            RecordTempDb.RecordList.Remove(recordToBeDeleted);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateRecord(int id, [FromBody]RecordDTO recordUpdate)
        {
            if(recordUpdate == null || id != recordUpdate.Id)
            {
                return BadRequest();
            }

            var recordInDb = RecordTempDb.RecordList.FirstOrDefault(r => r.Id == id);

            recordInDb.Name = recordUpdate.Name;
            recordInDb.Artist = recordUpdate.Artist;
            recordInDb.RunTimeString = recordUpdate.RunTimeString;

            return NoContent();
        }
    }
}
