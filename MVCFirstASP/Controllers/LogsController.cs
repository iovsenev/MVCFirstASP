using Microsoft.AspNetCore.Mvc;
using MVCFirstASP.Models.DB.Repositories;

namespace MVCFirstASP.Controllers
{
    public class LogsController : Controller
    {
        private readonly IRequestRepository _repository;
        private readonly ILogger<LogsController> _logger;

        public LogsController(IRequestRepository repository, ILogger<LogsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var logs = await _repository.GetAll();

            return View(logs);
        }
    }
}
