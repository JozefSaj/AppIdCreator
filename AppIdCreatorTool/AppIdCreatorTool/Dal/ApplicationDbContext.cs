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
        public virtual DbSet<TemporaryLicenseTemplateModel> TemporaryLicenseTemplateModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LicenseTemplateModel>().ToTable("LicenseTemplate");
            modelBuilder.Entity<TemporaryLicenseTemplateModel>().ToTable("TemporaryLicenseTemplate");
            base.OnModelCreating(modelBuilder);
        }

    }
}
