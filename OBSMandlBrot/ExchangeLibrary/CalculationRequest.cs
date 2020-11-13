using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeLibrary
{
    public class CalculationRequest
    {
        public readonly int Height;
        public readonly int Width;

        public CalculationRequest(int height, int width)
        {
            Height = height;
            Width = width;
        }
    }
}
