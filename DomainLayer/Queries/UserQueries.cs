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
            var result = dataBase.Users
                .Where(p => p.Password == password && p.UserName == userName && p.IsDeactivated == false)
                .FirstOrDefault();
            if (result is null) return false;

            DatabaseStateTracker.CurrentUser = result;
            return true;
        }

        public bool UserExists(string username)
        {
            return dataBase.Users.Any(u => u.UserName == username);
        }

        public bool TemporaryDeactivate(int userId, int days)
        {
            var user = dataBase.Users.Find(userId);

            if (user is null)
                return false;

            user.IsDeactivated = true;
            user.DeactivatedUntil = DateTime.Now.AddDays(days);
            dataBase.SaveChanges();
            return true;
        }

        public bool Reactivate(int userId)
        {
            var user = dataBase.Users.Find(userId);

            if (user is null) return false;

            user.IsDeactivated = false;
            user.DeactivatedUntil = null;
            dataBase.SaveChanges();
            return true;
        }

        public bool PermaDeactivate(int userId)
        {
            var user = dataBase.Users.Find(userId);

            if (user is null) return false;

            user.IsDeactivated = true;
            user.DeactivatedUntil = null;
            dataBase.SaveChanges();
            return true;
        }

        public bool EditUserName(string result)
        {
            var user = dataBase.Users.Find(DatabaseStateTracker.CurrentUser.UserId);

            if (user is null) return false;

            user.UserName = result;
            DatabaseStateTracker.CurrentUser = user;
            dataBase.SaveChanges();
            return true;
        }

        public ICollection<User> ReadAllUsers()
        {
            return dataBase.Users.ToList();
        }

        public bool EditPassword(string result)
        {
            var user = dataBase.Users.Find(DatabaseStateTracker.CurrentUser.UserId);

            if (user is null) return false;

            user.Password = result;
            DatabaseStateTracker.CurrentUser = user;
            dataBase.SaveChanges();
            return true;
        }
    }
}
