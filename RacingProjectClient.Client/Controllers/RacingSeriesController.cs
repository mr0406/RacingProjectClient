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
    public class RacingSeriesController : Controller
    {
        private RacingApi _racingApi = new RacingApi();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync("/racingseries");

            if(response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var racingSeries = JsonConvert.DeserializeObject<List<RacingSerie>>(result);
                return View(racingSeries);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/racingseries/{id}");

            if(response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var racingSerie = JsonConvert.DeserializeObject<RacingSerie>(result);
                return View(racingSerie);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/racingseries/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var racingSerie = JsonConvert.DeserializeObject<RacingSerie>(result);
                return View(racingSerie);
            }

            return NotFound();
        }
    }
}
