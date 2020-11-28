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
    [Route("[controller]")]
    public class RacingSeriesController : Controller
    {
        private RacingApi _racingApi = new RacingApi();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RacingSerie> racingSeries = new List<RacingSerie>();

            HttpClient client = _racingApi.Initial();
            HttpResponseMessage response = await client.GetAsync("/racingseries");

            if(response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                racingSeries = JsonConvert.DeserializeObject<List<RacingSerie>>(result);
            }

            return View(racingSeries);
        }
    }
}
