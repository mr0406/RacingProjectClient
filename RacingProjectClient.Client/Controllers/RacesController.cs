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
    public class RacesController : Controller
    {
        private RacingApi _racingApi = new RacingApi();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync("/races");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var races = JsonConvert.DeserializeObject<List<Race>>(result);
                return View(races);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/races/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var race = JsonConvert.DeserializeObject<Race>(result);
                return View(race);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/races/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var race = JsonConvert.DeserializeObject<Race>(result);
                return View(race);
            }

            return NotFound();
        }
    }
}
