﻿using Microsoft.AspNetCore.Http;
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
        public ActionResult<IEnumerable<RecordDTO>> GetRecords()
        {

            return Ok(RecordTempDb.RecordList);
        }

        [HttpGet("id:int")]
        public ActionResult<RecordDTO> GetRecord(int id)
        {
            return Ok(RecordTempDb.RecordList.FirstOrDefault(r => r.Id == id));
        }
    }
}
