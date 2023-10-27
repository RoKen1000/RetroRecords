using RetroRecords.DataAccess.DataContext;
using RetroRecords.Repository.IRepository;
using RetroRecords_RecordAPI.Models;
using RetroRecords_RecordAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public IQueryable<Record> Get(Expression<Func<Record, bool>> filter)
        {
            //Record? record = _db.Records.AsNoTracking().FirstOrDefault(r => r.Id == id);

            var dbContext = _db;
            var dbSet = dbContext.Set<Record>();
            return dbSet.Where(filter);
        }

        public IEnumerable<Record> GetAll()
        {
            return _db.Records.ToList();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void UpdatePatch(int id, RecordDTO recordDTO)
        {
            Record recordModel = new Record()
            {
                Id = id,
                Name = recordDTO.Name,
                Artist = recordDTO.Artist,
                UpdatedAt = DateTime.Now,
                RunTime = new TimeSpan(recordDTO.RunTimeArray[0], recordDTO.RunTimeArray[1], recordDTO.RunTimeArray[2]),
                Genre = recordDTO.Genre,
                ReleaseDate = new DateTime(recordDTO.ReleaseDateArray[0], recordDTO.ReleaseDateArray[1], recordDTO.ReleaseDateArray[2]),
                Label = recordDTO.Label
            };

            _db.Records.Update(recordModel);
        }

        public void UpdatePut(RecordDTO recordUpdate, Record recordInDb)
        {
            recordInDb.Name = recordUpdate.Name;
            recordInDb.Artist = recordUpdate.Artist;
            recordInDb.UpdatedAt = DateTime.Now;
            recordInDb.RunTime = new TimeSpan(recordUpdate.RunTimeArray[0],
                recordUpdate.RunTimeArray[1],
                recordUpdate.RunTimeArray[2]);
            recordInDb.Genre = recordUpdate.Genre;
            recordInDb.ReleaseDate = new DateTime(recordUpdate.ReleaseDateArray[0],
                recordUpdate.ReleaseDateArray[1],
                recordUpdate.ReleaseDateArray[2]);
            recordInDb.Label = recordUpdate.Label;
        }
    }
}
