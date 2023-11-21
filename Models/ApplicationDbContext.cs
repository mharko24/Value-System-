using ContractManagementValue.Data;
using Microsoft.EntityFrameworkCore;

namespace ContractManagementValue.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<SiteInstruction> SiteInstructions { get; set; }
        public virtual DbSet<PotentialClaim>PotentialClaims { get; set; }
        public virtual DbSet<FileUpload>FileUploads { get; set; }
        public virtual DbSet<EOTClaim>EOTClaims { get; set; }
        public virtual DbSet<UserApp> UserApps { get; set; }


        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<UserProject> UserProjects { get; set; }
    }
}
