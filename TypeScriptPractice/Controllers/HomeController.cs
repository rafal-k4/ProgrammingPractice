using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace TypeScriptPractice.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var task = GetSomethingAsync();
            var result = task.Result; // this doesn't deadlock

            var task1 = GetSiteLenghtAsync(@"https://www.google.com/");
            var result2 = task1.Result; // this doesn't deadlock

            return View();
        }

        private async Task<string> GetSomethingAsync()
        {
            await Task.Delay(200);

            return "42";
        }

        private async Task<int> GetSiteLenghtAsync(string url)
        {
            using var client = new HttpClient();
            var jsonString = await client.GetStringAsync(url);
            return jsonString.Length;
        }
    }
}