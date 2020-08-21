using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeadlockTests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var threadId1 = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thread ID in TOP-LEVEL Before: {threadId1}");

            //var task1 = GetSiteLengthAsync(@"https://www.google.com/");
            //var result1 = task1.Result; // this does deadlock

            var task2 = TestForTask_FromResult();
            var result2 = task2.Result;
            Debug.WriteLine($"Result from Task.FromResult: {result2}");

            var threadId2 = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thread ID in TOP-LEVEL After: {threadId2}");

            // when used with async-await:
            // Thread ID in TOP - LEVEL Before: 1
            // Thread ID in FIRST - LEVEL Method Before await: 1
            // Thread ID in SECOND - LEVEL Method Before await: 1
            // Thread ID in SECOND - LEVEL Method After await: 12
            // Thread ID in FIRST - LEVEL Method After await: 1
            // Thread ID in TOP - LEVEL After: 1

            // when used with .Result (ConfigureAwait(false) needed even in FIRST-LEVEL Method!)
            // ConfigureAwait(false) in FIRST-LEVEL and in SECOND-LEVEL Method
            // Thread ID in TOP - LEVEL Before: 1
            // Thread ID in FIRST - LEVEL Method Before await: 1
            // Thread ID in SECOND - LEVEL Method Before await: 1
            // Thread ID in SECOND - LEVEL Method After await: 13
            // Thread ID in FIRST - LEVEL Method After await: 13
            // Thread ID in TOP - LEVEL After: 1

            // when used with .Result (ConfigureAwait(false) needed even in FIRST-LEVEL Method!)
            // ConfigureAwait(false) in First-Level OR Second-Level CAUSE A DEADLOCK!!
            // Thread ID in TOP - LEVEL Before: 1
            // Thread ID in FIRST - LEVEL Method Before await: 1
            // Thread ID in SECOND - LEVEL Method Before await: 1
            // DEADLOCK!

            // Task.FromResult doesn't DEADLOCK !!! Task.FromResult is running synchronously (Task.FromResult return completed Task)
            // 
            // Thread ID in TOP - LEVEL Before: 1
            // Thread ID in FIRST - LEVEL Method Before await: 1
            // Thread ID in SECOND - LEVEL Method Before await: 1
            // Thread ID in SECOND - LEVEL Method After await: 1
            // Thread ID in FIRST - LEVEL Method After await: 1
            // Result from Task.FromResult: Task from result TEST
            // Thread ID in TOP - LEVEL After: 1

        }

        private async Task<string> TestForTask_FromResult()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thread ID in FIRST-LEVEL Method Before await: {threadId}");

            Debug.WriteLine("Before method");
            var nestedResult = NestedTask_FromResult();
            Debug.WriteLine("After method, before await");
            await nestedResult;
            Debug.WriteLine("After await");

            var threadId2 = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thread ID in FIRST-LEVEL Method After await: {threadId2}");
            
            return nestedResult;
        }

        private Task<string> NestedTask_FromResult()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thread ID in SECOND-LEVEL Method Before await: {threadId}");

            var result = "Task from result TEST";
            var task = Task.FromResult(result);

            var threadId2 = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thread ID in SECOND-LEVEL Method After await: {threadId2}");


            return task;
        }

        private async Task<int> GetSiteLengthAsync(string url)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thread ID in FIRST-LEVEL Method Before await: {threadId}");

            var result = await GetSiteLengthAsyncNested(url).ConfigureAwait(false); // this does deadlock too without ConfigureAwait(false) !! 

            var threadId2 = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thread ID in FIRST-LEVEL Method After await: {threadId2}");

            return result;
        }

        private async Task<int> GetSiteLengthAsyncNested(string url)
        {
            using var client = new HttpClient();

            var threadId = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thread ID in SECOND-LEVEL Method Before await: {threadId}");

            var content = await client.GetStringAsync(url);

            var threadId2 = Thread.CurrentThread.ManagedThreadId;
            Debug.WriteLine($"Thread ID in SECOND-LEVEL Method After await: {threadId2}");

            return content.Length;
        }
    }
}
