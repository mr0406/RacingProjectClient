using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RacingProjectClient.Client.ApiModels;
using RacingProjectClient.Client.Helper;
using RacingProjectClient.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RacingProjectClient.Client.Controllers
{
    public class DriversController : Controller
    {
        private RacingApi _racingApi = new RacingApi();

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            int pageNum = page ?? 1;

            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/drivers?page={pageNum}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var drivers = JsonConvert.DeserializeObject<List<Driver>>(result);
                return View(drivers);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Driver driver)
        {
            HttpClient client = _racingApi.Initial();
            string json = JsonConvert.SerializeObject(driver);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/drivers", httpContent);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/drivers/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var driver = JsonConvert.DeserializeObject<Driver>(result);
                return View(driver);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Driver newDriver)
        {
            HttpClient client = _racingApi.Initial();
            string json = JsonConvert.SerializeObject(newDriver);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"/drivers/{id}", httpContent);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync($"/drivers/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var driver = JsonConvert.DeserializeObject<Driver>(result);
                return View(driver);
            }

            return NotFound();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.DeleteAsync($"/drivers/{id}");

            return RedirectToAction("Index");
        }
    }
}
