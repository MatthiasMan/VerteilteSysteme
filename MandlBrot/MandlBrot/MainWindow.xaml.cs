using ExchangeLibrary;
using MandlBrot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace MandlBrot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MandelBrotModel model;

        private static readonly HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();

            this.model = new MandelBrotModel(/*this.PlotArea*/);
            //var c = () => this.Window_ContentRendered(3,new EventArgs());

            var task = MyAsyncMethod(this.model);
            task.Wait(5000);
            //task.Wait();

            this.DataContext = this.model;

            
        }

        private async Task<int> MyAsyncMethod(MandelBrotModel mod)
        {
            var json = JsonConvert.SerializeObject(new CalculationRequest() { Height = 200, Width = 200 }); //calculationrequest 2 probs höhe breite

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PostAsync("https://localhost:44364/api/calc", data);

            var respstr = await resp.Content.ReadAsStringAsync();

            var list = JsonConvert.DeserializeObject<List<(int, int, int)>>(respstr);

            mod.Add(list);

            return 4;
        }

        /*private async void Window_ContentRendered(object sender, EventArgs e)
        {
            var json = JsonConvert.SerializeObject(new CalculationRequest() { Height = 200, Width = 200 }); //calculationrequest 2 probs höhe breite

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PostAsync("https://localhost:44364/api/calc", data);
            //var resp = await client.GetAsync("https://localhost:44364/api/gett");
            //api/gett

            var respstr = await resp.Content.ReadAsStringAsync();

            var list = JsonConvert.DeserializeObject<List<(int,int,int)>>(respstr);

            tt.model.Add(list);

            //tt.DataContext = tt.model;
        }*/
    }
}
