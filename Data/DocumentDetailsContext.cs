using Document_Saver.Models;
using Microsoft.EntityFrameworkCore;

namespace Document_Saver.Data
{
    public class DocumentDetailsContext : DbContext
    {
        public DocumentDetailsContext(DbContextOptions<DocumentDetailsContext> options) : base(options)
        {

        }
        public DbSet<User> UserDetails { get; set; }
        public DbSet<ProjectDetails> ProjectDetails { get; set; }

        public DbSet<Activities> Activity { get; set; }
        public DbSet<ProjectMember> ProjectMember { get; set; }
        public DbSet<Documents> Document { get; set; }

    }
}

