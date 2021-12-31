using DomainLayer.Queries;
using DomainLayer.DatabaseEnums;
using DomainLayer.Entities;

namespace PresentationLayer.Entities
{
    public static class DashboardHandler
    {
        public static void GetResourcesByTag()
        {
            //print resource tags
            var resourceCategory = ResourceTagSelect();

            var resourceQuery = new ResourceQueries();
            var resourceList = resourceQuery.GetUserRecources(resourceCategory);

            if (resourceList is null || resourceList.Count is 0)
            {
                Console.WriteLine("Nema resursa!");
                return;
            }

            foreach (var resource in resourceList)
                Console.WriteLine($"{resource.ResourceContent} {resource.NameTag}");
        }

        public static void GetNoReplyResouces()
        {
            var resourceCategory = ResourceTagSelect();
            var resourceQuery = new ResourceQueries();

            var resourceList = resourceQuery.GetNoReplyResources(resourceCategory);

            if (resourceList is null || resourceList.Count is 0)
            {
                Console.WriteLine("Nema resursa!");
                return;
            }

            foreach (var resource in resourceList)
                Console.WriteLine($"{resource.ResourceContent} {resource.NameTag}");
        }

        public static void GetAllUsers()
        {
            var userQuery = new UserQueries();
            var usersList = userQuery.ReadAllUsers();

            foreach (var user in usersList)
                Console.WriteLine($"{user.UserName} {user.Role} {user.RepPoints}");
        }

        private static ResourceTag ResourceTagSelect()
        {
            var validInput = false;

            do
            {
                validInput = Checkers.CheckForNumber(Console.ReadLine().Trim(), out int menuOption);

                switch (menuOption)
                {
                    case (int)ResourceTag.Dev:
                        return ResourceTag.Dev;
                    case (int)ResourceTag.Multimedija:
                        return ResourceTag.Multimedija;
                    case (int)ResourceTag.Dizajn:
                        return ResourceTag.Dizajn;
                    case (int)ResourceTag.Marketing:
                        return ResourceTag.Marketing;
                    case (int)ResourceTag.Generalno:
                        return ResourceTag.Generalno;
                    default:
                        //handleWrongOption
                        break;
                }

            }while(validInput is false);

            return ResourceTag.Dev;
        }

        public static void GetUserInfo()
        {
            var currentUserInfo = DatabaseStateTracker.CurrentUser;
            Console.WriteLine($"{currentUserInfo.UserName} {currentUserInfo.Password} {currentUserInfo.Role}");
        }

        public static void GetPopularResources()
        {
            var resourceQuery = new ResourceQueries();

            var resourceList = resourceQuery.GetPopularResources();

            if(resourceList is null)
            {
                //empty list
                return;
            }

            foreach(var resource in resourceList)
            {
                Console.WriteLine($"{resource.ResourceContent} {resource.ResourceOwner}");
            }
        }
    }
}
