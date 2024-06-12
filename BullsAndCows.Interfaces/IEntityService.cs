namespace BullsAndCows.Interfaces
{
    public interface IEntityService<T> where T : class, IEntity, new()
    {
        public Task<T> Add(T entity);
        public Task<IEnumerable<T>> GetAll();
    }
}