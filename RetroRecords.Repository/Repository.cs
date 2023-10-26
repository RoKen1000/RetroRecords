using RetroRecords.DataAccess.DataContext;
using RetroRecords.Repository.IRepository;
using RetroRecords_RecordAPI.Models;

namespace RetroRecords.Repository
{
    public class Repository : IRepository<Record>
    {
        private readonly ApiDbContext _db;

        public Repository(ApiDbContext db)
        {
            _db = db;
        }

        public void Add(Record record)
        {
            throw new NotImplementedException();
        }

        public void Delete(Record record)
        {
            throw new NotImplementedException();
        }

        public Record Get(System.Linq.Expressions.Expression<Func<Record, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Record> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Record record)
        {
            throw new NotImplementedException();
        }
    }
}
