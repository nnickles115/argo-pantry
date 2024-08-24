using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

using ArgoPantry.Domain.Models.Entities;

namespace ArgoPantry.EntityFramework.Services; 
public class OrderDataService {
    private readonly IServiceProvider _serviceProvider;

    public OrderDataService(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }

    private async Task ExecuteInScope(Func<ArgoPantryDbContext, Task> action) {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ArgoPantryDbContext>();
        await action(context);
    }

    public async Task<Order> Create(Order newOrder, IEnumerable<Item> itemsToUpdate) {
        Order result = null!;
        await ExecuteInScope(async context => {
            context.Attach(newOrder.Student);
            context.Entry(newOrder.Student).State = EntityState.Unchanged;

            foreach(var item in itemsToUpdate) {
                context.Attach(item);
                context.Entry(item).State = EntityState.Modified;
            }

            EntityEntry<Order> createdResult = await context.Orders.AddAsync(newOrder);
            await context.SaveChangesAsync();
            result = createdResult.Entity;
        });
        return result;
    }

    public async Task<Order?> GetOrderWithItems(int orderId) {
        Order? result = null;
        await ExecuteInScope(async context => {
            result = await context.Orders
                .Include(o => o.Student)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item) // Include the items themselves
                .FirstOrDefaultAsync(o => o.Id == orderId);
        });
        return result;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersWithItems() {
        IEnumerable<Order>? result = null;
        await ExecuteInScope(async context => {
            result = await context.Orders
                .Include(o => o.Student)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item)
                .ToListAsync();
        });
        return result!;
    }

}