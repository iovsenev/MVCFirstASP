using Microsoft.AspNetCore.Mvc;
using MVCFirstASP.Models;
using MVCFirstASP.Models.DB;
using MVCFirstASP.Models.DB.Repositories;
using System.Diagnostics;

namespace MVCFirstASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
            _logger = logger;
        }

        public async Task <IActionResult> Index()
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Andrey",
                LastName = "Andreev",
                JoinDate = DateTime.Now
            };

            await _blogRepository.AddUser(user);

            Console.WriteLine($"User with id {user.Id}, " +
                $"named {user.FirstName} was successfully added on {user.JoinDate}");

            return View();
        }

        public async Task<IActionResult> Authors()
        {
            var authors =await _blogRepository.GetUsers();

            Console.WriteLine("See all blog authors");
            foreach (var user in authors)
            {
                Console.WriteLine($"Author name: {user.FirstName}, Joined: {user.JoinDate}");
            }

            return View(authors);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
