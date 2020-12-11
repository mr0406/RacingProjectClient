using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RacingProjectClient.Client.ApiModels;
using RacingProjectClient.Client.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RacingProjectClient.Client.Controllers
{
    public class ChartsController : Controller
    {
        private RacingApi _racingApi = new RacingApi();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Teams()
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/charts/teams");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                List<ChartElement> chartElements = JsonConvert.DeserializeObject<List<ChartElement>>(result);

                ViewData["Chart_Title"] = "Number of drivers in F1 teams";

                return View("~/Views/Charts/Chart.cshtml", chartElements);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> RacingSeries()
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/charts/racingSeries");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                List<ChartElement> chartElements = JsonConvert.DeserializeObject<List<ChartElement>>(result);

                ViewData["Chart_Title"] = "Number of teams in racing series";

                return View("~/Views/Charts/Chart.cshtml", chartElements);
            }

            return NotFound();
        }

    }
}
