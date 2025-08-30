using ShiftLogger.Console.Services;
using ShiftLogger.Contracts.Responses.Workers;
using Spectre.Console;

namespace ShiftLogger.Console.Controllers;

internal class LoggedInMenuController
{
    internal static void RunMainLoggedInMenu()
    {
        AnsiConsole.Clear();
        WorkerResponse? loggedInWorker = LoginPrompt();

        if (loggedInWorker == null)
        {
            return;
        }

        AnsiConsole.Clear();
        bool logout = false;
        while (!logout)
        {
            string menuChoice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title($"~Welcome, {loggedInWorker.Name}~")
                .PageSize(7)
                .AddChoices(new[]
                {
                    "1. Start Shift",
                    "2. End Shift",
                    "3. View All Shifts",
                    "4. Modify Shift",
                    "5. Delete Shift",
                    "0. [grey]Logout and return to the main menu[/]"
                }));

            int menuChoiceNumber = int.Parse(menuChoice.Substring(0, 1));

            AnsiConsole.Clear();
            switch (menuChoiceNumber)
            {
                case 0:
                    logout = true;
                    loggedInWorker = null;
                    break;
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
            }
        }
    }

    internal static WorkerResponse? LoginPrompt()
    {
        int workerId = AnsiConsole.Prompt(new TextPrompt<int>("\nEnter your worker Id: ")
            .ValidationErrorMessage("Please enter an Id. Your input should be a number."));
        WorkerResponse? worker = ShiftLoggerApiService.GetWorkerById(workerId);

        while ((worker == null || string.IsNullOrEmpty(worker.Name)) && workerId != 0)
        {
            workerId = AnsiConsole.Prompt(new TextPrompt<int>("\n\nNo workers were found with that Id. Please enter a valid Id or enter 0 to return to the previous menu: "));
            worker = ShiftLoggerApiService.GetWorkerById(workerId);
        }

        AnsiConsole.Clear();
        return worker;
    }
}
