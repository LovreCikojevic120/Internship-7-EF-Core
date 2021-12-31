using Microsoft.EntityFrameworkCore;
using System.Configuration;
using DataLayer.Entities;

namespace DomainLayer.Entities
{
    public static class DbContextFactory
    {
        public static StackInternshipDbContext GetStackInternshipDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlServer(ConfigurationManager.ConnectionStrings["StackInternship"].ConnectionString)
                .Options;

            return new StackInternshipDbContext(options);
        }
    }
}