using DataLayer.Entities.Models;
using DataLayer.Enums;

namespace DomainLayer.Entities
{
    public static class DatabaseStateTracker
    {
        public static User? CurrentUser;

        public static ResourceTag currentResourceTag = ResourceTag.Generalno;

        public static int GenerateEntityId()
        {
            Random random = new Random();
            return random.Next(DateTime.Now.Millisecond, 1000000) + CurrentUser.UserId;
        }
    }
}
