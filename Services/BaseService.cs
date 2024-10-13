using ExamApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Services;

public abstract class BaseService<T> where T : class
{
    private readonly SchoolContext _context; 

    protected BaseService(SchoolContext context)
    {
        _context = context;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(object id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteByIdAsync(object id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}