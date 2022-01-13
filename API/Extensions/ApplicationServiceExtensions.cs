using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

// Era destul de complicat sa folosesc API.Extensions ca nu gaseam, asa ca am lasat totul in Programs.cs  



namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        /*public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }*/
    }
}