using System;
using System.Threading;
using NetMQ;
using NetMQ.Sockets;

namespace Ventilator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("====== VENTILATOR ======");
            

            Console.WriteLine("Press enter when all is set up");
            Console.ReadLine();

            using (var sender = new PushSocket("@tcp://*:80"))
            {
                
                Console.WriteLine("Sending tasks to workers");

                int step = 10;
                int height = 200;  //400    hier kann height geändert werden
                int width = 200;   //       hier kann width geändert werden
                for (int start = 0; start < height; start += 10)
                {
                    Thread.Sleep(100);
                    sender.SendFrame(start + "," + step + "," + width);
                    Console.WriteLine("asked for " + start + " to " + step);
                    step = step + 10;
                }

                Console.WriteLine("Press any key to quit");
                Console.ReadLine();
            }
        }
    }
}