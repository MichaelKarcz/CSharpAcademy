using ShiftLogger.Console.Helpers;
using ShiftLogger.Console.Services;
using ShiftLogger.Contracts.Responses.Shifts;
using ShiftLogger.Contracts.Responses.Workers;
using Spectre.Console;
using System.Runtime.CompilerServices;

namespace ShiftLogger.Console.Controllers;

internal class MainMenuController
{
    private readonly ShiftLoggerApiService _shiftLoggerApiService;
    private readonly LoggedInMenuController _loggedInMenuController;
    private readonly EditWorkersMenuController _editWorkersMenuController;

    internal MainMenuController(ShiftLoggerApiService shiftLoggerApiService, LoggedInMenuController loggedInMenuController, EditWorkersMenuController editWorkersMenuController)
    {
        _shiftLoggerApiService = shiftLoggerApiService;
        _loggedInMenuController = loggedInMenuController;
        _editWorkersMenuController = editWorkersMenuController;
    }

    internal async Task RunMainMenuLoopAsync()
    {
        bool exitApplication = false;
        while (!exitApplication)
        {
            string menuChoice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("~Shift Logger - Main Menu~")
                .PageSize(7)
                .AddChoices(new[]
                {
                    "1. Login",
                    "2. View Workers",
                    "3. Edit Workers",
                    "0. [grey]Exit the application[/]"
                }));

            int menuChoiceNumber = int.Parse(menuChoice.Substring(0, 1));

            AnsiConsole.Clear();
            switch(menuChoiceNumber)
            {
                case 0:
                    exitApplication = true;
                    break;
                case 1:
                    await _loggedInMenuController.RunMainLoggedInMenuAsync();
                    break;
                case 2:
                    List<WorkerResponse> allWorkers = await _shiftLoggerApiService.GetAllWorkersAsync();
                    DisplayHelper.ViewWorkers(allWorkers);
                    break;
                case 3:
                    await _editWorkersMenuController.RunMainEditWorkersMenuAsync();
                    break;
                default:
                    break;
            }
        }
    }
}
