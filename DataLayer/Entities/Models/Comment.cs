namespace DataLayer.Entities.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
        public int CommentOwnerId { get; set; }
        public User CommentOwner { get; set; }
        public int? ParentCommentId { get; set; }
        public DateTime TimeOfPosting { get; set; }
        public int ResourceId { get; set; }
        public Resource Resource { get; set; }
    }
}
