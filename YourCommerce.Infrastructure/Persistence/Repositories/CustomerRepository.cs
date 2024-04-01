using Microsoft.EntityFrameworkCore;
using YourCommerce.Application.Data;
using YourCommerce.Domain.Customers;

namespace YourCommerce.Infrastructure.Persistence.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly IApplicationDbContext _context;

    public CustomerRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task Add(Customer customer) => await _context.Customers.AddAsync(customer);

    public Task<Customer?> GetByIdAsync(CustomerId id) => _context.Customers.SingleOrDefaultAsync(e => e.Id == id);
    
}