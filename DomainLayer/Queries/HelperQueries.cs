using DataLayer.Entities;
using DomainLayer.Entities;

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
            return dataBase.Comments.Where(c => c.CommentId == commentId).Single().ResourceId;
        }

        public string? GetAuthorName(int? resourceId, int? commentId)
        {
            if (commentId is null)
                return dataBase.UserResources.Join(dataBase.Users, ur => ur.UserId, u => u.UserId, (ur, u) => new
                {
                    ur.ResourceId,
                    u.UserName

                }).Where(r => r.ResourceId == resourceId).Select(un=>un.UserName).FirstOrDefault();

            return dataBase.UserComments.Join(dataBase.Users, c => c.UserId, uc => uc.UserId, (c, uc) => new
            {
                c.CommentId,
                uc.UserName

            }).Where(c => c.CommentId == commentId).Select(un => un.UserName).FirstOrDefault();
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
