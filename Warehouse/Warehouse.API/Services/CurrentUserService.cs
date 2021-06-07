using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using Warehouse.App.Common.Interfaces;
using Warehouse.App.Services;
using Warehouse.Domain.Entities;

namespace Warehouse.API.Services
{

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public User User { get; set; }

        public string UserId
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }
    }
}