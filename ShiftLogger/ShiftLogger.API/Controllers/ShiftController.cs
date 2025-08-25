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
    public ActionResult<ShiftResponse> CreateShift(CreateShiftRequest createShiftRequest)
    {
        try
        {
            Worker? worker = _workerService.GetWorkerById(createShiftRequest.WorkerId);
            if (worker == null)
            {
                return BadRequest(new { error = $"The shift could not be created because a worker could not be found with the provided Id: {createShiftRequest.WorkerId}" });
            }

            Shift result = _shiftService.CreateShift(new Shift
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
            string errorMessage = "There was an error creating the shift. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpGet("{workerId}")]
    public ActionResult<List<ShiftResponse>> GetAllShiftsForWorker(int workerId)
    {
        try
        {
            List<Shift> results = _shiftService.GetAllShiftsForWorker(workerId);
            if (results == null || results.Count < 1)
            {
                return NotFound($"There were no shifts found for a worker with Id: {workerId}.");
            }
            return Ok(results.Select(sh => sh.ToResponse()).ToList());
        }
        catch (Exception e)
        {
            string errorMessage = "There was an error retrieving the shifts. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpGet("api/[controller]/unfinished/{workerId}")]
    public ActionResult<ShiftResponse> GetUnfinishedShiftForWorker(int workerId)
    {
        try
        {
            Shift? result = _shiftService.GetUnfinishedShiftForWorker(workerId);
            if (result == null)
            {
                return NotFound($"There were no unfinished shifts found for a worker with Id: {workerId}.");
            }
            return Ok(result.ToResponse());
        }
        catch (Exception e)
        {
            string errorMessage = "There was an error retrieving the shift. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<ShiftResponse> UpdateShift(int id, UpdateShiftRequest shift)
    {
        try
        {
            Shift? existingShift = _shiftService.GetShiftById(id);
            if (existingShift == null)
            {
                return NotFound($"There were no shifts found to update with Id: {id}.");
            }

            Shift updatedShift = new Shift { 
                Id = existingShift.Id,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                WorkerId = existingShift.WorkerId,
                Worker = existingShift.Worker 
            };

            Shift? result = _shiftService.UpdateShift(id, updatedShift);
            if (result == null)
            {
                return NotFound($"There were no shifts found to update with Id: {id}.");
            }
            return Ok(result.ToResponse());
        }
        catch (Exception e)
        {
            string errorMessage = "There was an error updating the shift. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult<string> DeleteShift(int id)
    {
        try
        {
            string result = _shiftService.DeleteShift(id);

            if (string.IsNullOrEmpty(result))
            {
                return NotFound("There were no shifts found to delete with that id.");
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            string errorMessage = "There was an error deleting the shift. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }
}
