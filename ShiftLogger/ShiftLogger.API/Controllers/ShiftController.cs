using Microsoft.AspNetCore.Mvc;
using ShiftLogger.API.Contracts.Shifts;
using ShiftLogger.API.Contracts.Workers;
using ShiftLogger.API.Interfaces;

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
    public ActionResult<ShiftDto> CreateShift(CreateShiftDto createShiftDto)
    {
        try
        {
            Worker? worker = _workerService.GetWorkerById(createShiftDto.WorkerId);
            if (worker == null)
            {
                return BadRequest(new { error = $"The shift could not be created because a worker could not be found with the provided Id: {createShiftDto.WorkerId}" });
            }

            Shift result = _shiftService.CreateShift(new Shift
            {
                StartTime = createShiftDto.StartTime,
                EndTime = createShiftDto.EndTime,
                WorkerId = worker.Id,
                Worker = worker
            });

            return Ok(result.ToDto());
        }
        catch(Exception e)
        {
            string errorMessage = "There was an error creating the shift. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpGet("{workerId}")]
    public ActionResult<List<ShiftDto>> GetAllShiftsForWorker(int workerId)
    {
        try
        {
            List<Shift> results = _shiftService.GetAllShiftsForWorker(workerId);
            if (results == null || results.Count < 1)
            {
                return NotFound($"There were no shifts found for a worker with Id: {workerId}.");
            }
            return Ok(results.Select(sh => sh.ToDto()).ToList());
        }
        catch (Exception e)
        {
            string errorMessage = "There was an error retrieving the shifts. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpGet("api/[controller]/unfinished/{id}")]
    public ActionResult<ShiftDto> GetUnfinishedShiftForWorker(int id)
    {
        try
        {
            Shift? result = _shiftService.GetUnfinishedShiftForWorker(id);
            if (result == null)
            {
                return NotFound($"There were no unfinished shifts found for a worker with Id: {id}.");
            }
            return Ok(result.ToDto());
        }
        catch (Exception e)
        {
            string errorMessage = "There was an error retrieving the shift. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<Shift> UpdateShift(int id, UpdateShiftDto shift)
    {
        try
        {
            // TODO: Finish updating this to verify that the shift exists, and then passing in
            // a new shift with all of the data between the given shift and the found shift
            Shift? shiftToUpdate = _shiftService.GetShiftById(id);

            Shift? result = _shiftService.UpdateShift(id, new Shift {StartTime = shift.StartTime, EndTime = shift.EndTime));
            if (result == null)
            {
                return NotFound("There were no shifts found to update with that id.");
            }
            return Ok(result.ToDto());
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
