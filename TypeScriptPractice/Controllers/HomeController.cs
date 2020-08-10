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
        public async Task<IActionResult> Index()
        {
            var threadId1 = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thread ID in TOP-LEVEL Before: {threadId1}");

            var task1 = await GetSiteLengthAsync(@"https://www.google.com/");
            //var result2 = task1.Result; // this doesn't deadlock

            var threadId2 = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thread ID in TOP-LEVEL After: {threadId2}");

            // Result when whole action is async and await is used in top level:
            // Thread ID in TOP - LEVEL Before: 4
            // Thread ID in LOW - LEVEL Method Before await: 4
            // Thread ID in LOW - LEVEL Method After await: 5
            // Thread ID in TOP - LEVEL After: 5

            // Result when .Result is used:
            // Thread ID in TOP - LEVEL Before: 4
            // Thread ID in LOW - LEVEL Method Before await: 4
            // Thread ID in LOW - LEVEL Method After await: 5
            // Thread ID in TOP - LEVEL After: 4

            // so we can se, that calling thread is blocked and waiting for result (Before and After are the same thread ID - 4)


            return View();
        }

        private async Task<int> GetSiteLengthAsync(string url)
        {
            using var client = new HttpClient();

            var threadId1 = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thread ID in LOW-LEVEL Method Before await: {threadId1}");

            var jsonString = await client.GetStringAsync(url);
            // ConfigureAwait(false) does nothing, because .net core application is using thread pool context, not a synchronization context
            // so when calling thread is blocked by .Result then next free thread continue in this method after await completes
            //
            // Thread ID in TOP-LEVEL Method: 13
            // Thread ID in LOW - LEVEL Method Before await: 13
            // Thread ID in LOW - LEVEL Method After await: 6

            var threadId2 = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thread ID in LOW-LEVEL Method After await: {threadId2}");

            return jsonString.Length;
        }
    }
}