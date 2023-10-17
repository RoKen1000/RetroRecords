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
                new Record { Id = 2, Name = "Station To Station", Artist = "David Bowie", CreatedAt = DateTime.Now, RunTime = new TimeSpan(0, 37, 54), Genre = "Rock", ReleaseDate = new DateTime(1976, 1, 23), Label = "RCA"},
                new Record { Id = 3, Name = "Purple Rain", Artist = "Prince", CreatedAt = DateTime.Now, RunTime = new TimeSpan(0, 43, 55), Genre = "Funk Pop", ReleaseDate =new DateTime(1984, 6, 25), Label = "Warner Bros."},
                new Record { Id = 4, Name = "Selling England By The Pound", Artist = "Genesis", CreatedAt = DateTime.Now, RunTime = new TimeSpan(0, 53, 44), Genre = "Prog Rock", ReleaseDate=new DateTime(1973, 9, 28), Label = "Charisma"},
                new Record { Id = 5, Name = "Breakfast In America", Artist = "Supertramp", CreatedAt = DateTime.Now, RunTime = new TimeSpan(0, 46, 6), Genre = "Pop", ReleaseDate = new DateTime(1979, 3, 16), Label = "A&M"},
                new Record { Id = 6, Name = "My Arms, Your Hearse", Artist = "Opeth", CreatedAt = DateTime.Now, RunTime = new TimeSpan(0, 52, 34), Genre = "Metal", ReleaseDate = new DateTime(1998, 8, 18), Label = "Candlelight"},
                new Record { Id = 7, Name = "Computer World", Artist = "Kraftwerk", CreatedAt = DateTime.Now, RunTime = new TimeSpan(0, 34, 25), Genre = "Electronica", ReleaseDate = new DateTime(1981, 5, 11), Label = "Kling Klang"},
                new Record { Id = 8, Name = "Master of Puppets", Artist = "Metallica", CreatedAt = DateTime.Now, RunTime = new TimeSpan(0, 54, 52), Genre = "Metal", ReleaseDate = new DateTime(1986, 3, 3), Label = "Elektra"}
            );
        }
    }
}
