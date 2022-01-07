using PresentationLayer.Entities.Utility;

namespace PresentationLayer.Entities
{
    static class StartProgram
    {
        static void Main()
        {
            TaskManager.Tasker(MenuManager.MainMenuSwitcher);
        }
    }
}