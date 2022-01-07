using DataLayer.Enums;
using DomainLayer.Entities;
using DomainLayer.Queries;
using PresentationLayer.Entities.Utility;
using PresentationLayer.Enums;

namespace PresentationLayer.Entities.Interacters
{
    public class EntityInteract
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
                    Printer.ConfirmMessageAndClear("Vracate se na dashboard", MessageType.Note);
                    return true;
                default:
                    Printer.ConfirmMessageAndClear("Unos opcije izbornika neispravan", MessageType.Error);
                    return false;
            }
            return false;
        }

        private void DeleteEntity()
        {
            Console.WriteLine("Upisite ID resursa:");
            var validId = Checkers.CheckForNumber(Console.ReadLine(), out int entityId);

            if (ErrorHandler.PrintError(validId, 
                DatabaseStateTracker.CurrentUser.RepPoints < (int)ReputationPoints.CanDelete, helpQuery.IsResource(entityId),
                resourceQuery.DeleteResource(entityId)))
                return;

            if (ErrorHandler.PrintError(validId, 
                DatabaseStateTracker.CurrentUser.RepPoints < (int)ReputationPoints.CanDelete, helpQuery.IsComment(entityId),
                commentQuery.DeleteComment(entityId)))
                return;

            Printer.ConfirmMessageAndClear("Greška", MessageType.Error);
        }

        private void EditEntity()
        {
            if (DatabaseStateTracker.CurrentUser.Role == "Intern") return;

            Console.WriteLine("Upisite ID resursa:");
            var validId = Checkers.CheckForNumber(Console.ReadLine(), out int entityId);
            Console.WriteLine("Upisite novi sadrzaj resursa:");
            var validString = Checkers.CheckString(Console.ReadLine(), out string newContent);

            if (ErrorHandler.PrintError(validId,
                DatabaseStateTracker.CurrentUser.RepPoints < (int)ReputationPoints.CanEditAnyEntity, helpQuery.IsResource(entityId),
                resourceQuery.EditResource(entityId, newContent)))
                return;

            if (ErrorHandler.PrintError(validId,
                DatabaseStateTracker.CurrentUser.RepPoints < (int)ReputationPoints.CanEditAnyEntity, helpQuery.IsComment(entityId),
                commentQuery.EditComment(entityId, newContent)))
                return;

            Printer.ConfirmMessageAndClear("Greška", MessageType.Error);
        }

        private void DislikeEntity()
        {
            Console.WriteLine("Upisite ID:");
            var validId = Checkers.CheckForNumber(Console.ReadLine(), out int entityId);

            if (ErrorHandler.PrintError(validId,
                DatabaseStateTracker.CurrentUser.RepPoints < (int)ReputationPoints.CanDownvoteResource, helpQuery.IsResource(entityId),
                resourceQuery.DislikeResource(entityId)))
                return;

            if (ErrorHandler.PrintError(validId,
                DatabaseStateTracker.CurrentUser.RepPoints < (int)ReputationPoints.CanDownvoteComment, helpQuery.IsComment(entityId),
                commentQuery.DislikeComment(entityId)))
                return;

            Printer.ConfirmMessageAndClear("Greška", MessageType.Error);
        }

        private void LikeEntity()
        {
            Console.WriteLine("Upisite ID:");
            var validId = Checkers.CheckForNumber(Console.ReadLine(), out int entityId);

            if (ErrorHandler.PrintError(validId,
                DatabaseStateTracker.CurrentUser.RepPoints < (int)ReputationPoints.CanUpvote, helpQuery.IsResource(entityId),
                resourceQuery.LikeResource(entityId)))
                return;

            if (ErrorHandler.PrintError(validId,
                DatabaseStateTracker.CurrentUser.RepPoints < (int)ReputationPoints.CanUpvote, helpQuery.IsComment(entityId),
                commentQuery.LikeComment(entityId)))
                return;

            Printer.ConfirmMessageAndClear("Greška", MessageType.Error);
        }

        private void Comment()
        {
            Console.WriteLine("Upisite ID resursa:");
            var validId = Checkers.CheckForNumber(Console.ReadLine(), out int entityId);

            Console.WriteLine("Upisite sadrzaj odgovora[MIN 5 znakova]:");
            var validInput = Checkers.CheckString(Console.ReadLine(), out string content);

            if (ErrorHandler.PrintError(validId,
                DatabaseStateTracker.CurrentUser.RepPoints < (int)ReputationPoints.CanComment, helpQuery.IsResource(entityId),
                resourceQuery.CommentResource(entityId, content)))
                return;
                
            Printer.ConfirmMessageAndClear("Greska", MessageType.Error);

        }

        private void Reply()
        {
            Console.WriteLine("Upisite ID komentara:");
            var validId = Checkers.CheckForNumber(Console.ReadLine().Trim(), out int entityId);

            Console.WriteLine("Upisite sadrzaj odgovora [MIN 5 znakova]:");
            var validInput = Checkers.CheckString(Console.ReadLine().Trim(), out string content);

            if (ErrorHandler.PrintError(validId,
                DatabaseStateTracker.CurrentUser.RepPoints < (int)ReputationPoints.CanReply, helpQuery.IsComment(entityId),
                commentQuery.ReplyOnComment(entityId, content)))
                return;

            Printer.ConfirmMessageAndClear("Greška", MessageType.Error);
        }

        private void CreateResource()
        {
            if (DatabaseStateTracker.CurrentUser.Role == "Intern")
            {
                Printer.ConfirmMessageAndClear("Nemate ovlaštenje za ovu akciju", MessageType.Error);
                return;
            }

            Console.WriteLine("Upisite sadrzaj novog resursa:");
            var validString = Checkers.CheckString(Console.ReadLine(), out string content);

            if (validString)
            {
                resourceQuery.CreateResource(content);
                Printer.ConfirmMessageAndClear("Resurs uspjesno dodan", MessageType.Success);
                return;
            }

            Printer.ConfirmMessageAndClear("Sadrzaj krivo upisan", MessageType.Error);
        }
    }
}
