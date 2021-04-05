using BowlingLeague.Models;
using BowlingLeague.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        public IActionResult Index(long? teamId, string team ,int pageNum = 0)
        {
            int pageSize = 5;
            //pass in the team Id so i cna filter the data
            return View(new IndexViewModels
            {
                Bowlers = (context.Bowlers
                .Where(t => t.TeamId == teamId || teamId == null)
                .OrderBy(t => t.BowlerFirstName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList()),

                teams = (context.Teams.Where(n => n.TeamId == teamId).ToList()),


                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    //if no meal has been selected then get the full count. otherwise only count the number from the meal type that has been selected 
                    TotalNumItems = (teamId == null ? context.Bowlers.Count() :
                        context.Bowlers.Where(t => t.TeamId == teamId).Count())
                },
                BowlerCategory = team



            }); 
                
                
                
            /*.FromSqlInterpolated($"Select * From Bowlers Where TeamId = {teamId} OR {teamId} IS NULL")
             .ToList());*/
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
