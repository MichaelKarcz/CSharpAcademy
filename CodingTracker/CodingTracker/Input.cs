using Spectre.Console;

namespace CodingTracker
{
    internal static class Input
    {

        public static int MainMenu()
        {
            string menuChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Which [green]operation[/] would you like to perform?")
                .PageSize(5)
                .AddChoices(new[]
                {
                    "1) [green]View[/] all records",
                    "2) [green]Insert[/] a new record",
                    "3) [green]Update[/] an existing record",
                    "4) [red]Delete[/] an existing record",
                    "0) [grey]Exit the application[/]"
                }));

            int menuChoiceNumber = Int32.Parse(menuChoice.Substring(0,1));

            return menuChoiceNumber;
        }

        public static string GetDate()
        {
            string date = "";




            return date;
        }

        public static string GetStartTime()
        {
            string startTime = "";

            return startTime;
        }

        public static string GetEndTime()
        {
            string endTime = "";

            return endTime;
        }

    }
}
