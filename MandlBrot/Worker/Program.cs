using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using NetMQ;
using NetMQ.Sockets;

namespace Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("====== WORKER ======");
            using (var receiver = new PullSocket(">tcp://localhost:80"))
            using (var sender = new PushSocket(">tcp://localhost:400"))
            {
                //process tasks forever
                while (true)
                {
                    string workload = receiver.ReceiveFrameString();
                    Console.WriteLine("got");
                    string[] values = workload.Split(',');

                    double xReminder = -2 + ((double)Int32.Parse(values[0]) * 0.013);

                    MandlBrotCalcer calcer = new MandlBrotCalcer();
                    
                    List<(int,int,int)> list = calcer.CalcPart(Int32.Parse(values[0]), Int32.Parse(values[1]), 0, Int32.Parse(values[2]),18,4, xReminder, 1.2,0.013);

                    BinaryFormatter binaryFormatter = new BinaryFormatter();

                    byte[] listBytes;

                    using (var memoryStream = new MemoryStream())
                    {
                        binaryFormatter.Serialize(memoryStream, list);
                        listBytes = memoryStream.ToArray();
                    }

                    sender.SendFrame(listBytes);
                    
                    Console.WriteLine("Sending to Sink");
                    sender.SendFrame(string.Empty);
                }
            }

        }


    }
}
