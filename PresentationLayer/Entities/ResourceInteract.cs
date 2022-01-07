using DomainLayer.Entities;
using DomainLayer.Queries;
using PresentationLayer.Enums;

namespace PresentationLayer.Entities
{
    public class ResourceInteract
    {
        private CommentQueries commentQuery = new();
        private ResourceQueries resourceQuery = new();
        private HelperQueries helpQuery = new();

        public bool StartInteraction()
        {
            Printer.PrintResourceInteractMenu(DatabaseStateTracker.CurrentUser.Role);

            var validInput = Checkers.CheckForNumber(Console.ReadLine().Trim(), out int result);

            if (!validInput) return false;

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
                    Printer.ConfirmMessageAndClear("Vracate se na dashboard");
                    return true;
                default:
                    Printer.ConfirmMessageAndClear("Unos opcije izbornika neispravan");
                    return false;
            }
            return false;
        }

        private void DeleteEntity()
        {
            Console.WriteLine("Upisite ID resursa:");
            var validId = Checkers.CheckForNumber(Console.ReadLine(), out int entityId);

            if (validId && helpQuery.IsResource(entityId))
            {
                resourceQuery.DeleteResource(entityId);
                Printer.ConfirmMessageAndClear($"Resurs ID-a {entityId} izbrisan");
                return;
            }

            if (validId && helpQuery.IsComment(entityId))
            {
                commentQuery.DeleteComment(entityId);
                Printer.ConfirmMessageAndClear($"Komentar ID-a {entityId} izbrisan");
                return;
            }

            Printer.ConfirmMessageAndClear("Resurs ne postoji");
        }

        private void EditEntity()
        {
            Console.WriteLine("Upisite ID resursa:");
            var validId = Checkers.CheckForNumber(Console.ReadLine(), out int entityId);
            Console.WriteLine("Upisite novi sadrzaj resursa:");
            var validString = Checkers.CheckString(Console.ReadLine(), out string newContent);

            if (validId && helpQuery.IsResource(entityId) && validString)
            {
                resourceQuery.EditResource(entityId, newContent);
                Printer.ConfirmMessageAndClear($"Resurs ID-a {entityId} ureden");
                return;
            }

            if (validId && helpQuery.IsComment(entityId) && validString)
            {
                commentQuery.EditComment(entityId, newContent);
                Printer.ConfirmMessageAndClear($"Komentar ID-a {entityId} ureden");
                return;
            }

            Printer.ConfirmMessageAndClear("Greska");
        }

        private void DislikeEntity()
        {
            Console.WriteLine("Upisite ID:");
            var validId = Checkers.CheckForNumber(Console.ReadLine(), out int entityId);

            if (validId && helpQuery.IsResource(entityId))
            {
                resourceQuery.DislikeResource(entityId);
                Printer.ConfirmMessageAndClear($"Resurs ID-a {entityId} oznacen sa 'ne svida mi se'");
                return;
            }

            if (validId && helpQuery.IsComment(entityId)) 
            { 
                commentQuery.DislikeComment(entityId);
                Printer.ConfirmMessageAndClear($"Komentar ID-a {entityId} oznacen sa 'ne svida mi se'");
                return;
            }

            Printer.ConfirmMessageAndClear("Greska");
        }

        private void LikeEntity()
        {
            Console.WriteLine("Upisite ID:");
            var validId = Checkers.CheckForNumber(Console.ReadLine(), out int entityId);

            if (validId && helpQuery.IsResource(entityId))
            {
                resourceQuery.LikeResource(entityId);
                Printer.ConfirmMessageAndClear($"Resurs ID-a {entityId} oznacen sa 'svida mi se'");
                return;
            }

            if (validId && helpQuery.IsComment(entityId))
            {
                commentQuery.LikeComment(entityId);
                Printer.ConfirmMessageAndClear($"Komentar ID-a {entityId} oznacen sa 'svida mi se'");
                return;
            }

            Printer.ConfirmMessageAndClear("Resurs ne postoji!");
        }

        private void Comment()
        {
            Console.WriteLine("Upisite ID resursa:");
            var validId = Checkers.CheckForNumber(Console.ReadLine(), out int entityId);

            Console.WriteLine("Upisite sadrzaj odgovora[MIN 5 znakova]:");
            var validInput = Checkers.CheckString(Console.ReadLine(), out string content);

            if (validId && validInput && helpQuery.IsResource(entityId))
            {
                resourceQuery.CommentResource(entityId, content);
                Printer.ConfirmMessageAndClear($"Komentar dodan na resurs {entityId}");
                return;
            }
                
            Printer.ConfirmMessageAndClear("Greska");

        }

        private void Reply()
        {

            Console.WriteLine("Upisite ID komentara:");
            var validId = Checkers.CheckForNumber(Console.ReadLine().Trim(), out int entityId);

            Console.WriteLine("Upisite sadrzaj odgovora [MIN 5 znakova]:");
            var validInput = Checkers.CheckString(Console.ReadLine().Trim(), out string content);

            if (validId && validInput && helpQuery.IsComment(entityId))
            {
                commentQuery.ReplyOnComment(entityId, content);
                Printer.ConfirmMessageAndClear($"Odgovor dodan na komentar {entityId}");
                return;
            }

            Printer.ConfirmMessageAndClear("Komentar ne postoji");
        }

        private void CreateResource()
        {
            Console.WriteLine("Upisite sadrzaj novog resursa:");
            var validString = Checkers.CheckString(Console.ReadLine(), out string content);

            if (validString)
            {
                resourceQuery.CreateResource(content);
                Printer.ConfirmMessageAndClear("Resurs uspjesno dodan");
                return;
            }

            Printer.ConfirmMessageAndClear("Greska");
        }
    }
}
