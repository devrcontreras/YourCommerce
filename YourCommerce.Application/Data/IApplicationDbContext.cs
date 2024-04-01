using Microsoft.EntityFrameworkCore;
using YourCommerce.Domain.Customers;

namespace YourCommerce.Application.Data;

public interface IApplicationDbContext
{

    DbSet<Customer> Customers { get; set; }

    Task<int> SaveChangesAsync (CancellationToken cancellationToken = default );

}