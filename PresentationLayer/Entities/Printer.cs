using DataLayer.Entities.Models;
using DataLayer.Enums;

namespace PresentationLayer.Entities
{
    public static class Printer
    {
        public static void PrintResource(Resource resource, string userName)
        {
            Console.WriteLine($"ID:{resource.ResourceId} Tag: {resource.NameTag} Autor:{userName}\n" +
                $"Sadrzaj:{resource.ResourceContent}\n" +
                    $"Broj odgovora: {resource.NumberOfReplys} Likes: {resource.NumberOfLikes} Dislikes: {resource.NumberOfDislikes}\n");
        }

        public static void PrintResourceTagTitle(ResourceTag resourceTag)
        {
            Console.Clear();
            Console.Write("=", 10);
            Console.WriteLine($"=============\n\t{Enum.GetName(resourceTag).ToUpper()} POSTS\n==========\n");
        }

        public static void PrintUserInteractMenu()
        {
            Console.WriteLine("1 - Deaktiviraj privremeno\n2 - Deaktiviraj trajno\n3 - Ponovno aktiviraj\n4 - Nazad na dashboard");
        }

        public static void PrintUserSettingsMenu()
        {
            Console.WriteLine("1 - Promijeni korisnicko ime\n2 - Promijeni lozinku\n3 - Nazad na dashboard\n");
        }

        public static void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Dobrodosli, izaberite zelite li se registrirati ili prijaviti!\n" +
                "1 - Registracija\n2 - Login\n3 - Izlaz iz aplikacije\n");
        }

        internal static void PrintResourceInteractMenu(string userRole)
        {
            Console.WriteLine("Izaberite jednu od akcija:\n1 - Novi post\t2 - Odgovori na post\t3 - Odgovori na komentar\n" +
                "4 - Lajkaj\t5 - Dislajkaj\n6 - Uredi\t7 - Izbrisi\n8 - Nazad na dashboard");
        }

        public static void PrintDashboard()
        {
            Console.Clear();
            Console.WriteLine("Izaberite jednu od akcija:\n" +
                "1 - Objavljeni resursi\n2 - Korisnici\n3 - Neodgovoreno\n4 - Popularno\n5 - Moj profil\n6 - Logout\n");
        }

        public static void PrintEntityTagList()
        {
            Console.WriteLine("\nIzaberite kategoriju resursa:\n");
            Console.WriteLine("1 - Dev\n2 - Dizajn\n3 - Marketing\n4 - Multimedija\n5 - Generalno\n6 - Nazad na dashboard\n");
        }

        public static void ConfirmMessageAndClear(string message, MessageType messageType)
        {
            if(messageType == MessageType.Error)
                Console.BackgroundColor = ConsoleColor.Red;
            if (messageType == MessageType.Success)
                Console.BackgroundColor = ConsoleColor.Green;

            Console.WriteLine($"{message}, za nastavak pritisnite bilo koju tipku!");
            Console.ReadKey();
            Console.ResetColor();
            Console.Clear();
        }

        public static void PrintUserPersonal(User user)
        {
            Console.WriteLine($"ID: {user.UserId}\nKorisnicko ime: {user.UserName}\nLozinka: {user.Password}\n" +
                $"Uloga: {user.Role}\nReputacijaski bodovi: {user.RepPoints}");
        }

        public static void PrintUsers(User user)
        {
            if (user.IsDeactivated)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                if (user.DeactivatedUntil is null)
                    Console.WriteLine($"Username: {user.UserName} Role: {user.Role} User points: {user.RepPoints} Deactivated until: INDEFINITLY\n");
                else
                    Console.WriteLine($"Username: {user.UserName} Role: {user.Role} User points: {user.RepPoints} Deactivated until: {user.DeactivatedUntil.Value}\n");
                Console.ResetColor();
                return;
            }

            if (user.Role == "Admin")
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine($"Username: {user.UserName} Role: {user.Role} User points: {user.RepPoints}\n");
                Console.ResetColor();
                return;
            }

            if (user.IsTrusted)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Username: {user.UserName} Role: {user.Role} User points: {user.RepPoints}\n");
                Console.ResetColor();
                return;
            }

            Console.WriteLine($"Username: {user.UserName} Role: {user.Role} User points: {user.RepPoints}\n");
        }

        public static void PrintTitle(string message)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"\n{message}\n");
            Console.ResetColor();
        }


    }
}
