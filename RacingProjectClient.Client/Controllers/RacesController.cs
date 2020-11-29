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
    public class RacesController : Controller
    {
        private RacingApi _racingApi = new RacingApi();

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            int pageNum = page ?? 1;

            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/races?page={pageNum}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var races = JsonConvert.DeserializeObject<List<Race>>(result);
                return View(races);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Race race)
        {
            HttpClient client = _racingApi.Initial();
            string json = JsonConvert.SerializeObject(race);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/races", httpContent);

            return RedirectToAction("Index");
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

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Race newRace)
        {
            HttpClient client = _racingApi.Initial();
            string json = JsonConvert.SerializeObject(newRace);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"/races/{id}", httpContent);

            return RedirectToAction("Index");
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

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.DeleteAsync($"/races/{id}");

            return RedirectToAction("Index");
        }
    }
}
