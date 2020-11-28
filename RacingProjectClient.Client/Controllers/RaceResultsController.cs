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
    public class RaceResultsController : Controller
    {
        private RacingApi _racingApi = new RacingApi();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync("/raceresults");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var raceResults = JsonConvert.DeserializeObject<List<RaceResult>>(result);
                return View(raceResults);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/raceresults/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var raceResult = JsonConvert.DeserializeObject<RaceResult>(result);
                return View(raceResult);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/raceresults/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var raceResult = JsonConvert.DeserializeObject<RaceResult>(result);
                return View(raceResult);
            }

            return NotFound();
        }
    }
}
