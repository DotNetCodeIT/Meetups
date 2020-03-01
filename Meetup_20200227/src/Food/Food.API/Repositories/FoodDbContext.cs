using Microsoft.EntityFrameworkCore;
using Food.API.Entities;

namespace Food.API.Repositories
{
    public class FoodDbContext : DbContext
    {
        public FoodDbContext(DbContextOptions<FoodDbContext> options)
           : base(options)
        {

        }

        public DbSet<FoodEntity> FoodItems { get; set; }

    }
}
