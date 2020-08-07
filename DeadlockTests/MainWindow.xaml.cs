using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
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
            //var task = GetSomethingAsync();
            //var result = task.Result; // this does deadlock

            var task1 = GetSiteLengthAsync(@"https://www.google.com/");
            var result2 = task1.Result; // this does deadlock
        }

        private async Task<string> GetSomethingAsync()
        {
            await Task.Delay(200).ConfigureAwait(false);

            return "42";
        }

        private async Task<int> GetSiteLengthAsync(string url)
        {
            //using var client = new HttpClient();
            //var content = await client.GetStringAsync(url);
            //return content.Length;
            var result = await GetSiteLengthAsyncNested(url); // this does deadlock too !!
            return result;
        }

        private async Task<int> GetSiteLengthAsyncNested(string url)
        {
            using var client = new HttpClient();
            var content = await client.GetStringAsync(url).ConfigureAwait(false);
            return content.Length;
        }
    }
}
