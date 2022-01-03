using DataLayer.Entities;
using DomainLayer.Entities;

namespace DomainLayer.Queries
{
    public class ReputationQueries
    {
        private readonly StackInternshipDbContext dataBase;
        public ReputationQueries()
        {
            dataBase = DbContextFactory.GetStackInternshipDbContext();
        }

        private int CheckRepPoints(int points)
        {
            if (DatabaseStateTracker.CurrentUser.RepPoints - points < 1)
            {
                DatabaseStateTracker.CurrentUser.RepPoints = 1;
                return 1;
            }
            return DatabaseStateTracker.CurrentUser.RepPoints -= points;
        }

        public void GetUpvoteResource()
        {
            var user = dataBase.Users.Single(u => u.UserId == DatabaseStateTracker.CurrentUser.UserId);
            user.RepPoints += 10;
            dataBase.SaveChanges();
        }

        public void GetUpvoteComment()
        {
            var user = dataBase.Users.Single(u => u.UserId == DatabaseStateTracker.CurrentUser.UserId);
            user.RepPoints += 5;
            dataBase.SaveChanges();
        }

        public void GiveUpvote(int userId)
        {
            var user = dataBase.Users.Single(u => u.UserId == userId);
            user.RepPoints += 15;
            dataBase.SaveChanges();
        }

        public void GetDownvotePoints()
        {
            var user = dataBase.Users.Single(u => u.UserId == DatabaseStateTracker.CurrentUser.UserId);
            user.RepPoints = CheckRepPoints(5);
            dataBase.SaveChanges();
        }

        public void GiveDownvoteResource(int userId)
        {
            var user = dataBase.Users.Single(u => u.UserId == userId);
            user.RepPoints = CheckRepPoints(3);
            dataBase.SaveChanges();
        }

        public void GiveDownvoteComment(int userId)
        {
            var user = dataBase.Users.Single(u => u.UserId == userId);
            user.RepPoints = CheckRepPoints(2);
            dataBase.SaveChanges();
        }

    }
}
