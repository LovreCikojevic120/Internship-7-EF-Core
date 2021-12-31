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
                        UserName = "Ivan Bakotin",
                        Password = "12345",
                        Role = "Admin",
                        RepPoints = 1,
                        IsTrusted = true,
                    }
                });

            builder.Entity<Comment>()
                .HasData(new List<Comment>
                {
                    new Comment
                    {
                        CommentId = 1,
                        CommentContent = "Fritule su bezveze",
                        NumberOfLikes = 4,
                        NumberOfDislikes = 4,
                        ParentCommentId = null,
                        TimeOfPosting = new DateTime(2021,10,10),
                        CommentOwnerId = 1,
                        ResourceId = 1,
                    }
                });

            builder.Entity<Resource>()
                .HasData(new List<Resource>
                {
                    new Resource
                    {
                        ResourceId = 1,
                        ResourceContent = "Fritule su najbolje slatko",
                        NumberOfReplys = 7,
                        NumberOfLikes = 7,
                        NumberOfDislikes = 7,
                        NameTag = "Dev",
                        ResourceOwnerId = 1,
                    }
                });
        }
    }
}
