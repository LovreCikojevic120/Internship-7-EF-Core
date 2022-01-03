using DomainLayer.Queries;
using DomainLayer.DatabaseEnums;
using DomainLayer.Entities;

namespace PresentationLayer.Entities
{
    public static class DashboardHandler
    {
        public static void PrintEntitiesByTag()
        {
            Printer.PrintEntityTagList();

            var resourceCategory = ResourceTagSelect();

            var resourceQuery = new ResourceQueries();
            var commentQurey = new CommentQueries();

            var resourceList = resourceQuery.GetUserResources(resourceCategory);

            if (resourceList is null || resourceList.Count is 0)
            {
                Printer.ConfirmMessage("Lista resursa prazna");
                return;
            }

            foreach (var resource in resourceList)
            {
                Console.WriteLine($"{resource.ResourceContent} {resource.NameTag} {resource.ResourceId} {resource.NumberOfDislikes}");

                resourceQuery.AddUserResource(resource.ResourceId);
                var resourceCommentList = commentQurey.GetResourceComments(resource.ResourceId);
                if (resourceCommentList.Count is not 0)
                {

                    foreach (var comment in resourceCommentList)
                    {
                        Console.WriteLine($"\t{comment.CommentId} {comment.CommentContent}");

                        PrintSubcomments(comment.CommentId);

                        commentQurey.AddUserComment(comment.CommentId);
                    }
                }
            }

            ResourceInteract.Start();
        }

        private static void PrintSubcomments(int commentId)
        {
            var commentQurey = new CommentQueries();
            var subCommentList = commentQurey.GetSubComments(commentId);

            if (subCommentList.Count is 0) return;

            foreach (var subComment in subCommentList)
            {
                Console.WriteLine($"\t\t{subComment.CommentId} {subComment.CommentContent}");
                PrintSubcomments(subComment.CommentId);

                commentQurey.AddUserComment(subComment.CommentId);
            }
            return;
            
        }

        public static void GetNoReplyEntities()
        {
            Printer.PrintEntityTagList();

            var resourceCategory = ResourceTagSelect();
            var resourceQuery = new ResourceQueries();

            var resourceList = resourceQuery.GetNoReplyResources(resourceCategory);

            if (resourceList is null || resourceList.Count is 0)
            {
                Printer.ConfirmMessage("Lista resursa prazna");
                return;
            }

            foreach (var resource in resourceList)
                Console.WriteLine($"{resource.ResourceContent} {resource.NameTag}");

            ResourceInteract.Start();
        }

        public static void GetAllUsers()
        {
            var userQuery = new UserQueries();
            var usersList = userQuery.ReadAllUsers();

            foreach (var user in usersList)
                Console.WriteLine($"{user.UserName} {user.Role} {user.RepPoints}");

            Printer.ConfirmMessage($"Korisnici dohvaceni");
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
                        return ResourceTag.None;
                }

            }while(validInput is false);
        }

        public static void GetUserInfo()
        {
            var currentUserInfo = DatabaseStateTracker.CurrentUser;
            Console.WriteLine($"{currentUserInfo.UserName} {currentUserInfo.Password} {currentUserInfo.Role}");

            Printer.ConfirmMessage($"Vasi podaci dohvaceni");
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

            ResourceInteract.Start();
        }
    }
}
