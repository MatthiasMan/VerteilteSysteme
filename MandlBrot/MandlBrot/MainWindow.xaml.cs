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

            this.model.Calc();

            this.DataContext = this.model;



        }

        private async void Window_ContentRendered(object sender, EventArgs e)
        {

            var json = JsonConvert.SerializeObject( CalculationRequest()); //calculationrequest 2 probs höhe breite

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PostAsync("https://localhost:44351/ap/mandlbrot", data);

            var respstr = await resp.Content.ReadAsStringAsync();
           
            //var list = JsonConvert.DeserializeObject<List<Tripplees>>(respstr);   Triples: (X, Y, Iteration)


        }
    }
}
