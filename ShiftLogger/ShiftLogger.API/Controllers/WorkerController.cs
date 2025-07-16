using Microsoft.AspNetCore.Mvc;
using ShiftLogger.API.Interfaces;
using ShiftLogger.API.Models;
using ShiftLogger.API.Services;

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
    public ActionResult<Worker> CreateWorker(Worker worker)
    {
        try
        {
            return Ok(_workerService.CreateWorker(worker));
        }
        catch (Exception e)
        {
            string errorMessage = "There was an error creating the worker. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpGet]
    public ActionResult<List<Worker>> GetAllWorkers()
    {
        try
        {
            return Ok(_workerService.GetAllWorkers());
        }
        catch (Exception e)
        {
            string errorMessage = "There was an error retrieving the workers. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Worker> GetWorkerById(int id)
    {
        try
        {
            var result = _workerService.GetWorkerById(id);
            if (result == null || result.Id == 0)
            {
                return NotFound("There were no workers found with that id.");
            }
            return Ok(result);
        }
        catch (Exception e)
        {
            string errorMessage = "There was an error retrieving the worker. Additional details: ";
            errorMessage += e.InnerException != null ? e.InnerException.Message : e.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpPut("{id}")]
    public ActionResult<Worker> UpdateWorker(int id, Worker worker)
    {
        try
        {
            var result = _workerService.UpdateWorker(id, worker);
            if (result == null || result.Id == 0)
            {
                return NotFound("There were no workers found to update with that id.");
            }
            return Ok(result);
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
                return NotFound("There were no workers found to delete with that id.");
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
