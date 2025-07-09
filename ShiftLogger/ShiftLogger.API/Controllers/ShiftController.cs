using Microsoft.AspNetCore.Mvc;
using ShiftLogger.API.Models;
using ShiftLogger.API.Services;

namespace ShiftLogger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftController : ControllerBase
    {
        private readonly ShiftService _shiftService;

        public ShiftController(ShiftService service)
        {
            _shiftService = service;
        }

        [HttpPost]
        public ActionResult<Shift> CreateShift(Shift shift)
        {
            return Ok(_shiftService.CreateShift(shift));
        }

        [HttpGet("{id}")]
        public ActionResult<List<Shift>> GetAllShiftsForWorker(int id)
        {
            return Ok(_shiftService.GetAllShiftsForWorker(id));
        }

        [HttpGet("/unfinished/{id}")]
        public ActionResult<Shift> GetUnfinishedShiftForWorker(int id)
        {
            var result = _shiftService.GetUnfinishedShiftForWorker(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public ActionResult<Shift> UpdateShift(int id, Shift shift)
        {
            var result = _shiftService.UpdateShift(id, shift);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteShift(int id)
        {
            var result = _shiftService.DeleteShift(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
