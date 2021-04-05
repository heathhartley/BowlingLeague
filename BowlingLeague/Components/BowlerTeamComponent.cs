using BowlingLeague.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Components
{
    public class BowlerTeamComponent : ViewComponent 
    {
        private BowlingLeagueContext context;
        public BowlerTeamComponent(BowlingLeagueContext cxt)
        {
            context = cxt;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["team"];

            return View(context.Teams
                
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
