using DomainLayer.Queries;
using DataLayer.Enums;
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
            var resourceInteract = new EntityInteract();

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
            if (resourceList is null || resourceList.Count == 0)
            {
                Console.WriteLine("Lista resursa prazna!");
                return;
            }

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
            var resourceInteract = new EntityInteract();

            Printer.PrintEntityTagList();
            if (resourceCategory == ResourceTag.None) return false;

            Printer.PrintResourceTagTitle(resourceCategory);

            var resourceList = resourceQuery.GetNoReplyResources(resourceCategory);

            PrintResources(resourceList);

            if(resourceInteract.StartInteraction())
                return false;

            return true;
        }

        public bool GetAllUsers()
        {
            var usersList = userQuery.ReadAllUsers();

            foreach (var user in usersList)
                Printer.PrintUsers(user);

            if(DatabaseStateTracker.CurrentUser.Role == "Admin")
            {
                if (UserInteract.StartInteract())
                    return true;
            }  

            Printer.ConfirmMessageAndClear("Korisnici ispisani", MessageType.Success);
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

            Printer.ConfirmMessageAndClear("Krivi unos izbornika", MessageType.Error);
            DatabaseStateTracker.currentResourceTag = ResourceTag.None;
            return true;
        }

        public bool GetUserInfo()
        {
            var currentUserInfo = DatabaseStateTracker.CurrentUser;
            Printer.PrintUserPersonal(currentUserInfo);

            Printer.PrintUserSettingsMenu();
            if(UserInteract.StartSettings())
                return false;

            return true;
        }

        public bool GetPopularEntities()
        {
            var resourceInteract = new EntityInteract();
            var resourceList = resourceQuery.GetPopularResources();

            PrintResources(resourceList);

            if (resourceInteract.StartInteraction())
                return false;

            return true;
        }
    }
}
