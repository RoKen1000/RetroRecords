
using System.Linq.Expressions;

namespace RetroRecords.Repository.IRepository
{
    public interface IRepository<Record>
    {
        IEnumerable<Record> GetAll();
        Record Get(Expression<Func<Record, bool>> filter);
        void Add(Record record);
        void Update(Record record);
        void Delete(Record record);

    }
}
