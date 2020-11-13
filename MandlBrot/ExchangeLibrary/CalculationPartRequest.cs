﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeLibrary
{
    public class CalculationPartRequest
    {
       /* public readonly int XMin;
        public readonly int XMax;
        public readonly int YMin;
        public readonly int YMax;
        public readonly double  XCurr;
        public readonly double XStep;
        public readonly double YCurr;
        public readonly double YStep;
        public readonly int MaxIterations;
        public readonly int MaxBetrag;*/

        public CalculationPartRequest(int xMin, int xMax, int yMin, int yMax, double xCurr, double xStep, double yCurr, double yStep, int maxIterations,int maxBetrag)
        {
            XMin = xMin;
            XMax = xMax;
            YMin = yMin;
            YMax = yMax;
            XCurr = xCurr;
            XStep = xStep;
            YCurr = yCurr;
            YStep = yStep;
            MaxBetrag = maxBetrag;
            //MaxIterations = MaxIterations;
            MaxIterations = maxIterations;
        }

        public int XMin { get; set; }
        public int XMax { get; set; }
        public int YMin { get; set; }
        public int YMax { get; set; }
        public int MaxBetrag { get; set; }
        public int MaxIterations { get; set; }
        public double XCurr { get; set; }
    public double XStep { get; set; }
public double YCurr { get; set; }
    public double YStep { get; set; }

    }
}
