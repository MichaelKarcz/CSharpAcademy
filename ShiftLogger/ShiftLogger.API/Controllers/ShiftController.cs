using Microsoft.AspNetCore.Mvc;
using ShiftLogger.API.Contracts.Shifts;
using ShiftLogger.API.Interfaces;

namespace ShiftLogger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftController : ControllerBase
{
    private readonly IShiftService _shiftService;

    public ShiftController(IShiftService service)
    {
        _shiftService = service;
    }

    [HttpPost]
    public ActionResult<Shift> CreateShift(Shift shift)
    {
        try
        {
            return Ok(_shiftService.CreateShift(shift));
        }
        catch(Exception e)
        {
            string errorMessage = "There was an error creating the shift. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<List<ShiftDto>> GetAllShiftsForWorker(int id)
    {
        try
        {
            List<ShiftDto> results = _shiftService.GetAllShiftsForWorker(id);
            return results.Count > 0 ? Ok(results) : NotFound("There were no shifts found for a worker with that id.");
        }
        catch (Exception e)
        {
            string errorMessage = "There was an error retrieving the shift. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpGet("api/[controller]/unfinished/{id}")]
    public ActionResult<ShiftDto> GetUnfinishedShiftForWorker(int id)
    {
        try
        {
            var result = _shiftService.GetUnfinishedShiftForWorker(id);
            if (result == null || result.WorkerId == 0)
            {
                return NotFound("There were no unfinished shifts found for a worker with that id.");
            }
            return Ok(result);
        }
        catch (Exception e)
        {
            string errorMessage = "There was an error retrieving the shift. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<Shift> UpdateShift(int id, Shift shift)
    {
        try
        {
            var result = _shiftService.UpdateShift(id, shift);
            if (result == null || result.WorkerId == 0)
            {
                return NotFound("There were no shifts found to update with that id.");
            }
            return Ok(result);
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
            var result = _shiftService.DeleteShift(id);

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
