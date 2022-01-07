using DataLayer.Entities;
using DataLayer.Entities.Models;
using DataLayer.Enums;
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

        private void CheckPointsSubtraction(User user, int points)
        {
            if (user.RepPoints - points < 1)
                user.RepPoints = 1;

            user.RepPoints -= points;
            dataBase.SaveChanges();
        }

        private void CheckPointsAddition(User user, int points)
        {
            if (user.RepPoints + points >= (int)ReputationPoints.IsTrusted)
                user.IsTrusted = true;

            if (user.RepPoints + points >= (int)ReputationPoints.IsOrganizer)
                user.Role = "Admin";

            user.RepPoints += points;
            dataBase.SaveChanges();
        }

        public void GetUpvoteResource()
        {
            var user = dataBase.Users.Single(u => u.UserId == DatabaseStateTracker.CurrentUser.UserId);
            CheckPointsAddition(user, 10);
        }

        public void GetUpvoteComment()
        {
            var user = dataBase.Users.Single(u => u.UserId == DatabaseStateTracker.CurrentUser.UserId);
            CheckPointsAddition(user, 5);
        }

        public void GiveUpvote(int userId)
        {
            var user = dataBase.Users.Single(u => u.UserId == userId);
            CheckPointsAddition(user, 15);
        }

        public void GetDownvotePoints()
        {
            var user = dataBase.Users.Single(u => u.UserId == DatabaseStateTracker.CurrentUser.UserId);
            CheckPointsSubtraction(user, 5);
        }

        public void GiveDownvoteResource(int userId)
        {
            var user = dataBase.Users.Single(u => u.UserId == userId);
            CheckPointsSubtraction(user, 3);
        }

        public void GiveDownvoteComment(int userId)
        {
            var user = dataBase.Users.Single(u => u.UserId == userId);
            CheckPointsSubtraction(user, 2);
        }

    }
}
