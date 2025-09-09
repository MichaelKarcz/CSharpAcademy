using Microsoft.AspNetCore.Mvc;
using ShiftLogger.API.Interfaces;
using ShiftLogger.API.Models.Workers;
using ShiftLogger.Contracts.Requests.Workers;
using ShiftLogger.Contracts.Responses.Workers;

namespace ShiftLogger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkerController : ControllerBase
{
    private readonly IWorkerService _workerService;

    public WorkerController(IWorkerService workerService)
    {
        _workerService = workerService;
    }

    [HttpPost]
    public async Task<ActionResult<WorkerResponse>> CreateWorkerAsync(CreateWorkerRequest createWorkerDto)
    {
        try
        {
            Worker createdWorker = await _workerService.CreateWorkerAsync(new Worker { Name = createWorkerDto.Name });

            if (createdWorker == null)
            {
                return NotFound($"There was an error creating worker \"{createWorkerDto.Name}\".\n");
            }
            return Ok(createdWorker.ToResponse());
        }
        catch (Exception e)
        {
            string errorMessage = "There was an internal server error while creating the worker. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return StatusCode(500, new { error = errorMessage });
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkerResponse>>> GetAllWorkersAsync()
    {
        try
        {
            List<Worker> result = await _workerService.GetAllWorkersAsync();

            if (result == null || result.Count < 1)
            {
                return NotFound("There were no workers to retrieve.\n");
            }

            List<WorkerResponse> dtoList = result.Select(w => w.ToResponse()).ToList();

            return Ok(dtoList);
        }
        catch (Exception e)
        {
            string errorMessage = "There was an internal server error while retrieving the workers. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return StatusCode(500, new { error = errorMessage });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkerResponse>> GetWorkerByIdAsync(int id)
    {
        try
        {
            var result = await _workerService.GetWorkerByIdAsync(id);
            if (result == null)
            {
                return NotFound($"There were no workers found with id: {id}.\n");
            }
            return Ok(result.ToResponse());
        }
        catch (Exception e)
        {
            string errorMessage = "There was an internal server error while retrieving the worker. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return StatusCode(500, new { error = errorMessage });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WorkerResponse>> UpdateWorkerAsync(int id, UpdateWorkerRequest updateWorkerDto)
    {
        try
        {
            var result = await _workerService.UpdateWorkerAsync(id, new Worker { Id = updateWorkerDto.Id, Name = updateWorkerDto.Name});
            if (result == null)
            {
                return NotFound($"There were no workers found to update with Id: {id}.\n");
            }
            return Ok(result.ToResponse());
        }
        catch (Exception e)
        {
            string errorMessage = "There was an internal server error while updating the worker. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return StatusCode(500, new { error = errorMessage });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteWorkerAsync(int id)
    {
        try
        {
            var result = await _workerService.DeleteWorkerAsync(id);
            if (string.IsNullOrEmpty(result))
            {
                return NotFound("There were no workers found to delete with that id.\n");
            }
            return Ok(result);
        }
        catch (Exception e)
        {
            string errorMessage = "There was an error deleting the worker. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }
}
