using ShiftLogger.Contracts.Responses.Workers;
using ShiftLogger.Contracts.Responses.Shifts;
using ShiftLogger.Contracts.Requests.Workers;
using ShiftLogger.Contracts.Requests.Shifts;
using ShiftLogger.Console.Services;
using ShiftLogger.Console.Controllers;
using Spectre.Console;

namespace ShiftLogger.Console;
public class Program()
{
    public async static Task Main(string[] args)
    {
        AnsiConsole.WriteLine("Starting App...\n\n");

        ShiftLoggerApiService shiftLoggerApiService = new ShiftLoggerApiService();

        EditWorkersMenuController editWorkersMenuController = new EditWorkersMenuController(shiftLoggerApiService);
        LoggedInMenuController loggedInMenuController = new LoggedInMenuController(shiftLoggerApiService);
        MainMenuController mainMenuController = new MainMenuController(shiftLoggerApiService, loggedInMenuController, editWorkersMenuController);

        AnsiConsole.Clear();
        await mainMenuController.RunMainMenuLoopAsync();

        AnsiConsole.WriteLine("\n\nEnd of app...");
    }
}