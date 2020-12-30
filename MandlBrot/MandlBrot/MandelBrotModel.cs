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
        public event PropertyChangedEventHandler PropertyChanged;

        public MandelBrotModel()
        {
           
            this.Points = new List<Line>();
        }


        public void Add(List<(int,int,int)> coordinatedvalues)
        {
            byte[] colors = new byte[] { 0, 50, 100, 150, 200, 250 };


            for (int i = 0; i < coordinatedvalues.Count; i++)
            {
                (int, int, int) curr = coordinatedvalues.ElementAt(i);

                int t = colors[curr.Item3 % 6];
                this.Points.Add(new Line(new List<Point> { new Point(curr.Item2, curr.Item1), new Point(curr.Item2 + 1, curr.Item1) }, new SolidColorBrush(Color.FromRgb((byte)t, 250, (byte)t))));
                
            }

            this.FireOnPropertyChanged("Points");
            
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
