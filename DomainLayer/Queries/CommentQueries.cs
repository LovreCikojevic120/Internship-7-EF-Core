using DataLayer.Entities;
using DataLayer.Entities.Models;
using DomainLayer.DatabaseEnums;
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

        public void LikeComment(int commentId)
        {
            var reputationQuery = new ReputationQueries();
            var helpQuery = new HelperQueries();
            
            if (helpQuery.IsVoted(commentId) || 
                DatabaseStateTracker.CurrentUser.RepPoints < (int)ReputationPoints.CanUpvote)
            {
                Console.WriteLine("vec ste lajkali");
                return;
            }

            var comment = dataBase.Comments.Single(r => r.CommentId == commentId);
            comment.NumberOfLikes++;
            dataBase.SaveChanges();

            reputationQuery.GiveUpvote(comment.CommentOwnerId);
            reputationQuery.GetUpvoteComment();

            
        }

        public void DislikeComment(int commentId)
        {
            var helpQuery = new HelperQueries();
            var reputationQuery = new ReputationQueries();

            if (helpQuery.IsVoted(commentId) ||
                DatabaseStateTracker.CurrentUser.RepPoints < (int)ReputationPoints.CanDownvoteComment)
            {
                Console.WriteLine("vec ste lajkali");
                return;
            }

            var comment = dataBase.Comments.Find(commentId);
            comment.NumberOfDislikes++;
            dataBase.SaveChanges();

            reputationQuery.GiveDownvoteComment(comment.CommentOwnerId);
            reputationQuery.GetDownvotePoints();
        }

        public bool ReplyOnComment(int commentId)
        {
            var helpQuery = new HelperQueries();
            var reputationQuery = new ReputationQueries();

            if (helpQuery.IsCommented(commentId) || DatabaseStateTracker.CurrentUser.RepPoints < (int)ReputationPoints.CanReply)
            {
                Console.WriteLine("Vec ste komentirali ili nemate dovoljno bodova");
                return false;
            }

            var resourceId = helpQuery.GetResourceId(commentId);

            if(resourceId == null)
                return false;

            AddComment((int)resourceId, commentId);

            var userComment = dataBase.UserComments
                .Single(uc => uc.CommentId == commentId && uc.UserId==DatabaseStateTracker.CurrentUser.UserId);
            userComment.IsCommented = true;

            dataBase.SaveChanges();
            return true;
        }

        public void AddComment(int resourceId, int? parentCommentId)
        {
            Console.WriteLine("Upisi komentar");
            var commentContent = Console.ReadLine().Trim();
            Comment comment = new Comment(DatabaseStateTracker.GenerateEntityId(), commentContent, 0, 0, DatabaseStateTracker.CurrentUser.UserId, resourceId, parentCommentId);
            dataBase.Add(comment);
            dataBase.SaveChanges();
        }

        public void DeleteComment(int commentId)
        {
            var commentToDelete = dataBase.Comments.Find(commentId);
            var userComm = dataBase.UserComments.Find(commentToDelete.CommentOwnerId, commentToDelete.CommentId);

            if (commentToDelete is null || userComm is null) return;

            var subComments = dataBase.Comments.Where(c => c.ParentCommentId == commentToDelete.CommentId).ToList();

            if (subComments.Count() > 0)
                foreach (var subComment in subComments)
                    DeleteComment(subComment.CommentId);

            dataBase.UserComments.Remove(userComm);
            dataBase.Comments.Remove(commentToDelete);
            dataBase.SaveChanges();
        }

        public void AddUserComment(int commentId)
        {
            if (dataBase.UserComments.Any(uc => uc.UserId == DatabaseStateTracker.CurrentUser.UserId && uc.CommentId == commentId))
                return;

            UserComment userComment = new UserComment(DatabaseStateTracker.CurrentUser.UserId, commentId);
            dataBase.Add(userComment);
            dataBase.SaveChanges();
        }

        public void EditComment(int commentId, string newContent)
        {
            var comment = dataBase.Comments.Find(commentId);
            if (comment is null || dataBase.UserComments.Find(comment.CommentOwnerId, commentId) is null) return;

            comment.CommentContent = newContent;
            dataBase.SaveChanges();
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
