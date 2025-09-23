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
                    await ViewAllWorkers();
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

    private async Task ViewAllWorkers()
    {
        AnsiConsole.Clear();

        ServiceResult<List<WorkerResponse>> serviceResult = await _shiftLoggerApiService.GetAllWorkersAsync();
        if (serviceResult.Success)
        {
            DisplayHelper.ViewWorkers(serviceResult.Data);
        }
        else
        {
            AnsiConsole.WriteLine($"\nFailed to view all workers: {serviceResult.ErrorMessage}\n");
        }
    }
    
    private async Task AddWorkerAsync()
    {
        AnsiConsole.Clear();
        string workerName = AnsiConsole.Prompt(new TextPrompt<string>("Enter the name of the new worker: "));
        if (string.IsNullOrEmpty(workerName))
        {
            AnsiConsole.WriteLine("\nNo worker was added. Press any key to return to the previous menu.\n");
            AnsiConsole.Console.Input.ReadKey(false);
            return;
        }

        CreateWorkerRequest newWorker = new CreateWorkerRequest() { Name = workerName };
       
        ServiceResult<WorkerResponse> serviceResult = await _shiftLoggerApiService.CreateWorkerAsync(newWorker);

        if (serviceResult.Success && serviceResult.Data != null)
        {
            AnsiConsole.WriteLine($"\n{serviceResult.Data.Name} was added as a new worker successfully!\n");
        }
        else
        {
            AnsiConsole.WriteLine($"\nFailed to add new worker: {serviceResult.ErrorMessage}\n");
        }
    }

    private async Task ModifyExistingWorkerAsync()
    {
        AnsiConsole.Clear();
        ServiceResult<List<WorkerResponse>> getAllWorkersServiceResponse = await _shiftLoggerApiService.GetAllWorkersAsync();
        List<WorkerResponse>? allWorkers = new List<WorkerResponse>();
        if (getAllWorkersServiceResponse.Success)
        {
            allWorkers = getAllWorkersServiceResponse.Data;
        }
        else
        {
            AnsiConsole.WriteLine($"\nFailed to retrieve all workers: {getAllWorkersServiceResponse.ErrorMessage}\n");
            return;
        }


        WorkerResponse? workerToModify = InputHelper.SelectAWorker(allWorkers);
        if (workerToModify == null)
        {
            AnsiConsole.WriteLine("\nAn unexpected error has occurred and the selecter worker could not be processed.\n");
            return;
        }

        string newName = AnsiConsole.Prompt(new TextPrompt<string>("Note that the existing shifts associated with this worker " +
            "will still be associated with the new name.\nEnter the new name for this worker: "));
        if (string.IsNullOrEmpty(newName))
        {
            AnsiConsole.WriteLine("\nNo updates were made to this worker. Press any key to return to the previous menu.\n");
            AnsiConsole.Console.Input.ReadKey(false);
            return;
        }

        UpdateWorkerRequest updateWorkerRequest = new UpdateWorkerRequest() { Id = workerToModify.Id, Name = newName };
        ServiceResult<WorkerResponse?> serviceResult = await _shiftLoggerApiService.UpdateWorkerAsync(updateWorkerRequest.Id, updateWorkerRequest);

        if (serviceResult.Success)
        {
            AnsiConsole.WriteLine("\nThe worker was updated successfully! Press any key to return to the previous menu.\n");
        }
        else
        {
            AnsiConsole.WriteLine($"\nThere was an error updating this worker: {serviceResult.ErrorMessage}");
        }
    }

    private async Task DeleteExistingWorkerAsync()
    {
        AnsiConsole.Clear();
        ServiceResult<List<WorkerResponse>> getAllWorkersServiceResponse = await _shiftLoggerApiService.GetAllWorkersAsync();
        List<WorkerResponse>? allWorkers = new List<WorkerResponse>();
        if (getAllWorkersServiceResponse.Success)
        {
            allWorkers = getAllWorkersServiceResponse.Data;
        }
        else
        {
            AnsiConsole.WriteLine($"\nFailed to retrieve all workers: {getAllWorkersServiceResponse.ErrorMessage}\n");
            return;
        }

        WorkerResponse? workerToDelete = InputHelper.SelectAWorker(allWorkers);
        if (workerToDelete == null)
        {
            AnsiConsole.WriteLine("\nAn unexpected error has occurred and the selecter worker could not be processed.\n");
            return;
        }

        ServiceResult<string> serviceResult = await _shiftLoggerApiService.DeleteWorkerAsync(workerToDelete.Id);

        if (serviceResult.Success)
        {
            AnsiConsole.WriteLine("\nThe worker was deleted successfully! Press any key to return to the previous menu.\n");
            AnsiConsole.Console.Input.ReadKey(false);
            return;
        }
        else
        {
            AnsiConsole.WriteLine($"\nThere was an error deleting this worker: {serviceResult.ErrorMessage}\n");
            return;
        }
    }
}
