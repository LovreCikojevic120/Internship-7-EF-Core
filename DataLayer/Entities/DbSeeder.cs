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
                        CommentId = 3,
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
                        NumberOfLikes = 4,
                        NumberOfDislikes = 4,
                        NameTag = "Dev",
                        ResourceOwnerId = 1,
                    },
                    new Resource
                    {
                        ResourceId = 2,
                        NumberOfLikes = 4,
                        NumberOfDislikes = 4,
                        ResourceContent = "Krokanti su najbolje slatko",
                        NumberOfReplys = 0,
                        NameTag = "Generalno",
                        ResourceOwnerId = 1,
                    }
                });

            builder.Entity<UserResource>()
                .HasData(new List<UserResource>
                {
                    new UserResource
                    {
                        UserId = 1,
                        ResourceId = 1,
                    }
                });

            builder.Entity<UserComment>()
                .HasData(new List<UserComment>
                {
                    new UserComment
                    {
                        UserId= 1,
                        CommentId = 3,
                        IsVoted = false
                    }
                });
        }
    }
}

