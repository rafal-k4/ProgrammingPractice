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

       

        public async IAsyncEnumerable<WeatherForecastModel> GetWeatherForecst()
        {
            var weatherResult = await httpClient.GetJsonAsync<IEnumerable<WeatherForecastModel>>("sample-data/weather.json");

            foreach (var item in weatherResult)
            {
                await Task.Delay(500);
                yield return item;
            }
        }
    }
}
