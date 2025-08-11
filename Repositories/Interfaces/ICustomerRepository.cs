using DotNetCoreApiTemplate.DTOs;

namespace DotNetCoreApiTemplate.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<CustomerOrderDto>> GetCustomerOrdersAsync(int customerId);
    }
}
