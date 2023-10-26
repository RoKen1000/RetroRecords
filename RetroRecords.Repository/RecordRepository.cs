using RetroRecords.DataAccess.DataContext;
using RetroRecords.Repository.IRepository;
using RetroRecords_RecordAPI.Models;

namespace RetroRecords.Repository
{
    public class RecordRepository : IRecordRepository<Record>
    {
        private readonly ApiDbContext _db;

        public RecordRepository(ApiDbContext db)
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

        public Record Get(int id)
        {
            Record record = _db.Records.FirstOrDefault(r => r.Id == id);

            return record;
        }

        public IEnumerable<Record> GetAll()
        {
            return _db.Records.ToList();
        }

        public void Update(Record record)
        {
            throw new NotImplementedException();
        }
    }
}
