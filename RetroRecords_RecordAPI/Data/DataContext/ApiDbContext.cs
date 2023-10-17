using Microsoft.EntityFrameworkCore;
using RetroRecords_RecordAPI.Models;
using RetroRecords_RecordAPI.Models.Dto;

namespace RetroRecords_RecordAPI.Data.DataContext
{
    public class ApiDbContext : DbContext

    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            
        }

        public DbSet<Record> Records { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>().HasData(
                new Record { Id = 1, Name = "Aladdin Sane", Artist = "David Bowie", CreatedAt = DateTime.Now, RunTime = new TimeSpan(0, 41, 32), Genre = "Glam Rock", ReleaseDate = new DateTime(1973, 4, 19), Label = "RCA" },
                new Record { Id = 2, Name = "Station To Station", Artist = "David Bowie", CreatedAt = DateTime.Now, RunTime = new TimeSpan(0, 37, 54), Genre = "Rock", ReleaseDate = new DateTime(1976, 1, 23), Label = "RCA"}
            );
        }
    }
}
