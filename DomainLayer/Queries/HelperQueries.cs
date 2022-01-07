using DataLayer.Entities;
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.Queries
{
    public class HelperQueries
    {
        private readonly StackInternshipDbContext dataBase;
        public HelperQueries()
        {
            dataBase = DbContextFactory.GetStackInternshipDbContext();
        }

        public int? GetResourceId(int commentId)
        {
            var comment = dataBase.Comments.Find(commentId);

            if (comment == null)return null;

            return comment.ResourceId;
        }

        public string? GetAuthorName(int? resourceId, int? commentId)
        {
            if (commentId is null) {
                var resource = dataBase.Resources.Find(resourceId);

                return dataBase.UserResources.Join(dataBase.Users, ur => ur.UserId, u => u.UserId, (ur, u) => new
                {
                    ur.ResourceId,
                    u.UserName,
                    u.UserId

                }).Where(r => r.ResourceId == resourceId && r.UserId==resource.ResourceOwnerId).Select(un => un.UserName).FirstOrDefault();
            }

            var comment = dataBase.Comments.Find(commentId);

            return dataBase.UserComments.Join(dataBase.Users, c => c.UserId, uc => uc.UserId, (c, uc) => new
            {
                c.CommentId,
                uc.UserName,
                uc.UserId

            }).Where(c => c.CommentId == commentId && c.UserId==comment.CommentOwnerId).Select(un => un.UserName).FirstOrDefault();
        }

        public bool IsResource(int entityId)
        {
           return dataBase.Resources.Where(t => t.NameTag == Enum.GetName(DatabaseStateTracker.currentResourceTag)).Any(r => r.ResourceId == entityId);
        }

        public bool IsComment(int entityId)
        {
            return dataBase.Comments.Join(dataBase.Resources, c => c.ResourceId, i => i.ResourceId, (c, i) => new
            {
                commentId = c.CommentId,
                resourceTag = i.NameTag
            })
                .Where(p => p.resourceTag == Enum.GetName(DatabaseStateTracker.currentResourceTag))
                .Any(c => c.commentId == entityId);
        }

        public bool IsViewed(int entityId)
        {
            if (IsResource(entityId))
            {
                return dataBase.UserResources
                    .Any(ur=>ur.UserId==DatabaseStateTracker.CurrentUser.UserId && ur.ResourceId==entityId);
            }

            return dataBase.UserComments
                .Any(uc => uc.UserId == DatabaseStateTracker.CurrentUser.UserId && uc.CommentId == entityId);
        }

        public bool IsVoted(int entityId)
        {
            if (IsResource(entityId))
            {
                return dataBase.UserResources.Any(ur => ur.UserId == DatabaseStateTracker.CurrentUser.UserId && ur.ResourceId == entityId && ur.IsVoted == true);
            }

            return dataBase.UserComments.Any(uc => uc.UserId == DatabaseStateTracker.CurrentUser.UserId && uc.CommentId == entityId && uc.IsVoted == true);
        }

        public bool IsCommented(int entityId)
        {
            if (IsResource(entityId))
            {
                return dataBase.UserResources
                .Any(ur => ur.UserId == DatabaseStateTracker.CurrentUser.UserId 
                && ur.ResourceId == entityId 
                && ur.IsCommented == true);
            }

            return dataBase.UserComments.Any(uc => uc.UserId == DatabaseStateTracker.CurrentUser.UserId 
            && uc.CommentId == entityId 
            && uc.IsCommented == true);
        }
    }
}
