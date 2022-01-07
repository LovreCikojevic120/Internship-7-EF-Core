using DomainLayer.Queries;
using DomainLayer.DatabaseEnums;
using DomainLayer.Entities;
using DataLayer.Entities.Models;

namespace PresentationLayer.Entities
{
    public class DashboardHandler
    {
        private ResourceQueries resourceQuery = new();
        private CommentQueries commentQuery = new();
        private UserQueries userQuery = new();

        public bool PrintEntitiesByTag(ResourceTag resourceCategory)
        {
            var resourceInteract = new ResourceInteract();

            Printer.PrintEntityTagList();

            if (resourceCategory == ResourceTag.None) return false;

            Printer.PrintResourceTagTitle(resourceCategory);

            var resourceList = resourceQuery.GetUserResources(resourceCategory);

            PrintResources(resourceList);

            if (resourceInteract.StartInteraction())
                return false;

            return true;
        }

        private void PrintComments(int resourceId, int? parentCommentId, string indent)
        {
            var resourceCommentList = commentQuery.GetResourceComments(resourceId, parentCommentId);

            if (resourceCommentList.Count is not 0)
            {
                foreach (var comment in resourceCommentList)
                {
                    Console.WriteLine(indent + $"ID: {comment.CommentId}\n{indent}Sadrzaj: {comment.CommentContent}\n" +
                        indent + $"Likes: {comment.NumberOfLikes} Dislikes: {comment.NumberOfDislikes}\n");

                    PrintComments(resourceId, comment.CommentId, indent + "\t");

                    commentQuery.AddUserComment(comment.CommentId);
                }
            }
        }

        private void PrintResources(List<(Resource, string)>? resourceList)
        {
            if(resourceList is null || resourceList.Count == 0) return;

            foreach (var resource in resourceList)
            {
                Printer.PrintResource(resource.Item1, resource.Item2);

                resourceQuery.AddUserResource(resource.Item1.ResourceId);

                Console.WriteLine("Comments:\n");
                PrintComments(resource.Item1.ResourceId, null, "\t");
            }
        }

        public bool GetNoReplyEntities(ResourceTag resourceCategory)
        {
            var resourceInteract = new ResourceInteract();

            Printer.PrintEntityTagList();
            if (resourceCategory == ResourceTag.None) return false;

            Printer.PrintResourceTagTitle(resourceCategory);

            var resourceList = resourceQuery.GetNoReplyResources(resourceCategory);

            if (resourceList is null || resourceList.Count is 0)
            {
                Printer.ConfirmMessageAndClear("Lista resursa prazna");
                return false;
            }

            PrintResources(resourceList);

            if(resourceInteract.StartInteraction())
                return false;

            return true;
        }

        public bool GetAllUsers()
        {
            var usersList = userQuery.ReadAllUsers();

            foreach (var user in usersList)
                Console.WriteLine($"{user.UserName} {user.Role} {user.RepPoints}");

            if(DatabaseStateTracker.CurrentUser.Role == "Admin")
                UserInteract.StartInteract();

            Printer.ConfirmMessageAndClear("Gotovo");
            return false;
        }

        public bool ResourceTagSelect()
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

        public bool GetUserInfo()
        {
            var currentUserInfo = DatabaseStateTracker.CurrentUser;
            Console.WriteLine($"{currentUserInfo.UserName} {currentUserInfo.Password} {currentUserInfo.Role} {currentUserInfo.RepPoints}");

            Printer.PrintUserSettingsMenu();
            UserInteract.StartSettings();

            return false;
        }

        public bool GetPopularEntities()
        {
            var resourceInteract = new ResourceInteract();
            var resourceList = resourceQuery.GetPopularResources();

            if(resourceList is null)
            {
                Printer.ConfirmMessageAndClear("Lista resursa prazna");
                return false;
            }

            PrintResources(resourceList);

            if (resourceInteract.StartInteraction())
                return false;

            return true;
        }
    }
}
