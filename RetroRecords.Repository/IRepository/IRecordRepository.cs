
using System.Linq.Expressions;

namespace RetroRecords.Repository.IRepository
{
    public interface IRecordRepository<Record>
    {
        IEnumerable<Record> GetAll();
        Record Get(int id);
        void Add(Record record);
        void Update(Record record);
        void Delete(Record record);

    }
}
