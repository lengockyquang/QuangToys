namespace QuangToys.Common.Interfaces
{
    public interface IRepository<T> where T: IEntity
    {
        public Task<bool> Add(T entity);
        public Task<bool> Update(T entity);
        public Task<bool> Delete(Guid id);
        public Task<T> Get(Guid id);
        public Task<List<T>> GetAll();
    }
}
