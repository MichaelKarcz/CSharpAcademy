using ShiftLogger.Console.Helpers;
using ShiftLogger.Console.Services;
using ShiftLogger.Contracts.Requests.Workers;
using ShiftLogger.Contracts.Responses.Workers;
using Spectre.Console;

namespace ShiftLogger.Console.Controllers;

internal class EditWorkersMenuController
{

    private readonly ShiftLoggerApiService _shiftLoggerApiService;

    internal EditWorkersMenuController(ShiftLoggerApiService shiftLoggerApiService)
    {
        _shiftLoggerApiService = shiftLoggerApiService;
    }


    internal async Task RunMainEditWorkersMenuAsync()
    {
        bool navigatePrevious = false;
        while (!navigatePrevious)
        {
            string menuChoice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("~Shift Logger - Edit Workers~")
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

            AnsiConsole.Clear();
            switch (menuChoiceNumber)
            {
                case 0:
                    navigatePrevious = true;
                    break;
                case 1:
                    List<WorkerResponse> allWorkers = await _shiftLoggerApiService.GetAllWorkersAsync();
                    DisplayHelper.ViewWorkers(allWorkers);
                    break;
                case 2:
                    await AddWorkerAsync();
                    break;
                case 3:
                    await ModifyExistingWorkerAsync();
                    break;
                case 4:
                    await DeleteExistingWorkerAsync();
                    break;
                default:
                    break;
            }
        }
    }

    private async Task AddWorkerAsync()
    {
        AnsiConsole.Clear();
        string workerName = AnsiConsole.Prompt(new TextPrompt<string>("Enter the name of the new worker: "));
        if (string.IsNullOrEmpty(workerName))
        {
            AnsiConsole.WriteLine("No worker was added. Press any key to return to the previous menu.\n");
            AnsiConsole.Console.Input.ReadKey(false);
            return;
        }

        CreateWorkerRequest newWorker = new CreateWorkerRequest() { Name = workerName };
        try
        {
            bool addWorkerSuccessful = await _shiftLoggerApiService.CreateWorkerAsync(newWorker);

            if (addWorkerSuccessful)
            {
                AnsiConsole.WriteLine($"{workerName} was added as a new worker successfully!\n");
            }
            else
            {
                AnsiConsole.WriteLine($"There was an issue adding {workerName} as a new worker.\n");
            }

        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"\n\nThere was an unexpected issue adding a new worker. More information: {ex.Message}\n");
        }
    }

    private async Task ModifyExistingWorkerAsync()
    {
        AnsiConsole.Clear();
        List<WorkerResponse> allWorkers = await _shiftLoggerApiService.GetAllWorkersAsync();

        WorkerResponse? workerToModify = InputHelper.SelectAWorker(allWorkers);
        if (workerToModify == null)
        {
            return;
        }

        string newName = AnsiConsole.Prompt(new TextPrompt<string>("Note that the existing shifts associated with this worker " +
            "will still be associated with the new name.\nEnter the new name for this worker: "));
        if (string.IsNullOrEmpty(newName))
        {
            AnsiConsole.WriteLine("No updates were made to this worker. Press any key to return to the previous menu.\n");
            AnsiConsole.Console.Input.ReadKey(false);
            return;
        }

        UpdateWorkerRequest updateWorkerRequest = new UpdateWorkerRequest() { Id = workerToModify.Id, Name = newName };
        WorkerResponse? updatedWorker;

        try
        {
            updatedWorker = await _shiftLoggerApiService.UpdateWorkerAsync(updateWorkerRequest.Id, updateWorkerRequest);
        }
        catch(Exception ex)
        {
            AnsiConsole.WriteLine($"There was an unexpected error attempting to update this worker. More information: {ex.Message}\n");
        }

    }

    private async Task DeleteExistingWorkerAsync()
    {
        AnsiConsole.Clear();
        List<WorkerResponse> allWorkers = await _shiftLoggerApiService.GetAllWorkersAsync();
        WorkerResponse? workerToDelete = InputHelper.SelectAWorker(allWorkers);
        if (workerToDelete == null)
        {
            return;
        }

        try
        {
            if (await _shiftLoggerApiService.DeleteWorkerAsync(workerToDelete.Id))
            {
                AnsiConsole.WriteLine($"The worker with Id = {workerToDelete.Id}, {workerToDelete.Name}, was deleted successfully!\n");
            }
            else AnsiConsole.WriteLine($"There was an error deleting {workerToDelete.Name}.\n");

        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"There was an unexpected error attempting to delete this worker. More information: {ex.Message}\n");
        }
    }
}
