using KonstantinHVACweb.BusinessLogic.Models;
using KonstantinHVACweb.BusinessLogic.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace KonstantinHVACweb.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _cache;

        public UserService(
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = memoryCache;;
        }

        public UserModel User
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;

                var user = (UserModel)context.Items["UserIdentity"];
                if (user != null) return user;

                var sessionId = (string)context.Items["SessionId"];

                user = (UserModel)_cache.Get(sessionId);
                if (user == null)
                {
                    user = new UserModel(_httpContextAccessor);

                    _cache.Set(sessionId, user,
                        new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(1)));
                }

                context.Items.Remove("UserIdentity");
                context.Items.Add("UserIdentity", user);

                return user;
            }
        }
    }
}
