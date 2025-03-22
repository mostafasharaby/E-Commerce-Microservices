﻿using CartService.Infrastructure.Context;
using CartService.Infrastructure.Interfaces;
using CartService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CartService.Infrastructure
{
    public static class CartInfrastructureDependencyInjection
    {
        public static void AddCartInfrastructureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            // Db
            services.AddDbContext<CartContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("CartConnection"),
                    sqlServerOption => sqlServerOption.EnableRetryOnFailure());
            });

            //DI
            services.AddScoped<ICartRepository, CartRepository>();

        }
    }
}
