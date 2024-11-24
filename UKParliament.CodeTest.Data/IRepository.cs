namespace UKParliament.CodeTest.Data;

public interface IRepository<T> where T : BaseEntity
{
    Task<int> CreateAsync(T entity);
    T? GetById(int id);
    List<T> List();
    Task UpdateAsync(T entity);
}