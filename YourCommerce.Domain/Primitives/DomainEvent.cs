using System.Transactions;
using MediatR;

namespace YourCommerce.Domain.Primitives;

public record DomainEvent(Guid Id) : INotification;