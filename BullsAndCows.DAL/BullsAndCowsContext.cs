using BullsAndCows.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BullsAndCows.DAL
{
    public class BullsAndCowsContext : DbContext
    {
        public DbSet<ScoreRecord> ScoreRecords { get; set; }

        public BullsAndCowsContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var currDir = AppDomain.CurrentDomain.BaseDirectory;

            optionsBuilder.UseSqlServer($"Data Source=.\\SQLEXPRESS;Database={currDir}ScoreDB;Trusted_Connection=True;Trust Server Certificate=True;");
        }
    }
}