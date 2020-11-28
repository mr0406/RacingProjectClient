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
    public class TeamsController : Controller
    {
        private RacingApi _racingApi = new RacingApi();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync("/teams");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var teams = JsonConvert.DeserializeObject<List<Team>>(result);
                return View(teams);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/teams/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var team = JsonConvert.DeserializeObject<Team>(result);
                return View(team);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/teams/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var team = JsonConvert.DeserializeObject<Team>(result);
                return View(team);
            }

            return NotFound();
        }
    }
}
