using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Entities
{
    public static class Printer
    {
        public static void PrintMainMenu()
        {

            Console.WriteLine("Dobrodosli, izaberite zelite li se registrirati ili prijaviti!\n" +
                "1 - Registracija\n2 - Login\n3 - Izlaz iz aplikacije\n");
        }

        internal static void PrintResourceInteractMenu()
        {
            Console.WriteLine("Izaberite jednu od akcija:\n1 - Komentiraj resurs\n2 - Odgovori na komentar\n" +
                "3 - Lajkaj komentar\n4 - Dislajkaj komentar\n5 - Povratak na dashboard\n");
        }

        public static void PrintDashboard()
        {
            Console.WriteLine("Izaberite jednu od akcija:\n" +
                "1 - Objavljeni resursi\n2 - Korisnici\n3 - Neodgovoreno\n4 - Popularno\n5 - Moj profil\n6 - Logout\n");
        }

        public static void PrintResourceTagList()
        {
            Console.WriteLine("1 - Dev\n2 - Dizajn\n3 - Marketing\n4 - Multimedija\n5 - Generalno\n");
        }

        public static void ConfirmMessage(string message)
        {
            Console.WriteLine($"{message}, za nastavak pritisnite bilo koju tipku!");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
