using ShiftLogger.Console.Helpers;
using ShiftLogger.Console.Services;
using ShiftLogger.Contracts.Responses.Shifts;
using ShiftLogger.Contracts.Responses.Workers;
using Spectre.Console;

namespace ShiftLogger.Console.Controllers;

internal static class MainMenuController
{
    internal static void RunMainMenuLoop()
    {
        bool exitApplication = false;
        while (!exitApplication)
        {
            string menuChoice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("~Shift Logger~")
                .PageSize(7)
                .AddChoices(new[]
                {
                    "1. Login",
                    "2. View Workers",
                    "3. Edit Workers",
                    "0. [grey]Exit the application[/]"
                }));

            int menuChoiceNumber = int.Parse(menuChoice.Substring(0, 1));

            switch(menuChoiceNumber)
            {
                case 0:
                    exitApplication = true;
                    break;
                case 1:
                    LoggedInMenuController.RunMainLoggedInMenu();
                    break;
                case 2:
                    DisplayHelper.ViewAllWorkers();
                    break;
                case 3:
                    EditWorkersMenuController.RunMainEditWorkersMenu();
                    break;
                default:
                    break;
            }
        }
    }
}
