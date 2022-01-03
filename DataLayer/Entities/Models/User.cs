namespace DataLayer.Entities.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int RepPoints { get; set; }
        public bool IsTrusted { get; set; }

        public ICollection<Comment> Comments { get; set; }  
        public ICollection<Resource> Resources { get; set; }

        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
            Role = "Intern";
            RepPoints = 1;
            IsTrusted = false;
        }
        public User() { }
    }
}
