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

            if (resourceCategory == ResourceTag.None) return;
            Printer.PrintResourceTagPosts(resourceCategory);

            var resourceQuery = new ResourceQueries();
            

            var resourceList = resourceQuery.GetUserResources(resourceCategory);

            if (resourceList is null || resourceList.Count is 0)
            {
                Printer.ConfirmMessage("Lista resursa prazna");
                return;
            }

            foreach (var resource in resourceList)
            {
                Console.WriteLine($"ID:{resource.ResourceId} Tag: {resource.NameTag}\nSadrzaj:{resource.ResourceContent}\n" +
                    $" Likes: {resource.NumberOfLikes} Dislikes: {resource.NumberOfDislikes}\n");

                resourceQuery.AddUserResource(resource.ResourceId);

                Console.WriteLine("Comments:\n");
                PrintComments(resource.ResourceId, null, "\t");
            }

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

                    indent += "\t";
                    PrintComments(resourceId, comment.CommentId, indent);

                    commentQurey.AddUserComment(comment.CommentId);
                }
            }
        }

        public static void GetNoReplyEntities()
        {
            Printer.PrintEntityTagList();

            var resourceCategory = ResourceTagSelect();
            if (resourceCategory == ResourceTag.None) return;
            Printer.PrintResourceTagPosts(resourceCategory);

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
        }

        private static ResourceTag ResourceTagSelect()
        {
            var resourceTag = Checkers.CheckForNumber(Console.ReadLine().Trim(), out int menuOption);

            do
            {
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
                    case (int)ResourceTag.None:
                        return ResourceTag.None;
                    default:
                        return ResourceTag.None;
                }

            }
            while(resourceTag is false);
        }

        public static void GetUserInfo()
        {
            var currentUserInfo = DatabaseStateTracker.CurrentUser;
            Console.WriteLine($"{currentUserInfo.UserName} {currentUserInfo.Password} {currentUserInfo.Role} {currentUserInfo.RepPoints}");

            Printer.ConfirmMessage($"Vasi podaci dohvaceni");
        }

        public static void GetPopularEntities()
        {
            var resourceQuery = new ResourceQueries();

            var resourceList = resourceQuery.GetPopularResources();

            Printer.ConfirmMessage("popular");

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
