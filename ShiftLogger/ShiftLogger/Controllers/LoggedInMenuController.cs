using ShiftLogger.Console.Helpers;
using ShiftLogger.Console.Services;
using ShiftLogger.Contracts.Requests.Shifts;
using ShiftLogger.Contracts.Responses.Shifts;
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
                    StartShift(loggedInWorker);
                    break;
                case 2:
                    EndShift(loggedInWorker);
                    break;
                case 3:
                    ViewAllShifts(loggedInWorker);
                    break;
                case 4:
                    ModifyShift(loggedInWorker);
                    break;
                case 5:
                    DeleteShift(loggedInWorker);
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

    internal static void StartShift(WorkerResponse loggedInWorker)
    {
        AnsiConsole.Clear();

        if (loggedInWorker == null)
        {
            AnsiConsole.WriteLine("There is no logged in worker to associate this shift with.\n");
            return;
        }

        ShiftResponse? unfinishedShift = ShiftLoggerApiService.GetUnfinishedShiftForWorker(loggedInWorker);
        if (unfinishedShift != null)
        {
            AnsiConsole.WriteLine("\nThere is already an ongoing shift - please finish that shift before starting a new one.\n\n");
            return;
        }

        CreateShiftRequest newShift = new CreateShiftRequest() { WorkerId = loggedInWorker.Id};

        bool shiftCreated = ShiftLoggerApiService.CreateShift(newShift);
        if (!shiftCreated) AnsiConsole.WriteLine("There was an error creating the shift.");
        else AnsiConsole.WriteLine("A new shift has been created successfully with the current time as the start time!\n\n");
    }

    internal static void EndShift(WorkerResponse loggedInWorker)
    {
        AnsiConsole.Clear();

        if (loggedInWorker == null)
        {
            AnsiConsole.WriteLine("There is no logged in worker to find shifts for.\n");
            return;
        }

        ShiftResponse? shiftToFinish = ShiftLoggerApiService.GetUnfinishedShiftForWorker(loggedInWorker);
        if (shiftToFinish == null)
        {
            AnsiConsole.WriteLine("There are no unfinished shifts to finish for this worker.\n\n");
        }
        else
        {
            shiftToFinish.EndTime = DateTime.Now;
            try
            {
                UpdateShiftRequest updatedShift = new UpdateShiftRequest() { Id = shiftToFinish.Id, StartTime = shiftToFinish.StartTime, EndTime = shiftToFinish.EndTime };
                shiftToFinish = ShiftLoggerApiService.UpdateShift(shiftToFinish.Id, updatedShift);

                if (shiftToFinish == null)
                {
                    AnsiConsole.WriteLine("There was an error updating the shift with a finish time.");
                }
                else
                {
                    AnsiConsole.WriteLine($"The shift is now finished with an end time of {shiftToFinish.EndTime.Value.ToString(DisplayHelper.DateFormat)}\n\n");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteLine($"There was an unexpected error attempting to finish the shift for this worker. Additional details: {ex.Message}\n\n");
            }
        }
    }

    internal static void ViewAllShifts(WorkerResponse loggedInWorker)
    {
        AnsiConsole.Clear();

        if (loggedInWorker == null)
        {
            AnsiConsole.WriteLine("There is no logged in worker to find shifts for.\n");
            return;
        }

        try
        {
            List<ShiftResponse> allShifts = ShiftLoggerApiService.GetAllShiftsForWorker(loggedInWorker);

            if (allShifts.Count < 1)
            {
                AnsiConsole.WriteLine("There are no shifts to display for this worker.\n\n");
                return;
            }

            DisplayHelper.ViewShifts(allShifts);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"There was an unexpected error attempting to retrieve and display the shifts for this worker. Additional details: {ex.Message}\n\n");
        }
    }

    internal static void ModifyShift(WorkerResponse loggedInWorker)
    {
        AnsiConsole.Clear();

        if (loggedInWorker == null)
        {
            AnsiConsole.WriteLine("There is no logged in worker to find shifts for.\n");
            return;
        }

        try
        {
            bool shiftUpdated = false;
            List<ShiftResponse> allShifts = ShiftLoggerApiService.GetAllShiftsForWorker(loggedInWorker);

            if (allShifts.Count < 1)
            {
                AnsiConsole.WriteLine($"There are no shifts to modify for this worker.\n\n");
                return;
            }

            ShiftResponse selectedShift = InputHelper.SelectAShift(allShifts);


            List<string> updatesToMake = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                    .Title("What would you like to [green]update[/] about the shift?")
                    .NotRequired()
                    .PageSize(3)
                    .InstructionsText(
                        "[grey](Press [blue]<space>[/] to toggle a selection, " +
                        "and press [green]<enter>[/] to accept)[/]")
                    .AddChoices(new[]
                    {
                        "Start Time", "End Time"
                    }));

            if (updatesToMake.Count > 0)
            {
                if (updatesToMake.Contains("Start Time"))
                {
                    selectedShift.StartTime = InputHelper.GetValidStartDate($"Enter the new start time in the format '{DisplayHelper.DateFormat}': ");
                    shiftUpdated = true;
                }
                if (updatesToMake.Contains("End Time"))
                {
                    selectedShift.EndTime = InputHelper.GetValidEndDate($"Enter the new end time in the format '{DisplayHelper.DateFormat}': ", selectedShift.StartTime);
                    shiftUpdated = true;
                }
            }
            else
            {
                AnsiConsole.WriteLine("\nNo changes were made.\n");
            }

            if (shiftUpdated)
            {
                UpdateShiftRequest updateShiftRequest = new UpdateShiftRequest() { Id = selectedShift.Id, StartTime = selectedShift.StartTime, EndTime = selectedShift.EndTime};
                ShiftLoggerApiService.UpdateShift(selectedShift.Id, updateShiftRequest);
                AnsiConsole.WriteLine("The shift has been updated!\n\n");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"There was an unexpected error attempting to modify the shift for this worker. Additional details: {ex.Message}\n\n");
        }
    }

    internal static void DeleteShift(WorkerResponse loggedInWorker)
    {
        AnsiConsole.Clear();

        if (loggedInWorker == null)
        {
            AnsiConsole.WriteLine("There is no logged in worker to find shifts for.\n");
            return;
        }

        try
        {
            List<ShiftResponse> allShifts = ShiftLoggerApiService.GetAllShiftsForWorker(loggedInWorker);

            if (allShifts.Count < 1)
            {
                AnsiConsole.WriteLine($"There are no shifts to delete for this worker.\n\n");
                return;
            }

            ShiftResponse selectedShift = InputHelper.SelectAShift(allShifts);

            bool confirmDelete = AnsiConsole.Prompt(
                    new TextPrompt<bool>($"Are you sure you'd like to delete this record?"
                                         + $"\n{selectedShift}")
                    .AddChoice(true)
                    .AddChoice(false)
                    .DefaultValue(false)
                    .WithConverter(choice => choice ? "Yes" : "No"));

            if (confirmDelete)
            {
                bool shiftDeleted = ShiftLoggerApiService.DeleteShift(selectedShift.Id);
                if (shiftDeleted)
                {
                    AnsiConsole.WriteLine("The shift has been deleted successfully.\n");
                }
                else AnsiConsole.WriteLine("There was an error deleting the shift.\n");
            }
            else AnsiConsole.WriteLine("\nThe shift was not deleted.\n");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"There was an unexpected error attempting to delete the shift for this worker. Additional details: {ex.Message}\n\n");
        }
    }
}