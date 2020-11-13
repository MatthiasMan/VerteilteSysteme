using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MandlBrot.Converter
{
    public class GeometryConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return value;
            }

            var line = (MandlBrot.Line)value;

            var points = line.Point;
            var i = 0;
            var newPath = new StreamGeometry();

            using (var context = newPath.Open())
            {
                var begun = false;

                for (i = 0; i < points.Count; i++)
                {
                    var current = points[i];

                    if (!begun)
                    {
                        begun = true;
                        context.BeginFigure(current, true, false);
                    }
                    else
                    {
                        context.ArcTo(current, new Size(1, 1), 1, false, SweepDirection.Counterclockwise, true, true);
                    }
                }
            }

            newPath.Freeze();
            return newPath.GetFlattenedPathGeometry();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
