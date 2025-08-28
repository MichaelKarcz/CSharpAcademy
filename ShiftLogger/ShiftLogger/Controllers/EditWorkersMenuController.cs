using ShiftLogger.Console.Helpers;
using ShiftLogger.Console.Services;
using ShiftLogger.Contracts.Requests.Workers;
using Spectre.Console;

namespace ShiftLogger.Console.Controllers;

internal class EditWorkersMenuController
{
    internal static void RunMainEditWorkersMenu()
    {
        bool navigatePrevious = false;
        while (!navigatePrevious)
        {
            string menuChoice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("~Shift Logger~")
                .PageSize(7)
                .AddChoices(new[]
                {
                    "1. View all workers",
                    "2. Add worker",
                    "3. Modify existing worker",
                    "4. Delete existing worker",
                    "0. [grey]Return to previous menu[/]"
                }));

            int menuChoiceNumber = int.Parse(menuChoice.Substring(0, 1));

            switch (menuChoiceNumber)
            {
                case 0:
                    navigatePrevious = true;
                    break;
                case 1:
                    DisplayHelper.ViewAllWorkers();
                    break;
                case 2:
                    AddWorker();
                    break;
                case 3:
                    ModifyExistingWorker();
                    break;
                case 4:
                    DeleteExistingWorker();
                    break;
                default:
                    break;
            }
        }
    }

    private static void AddWorker()
    {
        AnsiConsole.Clear();
        string workerName = AnsiConsole.Prompt(new TextPrompt<string>("Enter the name of the new worker: "));
        if (string.IsNullOrEmpty(workerName))
        {
            AnsiConsole.WriteLine("No worker was added. Press any key to return to the previous menu.");
            AnsiConsole.Console.Input.ReadKey(false);
            return;
        }

        CreateWorkerRequest newWorker = new CreateWorkerRequest() { Name = workerName };
        try
        {
            bool addWorkerSuccessful = ShiftLoggerAPIService.CreateWorker(newWorker);

            if (addWorkerSuccessful)
            {
                AnsiConsole.WriteLine($"{workerName} was added as a new worker successfully!");
            }
            else
            {
                AnsiConsole.WriteLine($"There was an issue adding {workerName} as a new worker.");
            }

        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"\n\nThere was an unexpected issue adding a new worker. More information: {ex.Message}\n");
        }
    }

    private static void ModifyExistingWorker()
    {
        AnsiConsole.Clear();
        throw new NotImplementedException();
    }

    private static void DeleteExistingWorker()
    {
        throw new NotImplementedException();
    }

}
