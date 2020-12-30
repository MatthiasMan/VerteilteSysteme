using ExchangeLibrary;
using MandlBrot;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
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

            this.model = new MandelBrotModel();
            this.Button_Click(this,null);

            this.DataContext = this.model;

            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            using (var receiver = new PullSocket("@tcp://localhost:400"))
            {

                var watch = Stopwatch.StartNew();

                for (int i = 0; i < 40; i++)
                {
                    var workerDone = receiver.ReceiveFrameBytes();
                    List<(int, int, int)> list = null;
                    BinaryFormatter binaryFormatter = new BinaryFormatter();

                    using (var memoryStream = new MemoryStream(workerDone))
                    {
                        if (memoryStream.Length != 0)
                        {
                            list = (List<(int, int, int)>)binaryFormatter.Deserialize(memoryStream);
                            this.model.Add(list);
                        }
                        
                    }

                }
                watch.Stop();
            }

        }

    }
}
