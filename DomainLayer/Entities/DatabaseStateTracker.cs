using DataLayer.Entities.Models;
using DomainLayer.DatabaseEnums;

namespace DomainLayer.Entities
{
    public static class DatabaseStateTracker
    {
        public static User? CurrentUser;

        public static ResourceTag currentResourceTag = ResourceTag.None;

        public static int GenerateEntityId()
        {
            Random random = new Random();
            return random.Next(DateTime.Now.Millisecond, 1000000) + CurrentUser.UserId;
        }
    }
}
