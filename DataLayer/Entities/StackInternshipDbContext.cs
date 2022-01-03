using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using DataLayer.Entities.Models;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataLayer.Entities
{
    public class StackInternshipDbContext : DbContext
    {
        public StackInternshipDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<UserResource> UserResources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserComment>().HasKey(uc => new { uc.UserId, uc.CommentId });
            modelBuilder.Entity<UserResource>().HasKey(ur => new { ur.UserId, ur.ResourceId });

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            DbSeeder.Execute(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }

    public class StackInternshipContextFactory : IDesignTimeDbContextFactory<StackInternshipDbContext>
    {
        public StackInternshipDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("App.config")
                .Build();

            configuration
                .Providers
                .First()
                .TryGet("connectionStrings:add:StackInternship:connectionString", out var connectionString);

            var options = new DbContextOptionsBuilder<StackInternshipDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new StackInternshipDbContext(options);
        }
    }
}