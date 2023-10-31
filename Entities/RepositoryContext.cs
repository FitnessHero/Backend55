using Microsoft.EntityFrameworkCore;
using Entities.Models.Database;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<User>? Users { get; set; }
		public DbSet<FoodDiary>? CalorieCounters { get; set; }
		public DbSet<Food>? Foods { get; set; }
		public DbSet<Nutrient>? FoodItems { get; set; }
        
	}
}
