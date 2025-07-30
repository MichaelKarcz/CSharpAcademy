using ShiftLogger.Models;
using ShiftLogger.Services;
using Spectre.Console;

namespace ShiftLogger.Controllers;

internal static class MenuController
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
                    "0. [grey]Exit the application[/]"
                }));

            int menuChoiceNumber = Int32.Parse(menuChoice.Substring(0, 1));

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

    internal static void RunMainLoggedInMenu()
    {
        AnsiConsole.Clear();
        Worker loggedInWorker = LoginPrompt();

        if (loggedInWorker == null || loggedInWorker.Id == 0)
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

            int menuChoiceNumber = Int32.Parse(menuChoice.Substring(0, 1));

            switch (menuChoiceNumber)
            {
                case 0:
                    logout = true;
                    loggedInWorker = new Worker();
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

    internal static void ViewAllWorkers()
    {
        AnsiConsole.Clear();

        List<Worker> allWorkers = ShiftLoggerAPIService.GetAllWorkers();

        if (allWorkers.Count == 0)
        {
            AnsiConsole.WriteLine("\n\nThere are no workers to view.\n\n");
            return;
        }

        Table table = new Table();
        table.AddColumn(new TableColumn("Worker Id").Centered().NoWrap());
        table.AddColumn(new TableColumn("Name").Centered().NoWrap());
        foreach (Worker worker in allWorkers)
        {
            table.AddRow(worker.Id.ToString(), worker.Name);
        }
        table.Border(TableBorder.Heavy);
        table.ShowRowSeparators();
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();

    }

    internal static Worker LoginPrompt()
    {
        int workerId = AnsiConsole.Prompt<int>(new TextPrompt<int>("\nEnter your worker Id: ")
            .ValidationErrorMessage("Please enter an Id. Your input should be a number."));
        Worker worker = ShiftLoggerAPIService.GetWorkerById(workerId);

        while (string.IsNullOrEmpty(worker.Name) && workerId != 0)
        {
            workerId = AnsiConsole.Prompt<int>(new TextPrompt<int>("\n\nNo workers were found with that Id. Please enter a valid Id or enter 0 to return to the previous menu: "));
            worker = ShiftLoggerAPIService.GetWorkerById(workerId);
        }

        AnsiConsole.Clear();
        return worker ?? new Worker();
    }

    internal static Worker SelectWorker(List<Worker> workers)
    {
        if (workers == null || workers.Count == 0)
        {
            AnsiConsole.WriteLine("There are no workers to choose from for this operation.");
            return new Worker();
        }
        Worker worker = AnsiConsole.Prompt(new SelectionPrompt<Worker>()
            .Title("Select a Worker")
            .PageSize(10)
            .MoreChoicesText("[grey](Use the up and down arrow keys to reveal more workers[/]")
            .AddChoices(workers));

        return worker;

    }
    
}
