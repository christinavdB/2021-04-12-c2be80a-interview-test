using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Aris.ServerTest.Filters;

namespace Aris.ServerTest.Controllers
{
    
    [ReturnUrlFromRequest]
    public class HomeController : BaseController
    {
        private readonly Services.IKoreApiGameService _gameService;

        public HomeController(Services.IKoreApiGameService gameService) 
        {
            _gameService = gameService;
        }

        public async Task<IActionResult> Index(string catFilter, string returnUrl)
        {
            var viewModel = new ViewModels.GamesListViewModel();
            var games = await _gameService.GetGamesAsync(GetAuthToken(), returnUrl);

            if (string.IsNullOrEmpty(catFilter))
            {
                viewModel.Games = games.OrderBy(a => a.Category).ThenBy(a => a.Platform).ThenBy(a => a.Name);
            }
            else
            {
                viewModel.Games = games.Where(a => a.Category == catFilter).OrderBy(a => a.Category).ThenBy(a => a.Platform).ThenBy(a => a.Name);
            }

            // Task 4 - create a List of Categories for the dropdown to display
            viewModel.Categories = games.OrderBy(a => a.Category).Select(a => a.Category).Distinct();

            return View(viewModel);
        }

        public async Task<IActionResult> Details(string game, string returnUrl)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(game);
            var gameUrl = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            var gameJson = await _gameService.GetGameAsync(GetAuthToken(), gameUrl, returnUrl);

            return Json(gameJson);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
