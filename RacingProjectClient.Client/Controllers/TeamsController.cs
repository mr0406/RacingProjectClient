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
    public class TeamsController : Controller
    {
        private RacingApi _racingApi = new RacingApi();

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            int pageNum = page ?? 1;

            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/teams?page={pageNum}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<IndexPackage<Team>>(result);
                var teams = data.Entities;
                ViewData["Actual_page"] = data.ActualPage;
                ViewData["Is_prev_disabled"] = !data.HasPreviousPage;
                ViewData["Is_next_disabled"] = !data.HasNextPage;
                return View(teams);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Team team)
        {
            HttpClient client = _racingApi.Initial();
            string json = JsonConvert.SerializeObject(team);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/teams", httpContent);

            return RedirectToAction("Index");
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

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Team newTeam)
        {
            HttpClient client = _racingApi.Initial();
            string json = JsonConvert.SerializeObject(newTeam);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"/teams/{id}", httpContent);

            return RedirectToAction("Index");
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

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.DeleteAsync($"/teams/{id}");

            return RedirectToAction("Index");
        }
    }
}
