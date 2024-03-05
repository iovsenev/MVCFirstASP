using Microsoft.AspNetCore.Mvc;
using MVCFirstASP.Models.DB;
using MVCFirstASP.Models.DB.Repositories;

namespace MVCFirstASP.Controllers
{
    public class UsersController: Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> loger, IBlogRepository blogRepository)
        {
            _logger = loger;
            _blogRepository = blogRepository;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _blogRepository.GetUsers();

            return View(authors);
        }

        public async Task<IActionResult> Register()
        {
            
            return View();
        }
    }
}
