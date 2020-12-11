using Microsoft.AspNetCore.Mvc;
using RacingProjectClient.Client.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RacingProjectClient.Client.Controllers
{
    public class ChartsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Teams()
        {
            List<ChartElement> chartElements = new List<ChartElement>();
            chartElements.Add(new ChartElement("Ferrari", 2));
            chartElements.Add(new ChartElement("Mercedes", 3));
            chartElements.Add(new ChartElement("RedBull", 2));

            ViewData["Chart_Title"] = "Number of drivers in F1 teams";
            return View("~/Views/Charts/Chart.cshtml", chartElements);
        }
    }
}
