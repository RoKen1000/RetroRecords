using RetroRecords.DataAccess.DataContext;
using RetroRecords.Repository.IRepository;
using RetroRecords_RecordAPI.Models;
using RetroRecords_RecordAPI.Models.Dto;

namespace RetroRecords.Repository
{
    public class RecordRepository : IRecordRepository
    {
        private readonly ApiDbContext _db;

        public RecordRepository(ApiDbContext db)
        {
            _db = db;
        }

        public Record Add(RecordDTO newRecord)
        {
            Record recordModel = new Record()
            {
                Name = newRecord.Name,
                Artist = newRecord.Artist,
                CreatedAt = DateTime.Now,
                RunTime = new TimeSpan(newRecord.RunTimeArray[0], newRecord.RunTimeArray[1], newRecord.RunTimeArray[2]),
                Genre = newRecord.Genre,
                ReleaseDate = new DateTime(newRecord.ReleaseDateArray[0],
                newRecord.ReleaseDateArray[1],
                newRecord.ReleaseDateArray[2]),
                Label = newRecord.Label
            };

            _db.Records.Update(recordModel);

            return recordModel;
        }

        public bool CheckRecordExists(string name)
        {
            Record? record = _db.Records.FirstOrDefault(r => r.Name.ToLower() == name.ToLower());

            if (record != null)
            {
                return true;
            }

            return false;
        }

        public void Delete(Record recordToBeDeleted)
        {
            _db.Records.Remove(recordToBeDeleted);
        }

        public Record Get(int id)
        {
            Record? record = _db.Records.FirstOrDefault(r => r.Id == id);

            return record;
        }

        public IEnumerable<Record> GetAll()
        {
            return _db.Records.ToList();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Record record)
        {
            throw new NotImplementedException();
        }
    }
}
