
using Microsoft.EntityFrameworkCore;

namespace UKParliament.CodeTest.Data;

public abstract class BaseRepository<T>(PersonManagerContext context) : IRepository<T> where T : BaseEntity
{
    protected abstract DbSet<T> DbSet { get; }

    public async Task<int> CreateAsync(T entity)
    {
        DbSet.Add(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public T? GetById(int id)
    {
        return DbSet.SingleOrDefault(p => p.Id == id);
    }

    public List<T> List()
    {
        return DbSet.ToList();
    }

    public async Task UpdateAsync(T entity)
    {
        var existingEntity =GetById(entity.Id);
        context.Entry(existingEntity).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
    }
}