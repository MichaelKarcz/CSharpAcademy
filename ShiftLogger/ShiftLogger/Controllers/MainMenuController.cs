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
                    RunMainLoggedInMenu();
                    break;
                case 2:
                    ViewAllWorkers();
                    break;
                default:
                    break;
            }
        }
    }

    
    #region MainMenu Methods

    internal static void ViewAllWorkers()
    {
        AnsiConsole.Clear();

        List<WorkerResponse> allWorkers = ShiftLoggerAPIService.GetAllWorkers();

        if (allWorkers.Count == 0)
        {
            AnsiConsole.WriteLine("\n\nThere are no workers to view.\n\n");
            return;
        }

        Table table = new Table();
        table.AddColumn(new TableColumn("Id").Centered().NoWrap());
        table.AddColumn(new TableColumn("Name").Centered().NoWrap());
        foreach (WorkerResponse worker in allWorkers)
        {
            table.AddRow(worker.Id.ToString(), worker.Name);
        }
        table.Border(TableBorder.Heavy);
        table.ShowRowSeparators();
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();

    }

    
    internal static WorkerResponse SelectWorker(List<WorkerResponse> workers)
    {
        if (workers == null || workers.Count == 0)
        {
            AnsiConsole.WriteLine("There are no workers to choose from for this operation.");
            return new WorkerResponse();
        }
        WorkerResponse worker = AnsiConsole.Prompt(new SelectionPrompt<WorkerResponse>()
            .Title("Select a Worker")
            .PageSize(10)
            .MoreChoicesText("[grey](Use the up and down arrow keys to reveal more workers[/]")
            .AddChoices(workers));

        return worker;

    }

    #endregion MainMenu Methods
}
