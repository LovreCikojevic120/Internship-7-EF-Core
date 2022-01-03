namespace DataLayer.Entities.Models
{
    public class UserComment
    {
        public int UserId { get; set; }
        public int CommentId { get; set; }

        public bool IsVoted { get; set; }
        public bool IsCommented { get; set; }

        public User User { get; set; }
        public Comment Comment { get; set; }

        public UserComment(int userId, int commentId)
        {
            UserId = userId;
            CommentId = commentId;
            IsVoted = false;
            IsCommented = false;
        }

        public UserComment() { }
    }
}
