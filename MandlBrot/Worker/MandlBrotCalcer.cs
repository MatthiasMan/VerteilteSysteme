using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Worker
{
    public class MandlBrotCalcer
    {

        public List<(int, int, int)> CalcPart(int xMin, int xMax, int yMin, int yMax, int maxIterations, int maxBetrag, double xReminder, double yReminder, double step)
        {
            List<(int, int, int)> coordinatedValues = new List<(int, int, int)>();


            double x = xReminder;
            double y = yReminder;
            for (int i = yMin; i < yMax; i++)
            {
                for (int j = xMin; j < xMax; j++)
                {
                    int iterations = Julia(maxIterations, x, y, maxBetrag);


                    coordinatedValues.Add((j, i, iterations));
                    x = x + step;
                }

                x = xReminder;
                y = y - step;
            }
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

       
    }
}
