using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MandlBrott
{
    public class Line
    {
        public List<Point> Point;
        public SolidColorBrush solidColorBrush;

        public Line(List<Point> point, SolidColorBrush solidBrush)
        {
            Point = point;
            SolidColorBrush = solidBrush;
        }

        public SolidColorBrush SolidColorBrush
        {
            get
            {
                return this.solidColorBrush;
            }
            set
            {
                this.solidColorBrush = value;
            }
        }
    }
}
