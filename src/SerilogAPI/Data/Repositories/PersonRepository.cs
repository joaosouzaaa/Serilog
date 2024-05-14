using Microsoft.EntityFrameworkCore;
using SerilogAPI.Data.DatabaseContexts;
using SerilogAPI.Data.Repositories.BaseRepositories;
using SerilogAPI.Entities;
using SerilogAPI.Interfaces.Repositories;

namespace SerilogAPI.Data.Repositories;

public sealed class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    public PersonRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Task AddAsync(Person person)
    {
        DbContextSet.Add(person);

        return _dbContext.SaveChangesAsync();
    }

    public Task UpdateAsync(Person person)
    {
        _dbContext.Entry(person).State = EntityState.Modified;

        return _dbContext.SaveChangesAsync();
    }

    public Task ExistsAsync(int id) =>
        DbContextSet.AnyAsync(p => p.Id == id);

    public async Task DeleteAsync(int id)
    {
        var person = await DbContextSet.FirstOrDefaultAsync(p => p.Id == id);

        DbContextSet.Remove(person!);

        await _dbContext.SaveChangesAsync();
    }

    public Task<Person?> GetByIdAsync(int id, bool asNoTracking)
    {
        var query = (IQueryable<Person>)DbContextSet;

        if (asNoTracking)
        {
            query = DbContextSet.AsNoTracking();
        }

        return query.FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<List<Person>> GetAllAsync() =>
        DbContextSet.AsNoTracking()
                    .ToListAsync();
}
