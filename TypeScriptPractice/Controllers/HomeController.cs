using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace TypeScriptPractice.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var task = GetSomethingAsync();
            var result = task.Result; // this doesn't deadlock

            var threadId = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thead ID in TOP-LEVEL Method: {threadId}");

            var task1 = GetSiteLengthAsync(@"https://www.google.com/");
            var result2 = task1.Result; // this doesn't deadlock

            return View();
        }

        private async Task<string> GetSomethingAsync()
        {
            await Task.Delay(200);

            return "42";
        }

        private async Task<int> GetSiteLengthAsync(string url)
        {
            using var client = new HttpClient();

            var threadId = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thead ID in LOW-LEVEL Method Before await: {threadId}");

            var jsonString = await client.GetStringAsync(url).ConfigureAwait(false);

            var threadId2 = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thead ID in LOW-LEVEL Method After await: {threadId}");

            return jsonString.Length;
        }
    }
}