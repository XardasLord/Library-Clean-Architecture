using System;
using System.Linq;
using System.Security.Claims;
using Library.Application.Auth;
using Microsoft.AspNetCore.Http;

namespace Library.Infrastructure.Authorization
{
    public class TokenAuthInfo : ITokenAuthInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenAuthInfo(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        public long UserId => GetUserId();

        private long GetUserId()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;

            var userId = claims.SingleOrDefault(x => x.Type == ClaimTypes.Sid)?.Value 
                   ?? throw new ArgumentException("Cannot obtain UserId value from JWT token.");

            return long.Parse(userId);
        }
    }
}
