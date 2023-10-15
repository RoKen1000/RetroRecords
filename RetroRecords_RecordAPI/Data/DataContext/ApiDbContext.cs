using Microsoft.EntityFrameworkCore;
using RetroRecords_RecordAPI.Models;

namespace RetroRecords_RecordAPI.Data.DataContext
{
    public class ApiDbContext : DbContext

    {
        public DbSet<Record> Records { get; set; }


    }
}
