using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace Sink
{
    public class Program                            // OBSELETE
    {
        public static void Main(string[] args)
        {
            // Task Sink
            // Bindd PULL socket to tcp://localhost:5558
            // Collects results from workers via that socket
            Console.WriteLine("====== SINK ======");


            //socket to receive messages on
           /* using (var sendTo = new PushSocket(">tcp://localhost:82"))
            using (var receiver = new PullSocket("@tcp://localhost:400"))
            {
                //wait for start of batch (see Ventilator.csproj Program.cs)
                var watch = Stopwatch.StartNew();

                for (int task = 0; task < 400; task += 10)
                {
                    var workerDone = receiver.ReceiveFrameBytes();
                    Console.WriteLine("got info " + task);

                    sendTo.SendFrame(workerDone);

                    Console.WriteLine("sended info " + task);

                    /*List<(int, int, int)> list = null;
                    BinaryFormatter binaryFormatter = new BinaryFormatter();

                    using (var memoryStream = new MemoryStream(workerDone))
                    {
                        list = (List<(int, int, int)>)binaryFormatter.Deserialize(memoryStream);
                    }*/


                    /*for (int i = 0; i < list.Count; i++)
                    {
                        var curr = list.ElementAt(i);
                        //Console.WriteLine("sended: " + curr.Item1.ToString() + " " + curr.Item2.ToString() + " " + curr.Item3.ToString());
                    }*/

                /*}*/
                //watch.Stop();
                //Calculate and report duration of batch
                //Console.WriteLine();
                
            //}
        Console.WriteLine("Close");
                Console.ReadLine();
            
        }
    }
}