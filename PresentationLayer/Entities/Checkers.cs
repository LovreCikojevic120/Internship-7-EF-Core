namespace PresentationLayer.Entities
{
    public static class Checkers
    {
        public static bool CheckString(string suspectString, out string result)
        {
            if (string.IsNullOrWhiteSpace(suspectString) || suspectString.Length < 5)
            {
                Printer.ConfirmMessageAndClear("Polje neispravno upisano");
                result = string.Empty;
                return false;
            }
            result = suspectString;
            return true;
        }
        public static bool CheckForNumber(string suspectString, out int number)
        {
            if (int.TryParse(suspectString, out int result))
            {
                number = result;
                return true;
            }

            number = 0;
            return false;
        }
    }
}
