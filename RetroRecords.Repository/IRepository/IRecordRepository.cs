
using RetroRecords_RecordAPI.Models;
using RetroRecords_RecordAPI.Models.Dto;

namespace RetroRecords.Repository.IRepository
{
    public interface IRecordRepository
    {
        IEnumerable<Record> GetAll();
        Record Get(int id);
        Record Add(RecordDTO newRecord);
        bool CheckRecordExists(string name);
        void UpdatePut(RecordDTO recordUpdate, Record recordInDb);
        void UpdatePatch(int id, RecordDTO recordInDb);
        void Delete(Record record);
        void Save();
    }
}
