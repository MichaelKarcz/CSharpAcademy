using Microsoft.AspNetCore.Mvc;
using ShiftLogger.API.Models.Shifts;
using ShiftLogger.API.Models.Workers;
using ShiftLogger.API.Interfaces;
using ShiftLogger.Contracts.Requests.Shifts;
using ShiftLogger.Contracts.Responses.Shifts;

namespace ShiftLogger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftController : ControllerBase
{
    private readonly IShiftService _shiftService;
    private readonly IWorkerService _workerService;

    public ShiftController(IShiftService service, IWorkerService workerService)
    {
        _shiftService = service;
        _workerService = workerService;
    }

    [HttpPost]
    public async Task<ActionResult<ShiftResponse>> CreateShiftAsync(CreateShiftRequest createShiftRequest)
    {
        try
        {
            Worker? worker = await _workerService.GetWorkerByIdAsync(createShiftRequest.WorkerId);
            if (worker == null)
            {
                return BadRequest(new { error = $"The shift could not be created because a worker could not be found with the provided Id: {createShiftRequest.WorkerId}\n" });
            }

            Shift result = await _shiftService.CreateShiftAsync(new Shift
            {
                StartTime = createShiftRequest.StartTime,
                EndTime = createShiftRequest.EndTime,
                WorkerId = worker.Id,
                Worker = worker
            });

            return Ok(result.ToResponse());
        }
        catch(Exception e)
        {
            string errorMessage = "There was an internal server error while creating the shift. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return StatusCode(500, new { error = errorMessage });
        }
    }

    [HttpGet("{workerId}")]
    public async Task<ActionResult<List<ShiftResponse>>> GetAllShiftsForWorkerAsync(int workerId)
    {
        try
        {
            List<Shift> results = await _shiftService.GetAllShiftsForWorkerAsync(workerId);
            if (results == null || results.Count < 1)
            {
                return NotFound($"There were no shifts found for a worker with Id: {workerId}.\n");
            }
            return Ok(results.Select(sh => sh.ToResponse()).ToList());
        }
        catch (Exception e)
        {
            string errorMessage = "There was an internal server error while retrieving the shifts. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return StatusCode(500, new { error = errorMessage });
        }
    }

    [HttpGet("unfinished/{workerId}")]
    public async Task<ActionResult<ShiftResponse>> GetUnfinishedShiftForWorkerAsync(int workerId)
    {
        try
        {
            Shift? result = await _shiftService.GetUnfinishedShiftForWorkerAsync(workerId);
            if (result == null)
            {
                return NotFound($"There were no unfinished shifts found for a worker with Id: {workerId}.\n");
            }
            return Ok(result.ToResponse());
        }
        catch (Exception e)
        {
            string errorMessage = "There was an internal server error while retrieving the shift. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return StatusCode(500, new { error = errorMessage });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ShiftResponse>> UpdateShiftAsync(int id, UpdateShiftRequest shift)
    {
        try
        {
            Shift? existingShift = await _shiftService.GetShiftByIdAsync(id);
            if (existingShift == null)
            {
                return NotFound($"There were no shifts found to update with Id: {id}.\n");
            }

            Shift updatedShift = new Shift { 
                Id = existingShift.Id,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                WorkerId = existingShift.WorkerId,
                Worker = existingShift.Worker 
            };

            Shift? result = await _shiftService.UpdateShiftAsync(id, updatedShift);
            if (result == null)
            {
                return NotFound($"There were no shifts found to update with Id: {id}.\n");
            }
            return Ok(result.ToResponse());
        }
        catch (Exception e)
        {
            string errorMessage = "There was an internal server error while updating the shift. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return StatusCode(500, new { error = errorMessage });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteShiftAsync(int id)
    {
        try
        {
            string result = await _shiftService.DeleteShiftAsync(id);

            if (string.IsNullOrEmpty(result))
            {
                return NotFound("There were no shifts found to delete with that id.\n");
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            string errorMessage = "There was an internal server error while deleting the shift. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return StatusCode(500, new { error = errorMessage });
        }
    }
}
