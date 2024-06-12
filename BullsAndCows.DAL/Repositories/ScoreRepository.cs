using BullsAndCows.DAL.Entities;
using BullsAndCows.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BullsAndCows.DAL.Repositories
{
    public class ScoreRepository(BullsAndCowsContext context) : IRepository<ScoreRecord>
    {
        private readonly BullsAndCowsContext _context = context;
        public async Task<ScoreRecord> Add(ScoreRecord entity)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<ScoreRecord>> GetAll() => await _context.ScoreRecords.ToListAsync().ConfigureAwait(false);
    }
}
