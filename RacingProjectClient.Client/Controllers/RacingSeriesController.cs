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
    public class RacingSeriesController : Controller
    {
        private RacingApi _racingApi = new RacingApi();

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            int pageNum = page ?? 1;

            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/racingseries?page={pageNum}");

            if(response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var racingSeries = JsonConvert.DeserializeObject<List<RacingSerie>>(result);
                return View(racingSeries);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RacingSerie racingSerie)
        {
            HttpClient client = _racingApi.Initial();
            string json = JsonConvert.SerializeObject(racingSerie);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/racingseries", httpContent);

            return RedirectToAction("Index");
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

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RacingSerie newRacingSerie)
        {
            HttpClient client = _racingApi.Initial();
            string json = JsonConvert.SerializeObject(newRacingSerie);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"/racingseries/{id}", httpContent);

            return RedirectToAction("Index");
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

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.DeleteAsync($"/racingseries/{id}");

            return RedirectToAction("Index");
        }
    }
}
