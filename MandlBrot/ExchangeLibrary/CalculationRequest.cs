using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeLibrary
{
    public class CalculationRequest
    {
        /*private int height;
        private int width;*/

        public CalculationRequest()
        {
        }

        public CalculationRequest(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public int Height
        {
            get; set;
        }

        public int Width
        {
            get;set;
        }
    }
}
