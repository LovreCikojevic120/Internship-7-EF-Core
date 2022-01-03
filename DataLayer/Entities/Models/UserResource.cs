namespace DataLayer.Entities.Models
{
    public class UserResource
    {
        public int UserId { get; set; }
        public int ResourceId { get; set; }

        public User User { get; set; }
        public Resource Resource { get; set; }

        public bool IsVoted { get; set; }
        public bool IsCommented { get; set; }

        public UserResource(int userId, int resourceId)
        {
            UserId = userId;
            ResourceId = resourceId;
            IsVoted = false;
            IsCommented = false;
        }

        public UserResource() { }
    }
}
