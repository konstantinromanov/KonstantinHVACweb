using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using KonstantinHVACweb.BusinessLogic.Extensions;
using Microsoft.AspNetCore.Routing;
using KonstantinHVACweb.BusinessLogic.Services.Interface;
using KonstantinHVACweb.BusinessLogic.Models;
using System.Linq;

namespace KonstantinHVACweb.Middleware
{
    public class LocalizationManagementMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationManagementMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var routeData = context.GetRouteData();
            var routeValues = routeData.Values;

            if (routeValues.ContainsKey("language") && routeValues.ContainsKey("controller") && routeValues.ContainsKey("action"))
            {
                var language = routeValues["language"].MyToStringTrimLower();
                if (Language.AllLanguages.Select(z => z.IsoCode).Contains(language))
                    userService.User.Language = Language.GetLanguageByIsoCode(language);
                else
                    routeValues["language"] = userService.User.Language.IsoCode;
            };

            await _next(context);
        }
    }
}