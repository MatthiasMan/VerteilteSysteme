using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
//using Newtonsoft.Json;
//using System.Text.Json;
//using System.Text.Json.Serialization;

namespace MandlBrot
{
    public class MandelBrotModel
    {
        private object Locker = new object();
        /// <summary>
        /// The calculated points of the function. 
        /// </summary>
        private List<Line> points;


        /// <summary>
        /// Occurs, when a property of the function changed.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public MandelBrotModel()
        {
           
            this.Points = new List<Line>();
        }

        public void Calc()
        {
            double xReminder = -2;
            double yReminder = 1.2;

            int maxIterations = 18;
            int maxBetrag = 4;

            int yPixels = 200;
            int xPixels = 200;

            // 0.02
            double step = 0.013;

            byte[] colors = new byte[] { 0, 50,100,150,200,250};


            /*for (int i = 0; i < yPixels; i++)
            {
                for (int j = 0; j < xPixels; j++)
                {
                    
                    int iterations = Julia(maxIterations, x, y, maxBetrag);

                    //int t = iterations * 50 % 250;
                    int t = colors[iterations % 6];

                    this.Points.Add(new Line(new List<Point> { new Point(j,i), new Point(j +1, i) }, new SolidColorBrush(Color.FromRgb( (byte)t,250, (byte)t))));
                    x = x + step;
                }

                x = xReminder;
                y = y - step;
            }*/


            List<Task<List<(int, int, int)>>> tasks = new List<Task<List<(int, int, int)>>>();  // = Task.Factory.StartNew(new Action(()=> { this.CalcPart(0, xPixels, 0, yPixels, maxIterations, maxBetrag, xReminder, yReminder, step) }));


            /*Message1 message1 = new Message1  //ein Beispiel  Message
            {
                coordinates = new List<(int, int)> { (1, 1), (2, 3), (5, 1) }   // wir schiken wirklich nur die koordinaten wo es schwarz sein soll, alles andre is weiß
            };

            Message2 message2 = new Message2  //ein Beispiel  Message
            {
                coordinates = new List<(int, int, bool)> { (1, 1, true), (2, 3, false), (5, 1, true) }  // wir schiken alle koordinaten und je nach schwarz oder weiß, true oder false
            };

            Message3 message3 = new Message3  //ein Beispiel  Message
            {
                coordinates = new List<(int, int, int)> { (1, 1, 6234), (2, 3, 12412), (5, 1, 897124) }  // wir schicken alle koordinaten und die jeweiligen iterationswerte
            };*/




            //NewtonSoft.Json.JavaScriptSerializer

            Args args = new Args      //ein Beispiel arg
            {
                xMin = 0,
                xMax = 100,
                yMin = 0,
                yMAx = 100,
                xZoomValue = -2.0,  //optional etwa wir gebens an oda wir machen uns ein fixes aus was alle nehmen müssen
                yZoomValue = 1.2,    //optional etwa wir gebens an oda wir machen uns ein fixes aus was alle nehmen müssen
            };

            List<(int, int)> coordinates1 = new List<(int, int)> { (1, 1), (2, 3), (5, 1) };   // wir schiken wirklich nur die koordinaten wo es schwarz sein soll, alles andre is weiß
            (int,int)[] coordinates11 = new [] { (1, 1), (2, 3), (5, 1) };

            List<(int, int, bool)> coordinates2 = new List<(int, int, bool)> { (1, 1, true), (2, 3, false), (5, 1, true) };  // wir schiken alle koordinaten und je nach schwarz oder weiß, true oder false
            (int, int, bool)[] coordinates22 = new[] { (1, 1, true), (2, 3, false), (5, 1, true) };

            List<(int, int, int)> coordinates3 = new List<(int, int, int)> { (1, 1, 6234), (2, 3, 12412), (5, 1, 897124) };  // wir schicken alle koordinaten und die jeweiligen iterationswerte
            (int, int, int)[] coordinates33 = new[] { (1, 1, 6234), (2, 3, 12412), (5, 1, 897124) };

            JsonSerializer jsonSerializer = new JsonSerializer();          //NewtonSoft.Json      NUGGET-Package

            // for Loop erstelle lauter args wo man xMin xMax yMin yMax ändert, egal ob zeilenweise, spaltenweise etc
            //{
            string gejastonStr = JsonConvert.SerializeObject(new List<(int, int, bool)> { (1, 1, true), (2, 3, false), (5, 1, true) });
            // etwa mit Rest schicken wir lauter POST requests und bekommen Messages zurück
            // oder TCP machen wir mit jedem Server eine Verbindung und haben einen listener der uns dann Messages zurück gibt   (vl muss ma no byte -> string und umgekehrt konvertiern)
            //}

            // warte bisd du alle Teile hast, oda async oda egal wers wie machn will

            //for Loop alle Teile
            //{
            List<(int, int, bool)> onePart = JsonConvert.DeserializeObject<List<(int, int, bool)>>(gejastonStr);
            // MYPAINTER.paint(onePart)      jeder wie ers machen will
            //}




            for (int k = 0; k < 2; k++)
            {
                int xMin = 0;
                int xMax = 100;
                double xxReminder = xReminder; 
                if(k == 1) { xMin = 101; xMax = 200; xxReminder = xReminder + 100 * step; }

                Task < List < (int, int, int) >> task = Task.Factory.StartNew(() => { return this.CalcPart(xMin, xMax, 0, yPixels, maxIterations, maxBetrag, xxReminder, yReminder, step); });
                tasks.Add(task);
            }


            Task.WaitAll(tasks.ToArray());

            List<(int, int, int)> coordinatedValues = new List<(int, int, int)>();

            for (int j = 0; j < tasks.Count; j++)
            {
                coordinatedValues.AddRange(tasks.ElementAt(j).Result);
            }

            //task.Result;


            //List<(int, int, int)> coordinatedValues = this.CalcPart(0, xPixels, 0, yPixels, maxIterations, maxBetrag, xReminder, yReminder, step);

            for (int i = 0; i < coordinatedValues.Count; i++)
            {
                int y = coordinatedValues.ElementAt(i).Item1;
                int x = coordinatedValues.ElementAt(i).Item2;
                int iterations = coordinatedValues.ElementAt(i).Item3;
                int t = colors[iterations % 6];
                this.Points.Add(new Line(new List<Point> { new Point(y, x), new Point(y + 1, x) }, new SolidColorBrush(Color.FromRgb((byte)t, 250, (byte)t))));
            }

        }

        public List<(int, int, int)> CalcPart(int xMin, int xMax, int yMin, int yMax, int maxIterations, int maxBetrag, double xReminder, double yReminder, double step)
        {
            List<(int, int, int)> coordinatedValues = new List<(int, int, int)>();


            double x = xReminder;
            double y = yReminder;
            //lock (Locker)
            //{
                for (int i = yMin; i < yMax; i++)
                {
                    for (int j = xMin; j < xMax; j++)
                    {

                        int iterations = Julia(maxIterations, x, y, maxBetrag);

                        //int t = iterations * 50 % 250;

                        coordinatedValues.Add((j, i, iterations));
                        //this.Points.Add(new Line(new List<Point> { new Point(j, i), new Point(j + 1, i) }, new SolidColorBrush(Color.FromRgb((byte)t, 250, (byte)t))));
                        x = x + step;
                    }

                    x = xReminder;
                    y = y - step;
                }
           // }
            

            return coordinatedValues;
        }

        private int Julia(int maxIter, double real, double imag, int max_betrag)
        {
            double curReal = real;
            double currImag = imag;
            double value = curReal * curReal + currImag * currImag;
            int remainIter = maxIter;

            while (value <= max_betrag && remainIter > 0)
            {
                remainIter = remainIter - 1;

                double z_re2 = curReal * curReal;
                double z_im2 = currImag * currImag;

                currImag = 2 * curReal * currImag + imag;
                curReal = z_re2 - z_im2 + real;

                value = z_re2 + z_im2;
            }

            return (maxIter - remainIter);
        }


        public List<Line> Points
        {
            get
            {
                return this.points;
            }

            set
            {
                this.points = value ?? throw new ArgumentNullException();
                this.FireOnPropertyChanged();
            }
        }


        /// <summary>
        /// Fires the on property changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void FireOnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

       /* private class Message1
        {
            public List<(int, int)> coordinates { get; set; }
        }


        private class Message2
        {
            public List<(int, int, bool)> coordinates { get; set; }
        }

        private class Message3
        {
            public List<(int, int, int)> coordinates { get; set; }
        }*/

        private class Args
        {
            public int xMin { get; set; }
            public int xMax { get; set; }
            public int  yMin{ get; set; }
             public int yMAx { get; set; }
            public  double xZoomValue { get; set; } //optional etwa wir gebens an oda wir machen uns ein fixes aus
            public double  yZoomValue { get; set; }    //optional etwa wir gebens an oda wir machen uns ein fixes aus  { get; set; }
        }
    }
}
