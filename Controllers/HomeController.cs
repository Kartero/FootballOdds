using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FootballOdds.Models;
using FootballOdds.Models.Helpers;
using Microsoft.AspNetCore.Hosting;
using FootballOdds.Models.RawData;

namespace FootballOdds.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _env;

        private readonly FootballOddsContext _context;

        public HomeController(IHostingEnvironment env, FootballOddsContext context)
        {
            _env = env;
            _context = context;
        }

        public IActionResult Index()
        {
            var fixtures = new Fixtures(_env, _context);

            //CsvReader csvReader = new CsvReader(_env);
            //string[] files = csvReader.ReadAll("source");
            return View(fixtures);
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
