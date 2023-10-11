using RetroRecords_RecordAPI.Models.Dto;

namespace RetroRecords_RecordAPI.Data
{
    public class RecordTempDb
    {
        public static List<RecordDTO> RecordList = new List<RecordDTO>{
                new RecordDTO{Id = 1, Name = "Aladdin Sane"},
                new RecordDTO{Id = 2, Name = "Station To Station"}
            };
    }
}
