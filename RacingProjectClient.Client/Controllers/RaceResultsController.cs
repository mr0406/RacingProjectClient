using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RacingProjectClient.Client.ApiModels;
using RacingProjectClient.Client.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RacingProjectClient.Client.Controllers
{
    public class RaceResultsController : Controller
    {
        private RacingApi _racingApi = new RacingApi();

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            int pageNum = page ?? 1;

            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/raceresults?page={pageNum}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<IndexPackage<RaceResult>>(result);
                var raceResults = data.Entities;
                ViewData["Actual_page"] = data.ActualPage;
                ViewData["Is_prev_disabled"] = !data.HasPreviousPage;
                ViewData["Is_next_disabled"] = !data.HasNextPage;
                return View(raceResults);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RaceResult raceResult)
        {
            HttpClient client = _racingApi.Initial();
            string json = JsonConvert.SerializeObject(raceResult);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/raceresults", httpContent);

            return RedirectToAction("Index");
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

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RaceResult newRaceResult)
        {
            HttpClient client = _racingApi.Initial();
            string json = JsonConvert.SerializeObject(newRaceResult);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"/raceresults/{id}", httpContent);

            return RedirectToAction("Index");
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

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.DeleteAsync($"/raceresults/{id}");

            return RedirectToAction("Index");
        }
    }
}
