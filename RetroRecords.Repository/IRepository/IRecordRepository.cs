
using RetroRecords_RecordAPI.Models;
using RetroRecords_RecordAPI.Models.Dto;
using System.Linq.Expressions;

namespace RetroRecords.Repository.IRepository
{
    public interface IRecordRepository
    {
        IEnumerable<Record> GetAll();
        IQueryable<Record> Get(Expression<Func<Record, bool>> filter);
        Record Add(RecordDTO newRecord);
        void UpdatePut(RecordDTO recordUpdate, Record recordInDb);
        void UpdatePatch(int id, RecordDTO recordInDb);
        void Delete(Record record);
        void Save();
    }
}
