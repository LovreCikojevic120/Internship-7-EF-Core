namespace PresentationLayer.Entities
{
    static class StartProgram
    {
        static void Main()
        {
            var notExit = false;
            do
            {
                Console.WriteLine("Dobrodosli, izaberite jednu od opcija:");
                //print menu
                notExit = MenuManager.MainMenuSwitcher();

            }while(notExit);
        }
    }
}