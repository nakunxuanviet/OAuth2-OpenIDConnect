using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using NaKun.WebAPI.Client.Models;
using NaKun.WebAPI.Client.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NaKun.WebAPI.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IIdentityServerService _identityServerService;
        public WeatherController(IIdentityServerService identityServer4Service)
        {
            _identityServerService = identityServer4Service;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var OAuth2Token = await _identityServerService.GetToken("weatherApi.read");
            using (var client = new HttpClient())
            {
                client.SetBearerToken(OAuth2Token.AccessToken);
                var result = await client.GetAsync("https://localhost:5002/weatherforecast");    // 5002 là server api
                if (result.IsSuccessStatusCode)
                {
                    var model = await result.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<WeatherForecast>>(model);
                }
                else
                {
                    throw new Exception("Some Error while fetching Data");
                }
            }
        }
    }
}
