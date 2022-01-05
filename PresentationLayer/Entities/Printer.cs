using DomainLayer.DatabaseEnums;

namespace PresentationLayer.Entities
{
    public static class Printer
    {
        public static void PrintResourceTagTitle(ResourceTag resourceTag)
        {
            Console.Clear();
            Console.WriteLine($"==========\n{Enum.GetName(resourceTag).ToUpper()} POSTS\n==========\n");
        }

        public static void PrintUserInteractMenu()
        {
            Console.WriteLine("1 - Deaktiviraj privremeno\n2 - Deaktiviraj trajno\n3 - Ponovno aktiviraj\n4 - Nazad na dashboard");
        }

        public static void PrintUserSettingsMenu()
        {
            Console.WriteLine("1 - Promijeni korisnicko ime\n2 - Promijeni lozinku\n");
        }

        public static void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Dobrodosli, izaberite zelite li se registrirati ili prijaviti!\n" +
                "1 - Registracija\n2 - Login\n3 - Izlaz iz aplikacije\n");
        }

        internal static void PrintResourceInteractMenu(string userRole)
        {
            Console.WriteLine("Izaberite jednu od akcija:\n1 - Komentiraj post\n2 - Odgovori na komentar\n" +
                "3 - Lajkaj\n4 - Dislajkaj\n5 - Nazad na dashboard\n");
        }

        public static void PrintDashboard()
        {
            Console.Clear();
            Console.WriteLine("Izaberite jednu od akcija:\n" +
                "1 - Objavljeni resursi\n2 - Korisnici\n3 - Neodgovoreno\n4 - Popularno\n5 - Moj profil\n6 - Logout\n");
        }

        public static void PrintEntityTagList()
        {
            Console.WriteLine("1 - Dev\n2 - Dizajn\n3 - Marketing\n4 - Multimedija\n5 - Generalno\n6 - Nazad na dashboard\n");
        }

        public static void ConfirmMessage(string message)
        {
            Console.WriteLine($"{message}, za nastavak pritisnite bilo koju tipku!");
            Console.ReadKey();
            Console.Clear();
        }

        public static void PrintTitle(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
        }


    }
}
