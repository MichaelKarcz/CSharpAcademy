using Microsoft.AspNetCore.Mvc;
using ShiftLogger.API.Models;
using ShiftLogger.API.Services;

namespace ShiftLogger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly WorkerService _workerService;

        public WorkerController(WorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpPost]
        public ActionResult<Worker> CreateWorker(Worker worker)
        {
            return Ok(_workerService.CreateWorker(worker));
        }

        [HttpGet]
        public ActionResult<List<Worker>> GetAllWorkers()
        {
            return Ok(_workerService.GetAllWorkers());
        }

        [HttpGet("{id}")]
        public ActionResult<Worker> GetWorkerById(int id)
        {
            var result = _workerService.GetWorkerById(id);

            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public ActionResult<Worker> UpdateWorker(int id, Worker worker)
        {
            var result = _workerService.UpdateWorker(id, worker);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteWorker(int id)
        {
            var result = _workerService.DeleteWorker(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
