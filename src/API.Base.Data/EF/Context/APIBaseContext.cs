using Microsoft.EntityFrameworkCore;

namespace API.Base.Data.EF.Context
{
    public class APIBaseContext : DbContext
    {
        public APIBaseContext(DbContextOptions<APIBaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Model
            
            base.OnModelCreating(modelBuilder);
        }
    }
}