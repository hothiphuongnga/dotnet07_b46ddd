namespace SalesDb.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using SalesDb.Domain.Entities;
using SalesDb.Domain.Interfaces;
using SalesDb.Infrastructure.Persistence;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _db;
    public OrderRepository(AppDbContext db)
    {
        _db = db;
    }
    public async Task AddAsync(Order order)
    {
        await _db.Orders.AddAsync(order);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _db.Orders.Include(o => o.OrderDetails).ToListAsync();
    }
    public async Task<Order?> GetByIdAsync(int id)=>
        await _db.Orders.Include(o => o.OrderDetails).SingleOrDefaultAsync(o => o.Id == id);
}