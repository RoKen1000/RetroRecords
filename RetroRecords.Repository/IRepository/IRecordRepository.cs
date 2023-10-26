
using RetroRecords_RecordAPI.Models;
using RetroRecords_RecordAPI.Models.Dto;
using System.Linq.Expressions;

namespace RetroRecords.Repository.IRepository
{
    public interface IRecordRepository
    {
        IEnumerable<Record> GetAll();
        Record Get(int id);
        Record Add(RecordDTO newRecord);
        bool CheckRecordExists(string name);
        void Update(RecordDTO recordUpdate, Record recordInDb);
        void Delete(Record record);
        void Save();
    }
}
