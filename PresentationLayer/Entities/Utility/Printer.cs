using DataLayer.Entities.Models;
using DataLayer.Enums;

namespace PresentationLayer.Entities.Utility
{
    public static class Printer
    {
        public static void PrintResource(Resource resource, string userName)
        {
            Console.WriteLine(new String('=', 50));
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"ID:{resource.ResourceId} Tag: {resource.NameTag} Autor:{userName}\n" +
                $"{resource.ResourceContent}\n\n" +
                    $"Broj odgovora: {resource.NumberOfReplys} Likes: {resource.NumberOfLikes} Dislikes: {resource.NumberOfDislikes}\n" +
                    $"Napravljeno: {resource.TimeOfPosting}");
            Console.WriteLine("Comments:\n");
            Console.ResetColor();
        }

        public static void PrintComment(Comment comment, string userName, string indent)
        {
            Console.WriteLine(indent + $"ID: {comment.CommentId} Autor: {userName}\n{indent}{comment.CommentContent}\n\n" +
                        $"{indent}Likes: {comment.NumberOfLikes} Dislikes: {comment.NumberOfDislikes}\n" +
                        $"{indent}Napravljeno: {comment.TimeOfPosting}");
            Console.WriteLine(new String('-', 50));
        }

        public static void PrintResourceTagTitle(ResourceTag resourceTag)
        {
            Console.Clear();
            Console.WriteLine(new String('=', 40));
            Console.WriteLine($"\t{Enum.GetName(resourceTag).ToUpper()} POSTS");
            Console.WriteLine(new String('=', 40));
        }

        public static void PrintUserInteractMenu()
        {
            Console.WriteLine("\nIzaberite opciju:\n1 - Deaktiviraj privremeno\n2 - Deaktiviraj trajno\n" +
                "3 - Ponovno aktiviraj\n4 - Nazad na dashboard");
        }

        public static void PrintUserSettingsMenu()
        {
            Console.WriteLine("1 - Promijeni korisnicko ime\n2 - Promijeni lozinku\n3 - Nazad na dashboard\n");
        }

        public static void PrintMainMenu()
        {
            Console.WriteLine("1 - Registracija\n2 - Login\n3 - Izlaz iz aplikacije\n");
        }

        internal static void PrintResourceInteractMenu(string userRole)
        {
            Console.WriteLine("Izaberite jednu od akcija:\n1 - Novi post\t2 - Odgovori na post\t3 - Odgovori na komentar\n" +
                "4 - Lajkaj\t5 - Dislajkaj\n6 - Uredi\t7 - Izbrisi\n8 - Nazad na dashboard");
        }

        public static void PrintDashboard()
        {
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
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            if (messageType == MessageType.Error)
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
            Console.WriteLine($"\nID: {user.UserId}\nKorisnicko ime: {user.UserName}\nLozinka: {user.Password}\n" +
                $"Uloga: {user.Role}\nReputacijaski bodovi: {user.RepPoints}\n");
        }

        public static void PrintUsers(User user)
        {
            Console.WriteLine(new String('_', 70));
            Console.ForegroundColor = ConsoleColor.Black;

            if (user.IsDeactivated)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                if (user.DeactivatedUntil is null)
                    Console.WriteLine($"ID: {user.UserId}\nUsername: {user.UserName}\nRole: {user.Role}\nUser points: {user.RepPoints}\nDeactivated until: INDEFINITLY");
                else
                    Console.WriteLine($"ID: {user.UserId}\nUsername: {user.UserName}\nRole: {user.Role}\nUser points: {user.RepPoints}\nDeactivated until: {user.DeactivatedUntil.Value}");
                Console.ResetColor();
                return;
            }

            if (user.Role == Enum.GetName(UserRole.Organizator))
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"ID: {user.UserId}\nUsername: {user.UserName}\nRole: {user.Role}\nUser points: {user.RepPoints}");
                Console.ResetColor();
                return;
            }

            if (user.IsTrusted)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine($"ID: {user.UserId}\nUsername: {user.UserName}\nRole: {user.Role}\nUser points: {user.RepPoints}");
                Console.ResetColor();
                return;
            }

            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine($"ID: {user.UserId}\nUsername: {user.UserName}\nRole:\n{user.Role}\nUser points: {user.RepPoints}");
            Console.ResetColor();
        }

        public static void PrintTitle(string message)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(new String('=', 30));
            Console.WriteLine($"\t{message}");
            Console.WriteLine(new String('=', 30));
            Console.ResetColor();
        }


    }
}
