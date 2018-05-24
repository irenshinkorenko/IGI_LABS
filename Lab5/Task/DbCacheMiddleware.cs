using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Application.Models;

namespace Lab4
{
    public class DbCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _memoryCache;

        public DbCacheMiddleware(RequestDelegate next, IMemoryCache memCache)
        {
            this._next = next;
            this._memoryCache = memCache;
        }

        public async Task Invoke(HttpContext context, HospitalContext db)
        {
            string path = context.Request.Path.Value.ToLower();
            object model = db.Departments.Last();
           
            var modelTmp = model;
            if (!_memoryCache.TryGetValue(path, out modelTmp))
            {
                _memoryCache.Set(path, model,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            }

            await _next.Invoke(context);
        }
    }

    public static class DbCacheExtensions
    {
        public static IApplicationBuilder UseLastElementCache(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DbCacheMiddleware>();
        }
    }
}
