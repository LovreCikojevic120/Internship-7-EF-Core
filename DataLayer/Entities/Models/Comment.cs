using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Models
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
        public int CommentOwnerId { get; set; }
        public User CommentOwner { get; set; }
        public int? ParentCommentId { get; set; }
        public DateTime TimeOfPosting { get; set; }
        public int? ResourceId { get; set; }
        public Resource Resource { get; set; }

        public Comment(int commentId, string content, int likes, int dislikes, int owner, int? resource, int? parentCommentId)
        {
            CommentId = commentId;
            CommentContent = content;
            NumberOfLikes = likes;
            NumberOfDislikes = dislikes;
            CommentOwnerId = owner;
            ResourceId = resource;
            TimeOfPosting = DateTime.Now;
            ParentCommentId = parentCommentId;
        }

        public Comment() { }
    }
}
