using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

using ArgoPantry.Domain.Models.Entities;
using ArgoPantry.Domain.Services;

namespace ArgoPantry.EntityFramework.Services;
public class GenericDataService<T> : IDataService<T> where T : DomainObject {
    private readonly IServiceProvider _serviceProvider;

    public GenericDataService(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }

    private async Task ExecuteInScope(Func<ArgoPantryDbContext, Task> action) {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ArgoPantryDbContext>();
        await action(context);
    }

    public async Task<T> Create(T entity) {
        T result = null!;
        await ExecuteInScope(async context => {
            EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            result = createdResult.Entity;
        });
        return result;
    }

    public async Task<T?> Get(int id) {
        T? result = null;
        await ExecuteInScope(async context => {
            result = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
        });
        return result;
    }

    public async Task<T?> GetByColumn<TValue>(TValue value, string column) {
        T? result = null;
        await ExecuteInScope(async context => {
            result = await context.Set<T>().FirstOrDefaultAsync(
                e => EF.Property<TValue>(e, column)!.Equals(value)
            );
        });
        return result;
    }

    public async Task<IEnumerable<T>?> GetAll() {
        IEnumerable<T>? result = null;
        await ExecuteInScope(async context => {
            result = await context.Set<T>().ToListAsync();
        });
        return result;
    }

    public async Task<T> Update(int id, T entity) {
        T result = null!;
        await ExecuteInScope(async context => {
            entity.Id = id;
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            result = entity;
        });
        return result;
    }

    public async Task<bool> Delete(int id) {
        bool success = false;
        await ExecuteInScope(async context => {
            T? entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            if(entity != null) {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                success = true;
            }
        });
        return success;
    }
}