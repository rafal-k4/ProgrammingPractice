using BlazorPractice.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorPractice
{
    public class WeatherForecast : ComponentBase
    {
        [Inject]
        private HttpClient httpClient { get; set; }

       

        public async Task<IEnumerable<WeatherForecastModel>> GetWeatherForecst()
        {
            return await httpClient.GetJsonAsync<IEnumerable<WeatherForecastModel>>("sample-data/weather.json");
        }
    }
}
