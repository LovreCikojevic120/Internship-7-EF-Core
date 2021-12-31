using PresentationLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Entities
{
    public static class ResourceInteract
    {
        public static void Start()
        {
            var validInput = false;

            do
            {
                Printer.PrintResourceInteractMenu();

                validInput = Checkers.CheckForNumber(Console.ReadLine().Trim(), out int result);

                if (validInput)
                {
                    switch (result)
                    {
                        case (int)ResourceInteraction.Comment:
                            //comment
                            break;
                        case (int)ResourceInteraction.Reply:
                            //reply
                            break;
                        case (int)ResourceInteraction.Like:
                            //like
                            break;
                        case (int)ResourceInteraction.Dislike:
                            //dislike
                            break;
                        case (int)ResourceInteraction.None:
                            //go back
                            return;
                        default:
                            Printer.ConfirmMessage("Izabrana opcija ne postoji u izborniku");
                            break;
                    }
                }

                Printer.ConfirmMessage("Unos opcije izbornika neispravan");

            } while (validInput);
    }
}
