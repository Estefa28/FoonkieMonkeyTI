﻿using FM.API.Configurations;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace FM.API.Filters
{
    /// <summary>
    /// Valida que las credenciales enviadas por el usuario coincidan con las establecidas
    /// </summary>
    public class ClientAuth : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var services = context.HttpContext.RequestServices;
            var auth = services.GetService<IOptions<Auth>>().Value;

            var headers = context.HttpContext.Request.Headers;
            if (!headers.ContainsKey("client_id") ||
                !headers.ContainsKey("client_secret") ||
                auth.ClientId != headers["client_id"] ||
                auth.ClientSecret != headers["client_secret"])
            {
                context.HttpContext.Response.StatusCode = 401;
                await context.HttpContext.Response.WriteAsync("No autorizado");
                return;
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
