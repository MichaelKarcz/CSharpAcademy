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
    public ActionResult<WorkerResponse> CreateWorker(CreateWorkerRequest createWorkerDto)
    {
        try
        {
            Worker createdWorker = _workerService.CreateWorker(new Worker { Name = createWorkerDto.Name });

            if (createdWorker == null)
            {
                return NotFound($"There was an error creating worker \"{createWorkerDto.Name}\".\n");
            }
            return Ok(createdWorker.ToResponse());
        }
        catch (Exception e)
        {
            string errorMessage = "There was an error creating the worker. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpGet]
    public ActionResult<List<WorkerResponse>> GetAllWorkers()
    {
        try
        {
            List<Worker> result = _workerService.GetAllWorkers();

            if (result == null || result.Count < 1)
            {
                return NotFound("There were no workers to retrieve.\n");
            }

            List<WorkerResponse> dtoList = result.Select(w => w.ToResponse()).ToList();

            return Ok(dtoList);
        }
        catch (Exception e)
        {
            string errorMessage = "There was an error retrieving the workers. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<WorkerResponse> GetWorkerById(int id)
    {
        try
        {
            var result = _workerService.GetWorkerById(id);
            if (result == null)
            {
                return NotFound($"There were no workers found with id: {id}.\n");
            }
            return Ok(result.ToResponse());
        }
        catch (Exception e)
        {
            string errorMessage = "There was an error retrieving the worker. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<WorkerResponse> UpdateWorker(int id, UpdateWorkerRequest updateWorkerDto)
    {
        try
        {
            var result = _workerService.UpdateWorker(id, new Worker { Id = updateWorkerDto.Id, Name = updateWorkerDto.Name});
            if (result == null)
            {
                return NotFound($"There were no workers found to update with Id: {id}.\n");
            }
            return Ok(result.ToResponse());
        }
        catch (Exception e)
        {
            string errorMessage = "There was an error updating the worker. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult<string> DeleteWorker(int id)
    {
        try
        {
            var result = _workerService.DeleteWorker(id);
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
