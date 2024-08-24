namespace ArgoPantry.Domain.Services;

public interface IDataService<T> {
    Task<T> Create(T entity);
    Task<T?> Get(int id);
    Task<T?> GetByColumn<TValue>(TValue value, string column);
    Task<IEnumerable<T>?> GetAll();
    Task<T> Update(int id, T entity);
    Task<bool> Delete(int id);
}