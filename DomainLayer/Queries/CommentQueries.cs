using DataLayer.Entities;
using DataLayer.Entities.Models;
using DataLayer.Enums;
using DomainLayer.Entities;

namespace DomainLayer.Queries
{
    public class CommentQueries
    {
        private readonly StackInternshipDbContext dataBase;

        public CommentQueries()
        {
            dataBase = DbContextFactory.GetStackInternshipDbContext();
        }

        HelperQueries helpQuery = new();
        ReputationQueries reputationQuery = new();

        public bool LikeComment(int commentId)
        {
            if (helpQuery.IsVoted(commentId) || helpQuery.IsComment(commentId) is false)
                return false;

            var comment = dataBase.Comments.Single(r => r.CommentId == commentId);
            comment.NumberOfLikes++;
            dataBase.SaveChanges();

            reputationQuery.GiveUpvote(comment.CommentOwnerId);
            reputationQuery.GetUpvoteComment();

            return true;
        }

        public bool DislikeComment(int commentId)
        {
            if (helpQuery.IsVoted(commentId) || helpQuery.IsComment(commentId) is false)
                return false;

            var comment = dataBase.Comments.Find(commentId);
            comment.NumberOfDislikes++;
            dataBase.SaveChanges();

            reputationQuery.GiveDownvoteComment(comment.CommentOwnerId);
            reputationQuery.GetDownvotePoints();

            return true;
        }

        public bool ReplyOnComment(int commentId, string content)
        {
            if (helpQuery.IsCommented(commentId) || helpQuery.IsComment(commentId) || content.Count() is 0)
                return false;

            var resourceId = helpQuery.GetResourceId(commentId);

            if(resourceId == null)
                return false;

            AddComment((int)resourceId, commentId, content);

            dataBase.UserComments.Find(DatabaseStateTracker.CurrentUser.UserId, commentId).IsCommented = true;

            dataBase.SaveChanges();
            return true;
        }

        public void AddComment(int resourceId, int? parentCommentId, string commentContent)
        {
            var comment = new Comment(DatabaseStateTracker.GenerateEntityId(), commentContent, 0, 0, DatabaseStateTracker.CurrentUser.UserId, resourceId, parentCommentId);
            dataBase.Add(comment);
            dataBase.Resources.Find(resourceId).NumberOfReplys++;
            dataBase.SaveChanges();
        }

        public bool DeleteComment(int commentId)
        {
            if(helpQuery.IsComment(commentId) is false)return false;

            var subComments = GetSubComments(commentId);

            if (subComments.Count() > 0)
                foreach (var subComment in subComments)
                    DeleteComment(subComment.CommentId);

            var commentToDelete = dataBase.Comments.Find(commentId);
            var userComment = dataBase.UserComments.Find(commentToDelete.CommentOwnerId, commentToDelete.CommentId);

            if (commentToDelete is null || userComment is null) return false;

            dataBase.Resources.Find(helpQuery.GetResourceId(commentId)).NumberOfReplys--;

            dataBase.UserComments.Remove(userComment);
            dataBase.Comments.Remove(commentToDelete);
            dataBase.SaveChanges();

            return true;
        }

        public void AddUserComment(int commentId)
        {
            if (dataBase.UserComments.Any(uc => uc.UserId == DatabaseStateTracker.CurrentUser.UserId && uc.CommentId == commentId))
                return;

            UserComment userComment = new UserComment(DatabaseStateTracker.CurrentUser.UserId, commentId);
            dataBase.Add(userComment);
            dataBase.SaveChanges();
        }

        public bool EditComment(int commentId, string newContent)
        {
            if (helpQuery.IsComment(commentId) is false) return false;

            var comment = dataBase.Comments.Find(commentId);
            if (comment is null || dataBase.UserComments.Find(comment.CommentOwnerId, commentId) is null) 
                return false;

            comment.CommentContent = newContent;
            dataBase.SaveChanges();

            return true;
        }

        public List<Comment> GetResourceComments(int resourceId, int? parentCommentId)
        {
            return dataBase.Comments.Where(c => c.ResourceId == resourceId && c.ParentCommentId == parentCommentId).ToList();
        }

        public List<Comment> GetSubComments(int? parentCommentId)
        {
            return dataBase.Comments.Where(c => c.ParentCommentId == parentCommentId).ToList();
        }
    }
}
