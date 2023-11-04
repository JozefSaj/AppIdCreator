using AppIdCreatorTool.Model;
using Microsoft.EntityFrameworkCore;

namespace AppIdCreatorTool.Dal
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public virtual DbSet<LicenseTemplateModel> LicenseTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LicenseTemplateModel>().ToTable("LicenseTemplate");
            base.OnModelCreating(modelBuilder);
        }

    }
}
