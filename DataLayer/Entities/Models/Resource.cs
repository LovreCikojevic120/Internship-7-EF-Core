using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Models
{
    public class Resource
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResourceId { get; set; }
        public string ResourceContent { get; set; }
        public int ResourceOwnerId { get; set; }
        public User ResourceOwner { get; set; }
        public int NumberOfReplys { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
        public DateTime TimeOfPosting { get; set; }
        public string NameTag { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public Resource(int resourceId, string content, string nameTag, int ownerId)
        {
            ResourceId = resourceId;
            ResourceContent = content;
            NumberOfLikes = 0;
            NumberOfDislikes = 0;
            NumberOfReplys = 0;
            NameTag = nameTag;
            TimeOfPosting = DateTime.Now;
            ResourceOwnerId = ownerId;
        }
        public Resource() { }
    }
}
