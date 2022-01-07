using DomainLayer.Queries;
using DataLayer.Enums;
using DomainLayer.Entities;
using DataLayer.Entities.Models;
using PresentationLayer.Entities.Interacters;
using PresentationLayer.Entities.Utility;

namespace PresentationLayer.Entities.Handlers
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
                    Printer.PrintComment(comment.Item1, comment.Item2, indent);
                    
                    PrintComments(resourceId, comment.Item1.CommentId, indent + "\t");

                    commentQuery.AddUserComment(comment.Item1.CommentId);
                }
            }
        }

        private void PrintResources(List<(Resource, string)>? resourceList)
        {
            if (resourceList is null || resourceList.Count == 0)
            {
                Console.WriteLine("Lista resursa prazna!\n");
                return;
            }

            foreach (var resource in resourceList)
            {
                Printer.PrintResource(resource.Item1, resource.Item2);

                resourceQuery.AddUserResource(resource.Item1.ResourceId);

                
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
            Printer.PrintTitle("KORISNICI");
            var usersList = userQuery.ReadAllUsers();

            foreach (var user in usersList)
                Printer.PrintUsers(user);

            if(DatabaseStateTracker.CurrentUser.Role == Enum.GetName(UserRole.Organizator))
            {
                if (UserInteract.StartInteract())
                    return true;
            }  
            else
                Printer.ConfirmMessageAndClear("\nKorisnici ispisani", MessageType.Success);

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
            Printer.PrintTitle("VAŠI PODACI");

            var currentUserInfo = DatabaseStateTracker.CurrentUser;
            Printer.PrintUserPersonal(currentUserInfo);

            Printer.PrintUserSettingsMenu();
            if(UserInteract.StartSettings() is false)
                return false;

            return true;
        }

        public bool GetPopularEntities()
        {
            Printer.PrintTitle("POPULARNE OBJAVE");

            var resourceInteract = new EntityInteract();
            var resourceList = resourceQuery.GetPopularResources();

            PrintResources(resourceList);

            if (resourceInteract.StartInteraction())
                return false;

            return true;
        }
    }
}
