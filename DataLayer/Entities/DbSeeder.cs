using DataLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities
{
    public class DbSeeder
    {
        public static void Execute(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasData(new List<User>
                {
                    new User
                    {
                        UserId = 1,
                        UserName = "Jure Juric",
                        Password = "12345",
                        Role = "Organizator",
                        RepPoints = 10000,
                        IsTrusted = true,
                    },

                    new User
                    {
                        UserId = 2,
                        UserName = "Ivan Ivic",
                        Password = "Lozinka",
                        Role = "Intern",
                        RepPoints = 1,
                        IsTrusted = false,
                    }
                });
        }
    }
}

