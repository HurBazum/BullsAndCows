using BullsAndCows.DAL.Entities;
using BullsAndCows.Interfaces;

namespace BullsAndCows.BLL
{
    public class EntityService<T>(IRepository<ScoreRecord> repository) : IEntityService<T> where T : class, IEntity, new()
    {
        private readonly IRepository<ScoreRecord> _repository = repository;
        public async Task<T> Add(T entity)
        {
            ScoreRecord record = new() { Id = entity.Id, Player = entity.Player, Score = entity.Score };
            var result = await _repository.Add(record);
            return new T { Id = result.Id, Player = result.Player, Score = result.Score };
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            List<T> list = [];

            foreach(var item in await _repository.GetAll())
            {
                list.Add(new T { Id = item.Id, Player = item.Player, Score = item.Score });
            }

            return list;
        }
    }
}