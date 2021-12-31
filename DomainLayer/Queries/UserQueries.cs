using DataLayer.Entities;
using DataLayer.Entities.Models;
using DomainLayer.Entities;

namespace DomainLayer.Queries
{
    public class UserQueries
    {

        private readonly StackInternshipDbContext dataBase;
        public UserQueries()
        {
            dataBase = DbContextFactory.GetStackInternshipDbContext();
        }

        public void Register(string userName, string password)
        {
            User user = new User(userName, password);
            dataBase.Add(user);
            DatabaseStateTracker.CurrentUser = user;
            dataBase.SaveChanges();
        }

        public bool Login(string userName, string password)
        {
            var result = dataBase.Users.Where(p => p.Password == password).FirstOrDefault(u => u.UserName == userName);
            if (result is null) return false;

            DatabaseStateTracker.CurrentUser = result;
            return true;
        }

        public ICollection<User> ReadAllUsers()
        {
            return dataBase.Users.ToList();
        }
    }
}
