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
    }
}
