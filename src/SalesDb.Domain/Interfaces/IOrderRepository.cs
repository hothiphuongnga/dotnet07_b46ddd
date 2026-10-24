using SalesDb.Domain.Entities;

namespace SalesDb.Domain.Interfaces;

//IRepositoryBase CRUD
public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetAllAsync();
    Task AddAsync(Order order);
    // khong cho xoa , -> cancel
}