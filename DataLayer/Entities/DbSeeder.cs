using DataLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    public class DbSeeder
    {
        public static void Execute(ModelBuilder builder)
        {
            builder.Entity<Users>()
                .HasData(new List<Users>
                {
                    new Users
                    {
                        Id = 1,
                        UserName = "Ivan Bakotin",
                        Password = "12345",
                    }
                });

            builder.Entity<Comments>()
                .HasData(new List<Comments>
                {
                    new Comments
                    {
                        Id = 1
                    }
                });

            builder.Entity<Resources>()
                .HasData(new List<Resources>
                {
                    new Resources
                    {
                        Id = 1
                    }
                });
        }
    }
}
