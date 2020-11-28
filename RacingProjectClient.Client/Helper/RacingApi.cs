using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RacingProjectClient.Client.Helper
{
    public class RacingApi
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:52746");
            return Client;
        }
    }
}
