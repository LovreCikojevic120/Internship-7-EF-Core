namespace PresentationLayer.Entities
{
    static class StartProgram
    {
        static void Main()
        {
            var notExit = false;
            do
            {
                notExit = MenuManager.MainMenuSwitcher();

            }while(notExit);
        }
    }
}