using DomainLayer.Queries;
using DomainLayer.DatabaseEnums;
using DomainLayer.Entities;
using DataLayer.Entities.Models;

namespace PresentationLayer.Entities
{
    public static class DashboardHandler
    {
        public static void PrintEntitiesByTag()
        {
            Printer.PrintEntityTagList();

            var resourceCategory = ResourceTagSelect();

            if (resourceCategory == ResourceTag.None) return;

            Printer.PrintResourceTagTitle(resourceCategory);

            var resourceQuery = new ResourceQueries();
            var resourceList = resourceQuery.GetUserResources(resourceCategory);

            PrintResources(resourceList, resourceQuery);

            ResourceInteract.StartInteraction();
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

        public static void GetNoReplyEntities()
        {
            Printer.PrintEntityTagList();

            var resourceCategory = ResourceTagSelect();
            if (resourceCategory == ResourceTag.None) return;
            Printer.PrintResourceTagTitle(resourceCategory);

            var resourceQuery = new ResourceQueries();

            var resourceList = resourceQuery.GetNoReplyResources(resourceCategory);

            if (resourceList is null || resourceList.Count is 0)
            {
                Printer.ConfirmMessage("Lista resursa prazna");
                return;
            }

            foreach (var resource in resourceList)
                Console.WriteLine($"{resource.ResourceContent} {resource.NameTag}");

            ResourceInteract.StartInteraction();
        }

        public static void GetAllUsers()
        {
            var userQuery = new UserQueries();
            var usersList = userQuery.ReadAllUsers();

            foreach (var user in usersList)
                Console.WriteLine($"{user.UserName} {user.Role} {user.RepPoints}");

            Printer.ConfirmMessage($"Korisnici dohvaceni");

            if(DatabaseStateTracker.CurrentUser.Role == "Admin")
                UserInteract.StartInteract();
        }

        private static ResourceTag ResourceTagSelect()
        {
            bool validInput;

            do
            {
                Checkers.CheckForNumber(Console.ReadLine().Trim(), out int menuOption);

                if (Enum.IsDefined(typeof(ResourceTag), menuOption))
                    return (ResourceTag)menuOption;

                Console.WriteLine("Krivi unos");
                validInput = false;
            }
            while(validInput is false);

            return ResourceTag.None;
        }

        public static void GetUserInfo()
        {
            var currentUserInfo = DatabaseStateTracker.CurrentUser;
            Console.WriteLine($"{currentUserInfo.UserName} {currentUserInfo.Password} {currentUserInfo.Role} {currentUserInfo.RepPoints}");

            Printer.PrintUserSettingsMenu();
            UserInteract.StartSettings();
        }

        public static void GetPopularEntities()
        {
            var resourceQuery = new ResourceQueries();

            var resourceList = resourceQuery.GetPopularResources();

            if(resourceList is null)
            {
                Printer.ConfirmMessage("Lista resursa prazna");
                return;
            }

            foreach(var resource in resourceList)
                Console.WriteLine($"{resource.ResourceContent} {resource.ResourceOwner}");

            ResourceInteract.StartInteraction();
        }
    }
}
