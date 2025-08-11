using DotNetCoreApiTemplate.Data;
using DotNetCoreApiTemplate.DTOs;
using DotNetCoreApiTemplate.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace DotNetCoreApiTemplate.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerOrderDto>> GetCustomerOrdersAsync(int customerId)
        {
            return await _context.CustomerOrders
                .FromSqlInterpolated($"EXEC GetCustomerOrders @CustomerId = {customerId}")
                .ToListAsync();
        }
    }
}
