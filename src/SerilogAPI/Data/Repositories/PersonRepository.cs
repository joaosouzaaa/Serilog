using Microsoft.EntityFrameworkCore;
using SerilogAPI.Data.DatabaseContexts;
using SerilogAPI.Entities;
using SerilogAPI.Interfaces.Repositories;

namespace SerilogAPI.Data.Repositories;

public sealed class PersonRepository(
    AppDbContext dbContext) 
    : IPersonRepository, 
    IDisposable
{
    private DbSet<Person> PersonDbContextSet => dbContext.Set<Person>();

    public Task AddAsync(Person person)
    {
        PersonDbContextSet.Add(person);

        return dbContext.SaveChangesAsync();
    }

    public Task UpdateAsync(Person person)
    {
        dbContext.Entry(person).State = EntityState.Modified;

        return dbContext.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        PersonDbContextSet.AnyAsync(p => p.Id == id);

    public async Task DeleteAsync(int id)
    {
        var person = await PersonDbContextSet.FirstOrDefaultAsync(p => p.Id == id);

        PersonDbContextSet.Remove(person!);

        await dbContext.SaveChangesAsync();
    }

    public Task<Person?> GetByIdAsync(int id, bool asNoTracking)
    {
        var query = (IQueryable<Person>)PersonDbContextSet;

        if (asNoTracking)
        {
            query = PersonDbContextSet.AsNoTracking();
        }

        return query.FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<List<Person>> GetAllAsync() =>
        PersonDbContextSet.AsNoTracking()
                    .ToListAsync();

    public void Dispose()
    {
        dbContext.Dispose();

        GC.SuppressFinalize(this);
    }
}
