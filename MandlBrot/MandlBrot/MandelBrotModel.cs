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
            double x = xReminder;
            double y = yReminder;

            int maxIterations = 18;
            int maxBetrag = 4;

            int yPixels = 200;
            int xPixels = 200;

            // 0.02
            double step = 0.013;

            byte[] colors = new byte[] { 0, 50,100,150,200,250};


            for (int i = 0; i < yPixels; i++)
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
            }

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
