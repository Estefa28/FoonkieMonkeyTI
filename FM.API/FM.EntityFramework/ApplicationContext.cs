using FM.Domain.Models;
using FM.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FM.EntityFramework
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        #region Data Sets

        public DbSet<UserEntity> Users { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.ApplyConfiguration(new UserConfig());
        }
    }
}
