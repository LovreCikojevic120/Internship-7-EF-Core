using DomainLayer.DatabaseEnums;
using DomainLayer.Entities;
using DomainLayer.Queries;
using PresentationLayer.Enums;

namespace PresentationLayer.Entities
{
    public static class ResourceInteract
    {
        public static void StartInteraction()
        {
            Printer.PrintResourceInteractMenu(DatabaseStateTracker.CurrentUser.Role);

            var validInput = Checkers.CheckForNumber(Console.ReadLine().Trim(), out int result);

            do
            {
                switch (result)
                {
                    case (int)ResourceInteraction.New:
                        CreateResource();
                        break;
                    case (int)ResourceInteraction.Comment:
                        Comment();
                        break;
                    case (int)ResourceInteraction.Reply:
                        Reply();
                        break;
                    case (int)ResourceInteraction.Like:
                        LikeEntity();
                        break;
                    case (int)ResourceInteraction.Dislike:
                        DislikeEntity();
                        break;
                    case (int)ResourceInteraction.Edit:
                        EditEntity();
                        break;
                    case (int)ResourceInteraction.Delete:
                        DeleteEntity();
                        break;
                    case (int)ResourceInteraction.None:
                        Printer.ConfirmMessage("Vracate se na dashboard");
                        break;
                    default:
                        Printer.ConfirmMessage("Unos opcije izbornika neispravan");
                        validInput = false;
                        break;
                }
            } while (validInput is false);
        }

        private static void DeleteEntity()
        {
            var commentQuery = new CommentQueries();
            var resourceQuery = new ResourceQueries();
            var helpQuery = new HelperQueries();

            Console.WriteLine("Koi id?");
            var hmm = Checkers.CheckForNumber(Console.ReadLine(), out int hm);

            if (hmm && helpQuery.IsResource(hm))
                resourceQuery.DeleteResource(hm);

            if (hmm && helpQuery.IsComment(hm))
                commentQuery.DeleteComment(hm);

            else Console.WriteLine("yikes");
        }

        private static void EditEntity()
        {
            var commentQuery = new CommentQueries();
            var resourceQuery = new ResourceQueries();
            var helpQuery = new HelperQueries();

            Console.WriteLine("Koi id?");
            var hmm = Checkers.CheckForNumber(Console.ReadLine(), out int hm);
            Console.WriteLine("NOvi sardzaj");
            var validString = Checkers.CheckString(Console.ReadLine(), out string newContent);

            if (hmm && helpQuery.IsResource(hm))
                resourceQuery.EditResource(hm, newContent);

            if (hmm && helpQuery.IsComment(hm))
                commentQuery.EditComment(hm, newContent);

            else Console.WriteLine("yikes");
        }

        private static void DislikeEntity()
        {
            var commentQuery = new CommentQueries();
            var resourceQuery = new ResourceQueries();
            var helpQuery = new HelperQueries();

            Console.WriteLine("Koi id?");
            var hmm = Checkers.CheckForNumber(Console.ReadLine(), out int hm);

            if (hmm && helpQuery.IsResource(hm))
                resourceQuery.DislikeResource(hm);

            if (hmm && helpQuery.IsComment(hm))
                commentQuery.DislikeComment(hm);

            else Console.WriteLine("yikes");
        }

        private static void LikeEntity()
        {
            var commentQuery = new CommentQueries();
            var resourceQuery = new ResourceQueries();
            var helpQuery = new HelperQueries();

            Console.WriteLine("Koi id?");
            var hmm = Checkers.CheckForNumber(Console.ReadLine(), out int hm);

            if (hmm && helpQuery.IsResource(hm))
                resourceQuery.LikeResource(hm);

            if (hmm && helpQuery.IsComment(hm))
                commentQuery.LikeComment(hm);

            else Console.WriteLine("Resurs ne postoji!");
        }

        private static void Comment()
        {
            var resourceQuery = new ResourceQueries();
            var helpQuery = new HelperQueries();

            Console.WriteLine("Koi id?");
            var hmm = Checkers.CheckForNumber(Console.ReadLine(), out int hm);

            if (hmm && helpQuery.IsResource(hm))
                resourceQuery.CommentResource(hm);

            else Console.WriteLine("yikes");

        }

        private static void Reply()
        {
            var queri = new CommentQueries();
            var helpQuery = new HelperQueries();

            Console.WriteLine("Upisite ID komentara:");
            var hmm = Checkers.CheckForNumber(Console.ReadLine(), out int hm);

            if (hmm && helpQuery.IsComment(hm))
                queri.ReplyOnComment(hm);

            else Printer.ConfirmMessage("Komentar ne postoji");

            Printer.ConfirmMessage("Uspjesno ste odgovorili na komentar");
        }

        private static void CreateResource()
        {
            var resourceQuery = new ResourceQueries();
            Console.WriteLine("Sadrzaj:");
            var content = Console.ReadLine();
            resourceQuery.CreateResource(content);
        }
    }
}
