using DomainLayer.Queries;
using DomainLayer.DatabaseEnums;
using DomainLayer.Entities;
using DataLayer.Entities.Models;

namespace PresentationLayer.Entities
{
    public static class DashboardHandler
    {
        public static bool PrintEntitiesByTag(ResourceTag resourceCategory)
        {
            var resourceInteract = new ResourceInteract();

            Printer.PrintEntityTagList();

            if (resourceCategory == ResourceTag.None) return false;

            Printer.PrintResourceTagTitle(resourceCategory);

            var resourceQuery = new ResourceQueries();
            var resourceList = resourceQuery.GetUserResources(resourceCategory);

            PrintResources(resourceList, resourceQuery);

            if (resourceInteract.StartInteraction())
                return false;

            return true;
        }

        private static void PrintComments(int resourceId, int? parentCommentId, string indent)
        {
            var commentQurey = new CommentQueries();
            var resourceCommentList = commentQurey.GetResourceComments(resourceId, parentCommentId);

            if (resourceCommentList.Count is not 0)
            {
                foreach (var comment in resourceCommentList)
                {
                    Console.WriteLine(indent + $"ID: {comment.CommentId}\n{indent}Sadrzaj: {comment.CommentContent}\n" +
                        indent + $"Likes: {comment.NumberOfLikes} Dislikes: {comment.NumberOfDislikes}\n");

                    PrintComments(resourceId, comment.CommentId, indent + "\t");

                    commentQurey.AddUserComment(comment.CommentId);
                }
            }
        }

        private static void PrintResources(List<Resource>? resourceList, ResourceQueries resourceQuery)
        {
            if(resourceList is null || resourceList.Count == 0) return;

            foreach (var resource in resourceList)
            {
                Console.WriteLine($"ID:{resource.ResourceId} Tag: {resource.NameTag}\nSadrzaj:{resource.ResourceContent}\n" +
                    $" Likes: {resource.NumberOfLikes} Dislikes: {resource.NumberOfDislikes}\n");

                resourceQuery.AddUserResource(resource.ResourceId);

                Console.WriteLine("Comments:\n");
                PrintComments(resource.ResourceId, null, "\t");
            }
        }

        public static bool GetNoReplyEntities(ResourceTag resourceCategory)
        {
            var resourceInteract = new ResourceInteract();

            Printer.PrintEntityTagList();

            if (resourceCategory == ResourceTag.None) return false;

            Printer.PrintResourceTagTitle(resourceCategory);

            var resourceQuery = new ResourceQueries();

            var resourceList = resourceQuery.GetNoReplyResources(resourceCategory);

            if (resourceList is null || resourceList.Count is 0)
            {
                Printer.ConfirmMessageAndClear("Lista resursa prazna");
                return false;
            }

            PrintResources(resourceList, resourceQuery);

            if(resourceInteract.StartInteraction())
                return false;

            return true;
        }

        public static bool GetAllUsers()
        {
            var userQuery = new UserQueries();
            var usersList = userQuery.ReadAllUsers();

            foreach (var user in usersList)
                Console.WriteLine($"{user.UserName} {user.Role} {user.RepPoints}");

            if(DatabaseStateTracker.CurrentUser.Role == "Admin")
                UserInteract.StartInteract();

            Printer.ConfirmMessageAndClear("Gotovo");
            return false;
        }

        public static bool ResourceTagSelect()
        {
            Printer.PrintEntityTagList();

            Checkers.CheckForNumber(Console.ReadLine().Trim(), out int menuOption);

            if (Enum.IsDefined(typeof(ResourceTag), menuOption))
            {
                DatabaseStateTracker.currentResourceTag = (ResourceTag)menuOption;
                return false;
            }

            Console.WriteLine("Krivi unos");
            DatabaseStateTracker.currentResourceTag = ResourceTag.None;
            return true;
        }

        public static bool GetUserInfo()
        {
            var currentUserInfo = DatabaseStateTracker.CurrentUser;
            Console.WriteLine($"{currentUserInfo.UserName} {currentUserInfo.Password} {currentUserInfo.Role} {currentUserInfo.RepPoints}");

            Printer.PrintUserSettingsMenu();
            UserInteract.StartSettings();

            return false;
        }

        public static bool GetPopularEntities()
        {
            var resourceInteract = new ResourceInteract();
            var resourceQuery = new ResourceQueries();
            var resourceList = resourceQuery.GetPopularResources();

            if(resourceList is null)
            {
                Printer.ConfirmMessageAndClear("Lista resursa prazna");
                return false;
            }

            foreach(var resource in resourceList)
                Console.WriteLine($"{resource.ResourceContent} {resource.ResourceOwner}");

            if (resourceInteract.StartInteraction())
                return false;

            return true;
        }

        
    }
}
