using Warehouse.Domain.Entities;

namespace Warehouse.App.Services
{
    public interface ICurrentUserService
    {
        User User { get; }
        public string UserId { get; }
    }
}