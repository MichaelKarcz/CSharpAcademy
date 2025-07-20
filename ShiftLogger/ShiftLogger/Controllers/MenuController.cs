using ShiftLogger.Models;
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
                .PageSize(5)
                .AddChoices(new[]
                {
                    "1. Start Shift",
                    "2. End Shift",
                    "3. Modify Shift",
                    "4. Delete Shift",
                    "0. [grey]Exit the application[/]"
                }));

            int menuChoiceNumber = Int32.Parse(menuChoice.Substring(0, 1));

            switch(menuChoiceNumber)
            {
                case 0:
                    exitApplication = true;
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
