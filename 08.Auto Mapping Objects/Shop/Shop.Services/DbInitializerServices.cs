using System;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Services.Contracts;

namespace Shop.Services
{
    public class DbInitializerServices : IDbInitializerServices
    {
        private readonly ShopContext context;

        public DbInitializerServices(ShopContext context)
        {
            this.context = context;
        }

        public void InitializeDatabase()
        {
            this.context.Database.Migrate();
        }
    }
}
