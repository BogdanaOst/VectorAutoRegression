using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VectorAutoregression.Models
{
    public static class InputModels
    {
        public static readonly double[] BMI = new double[30]
              {
25,
25,
25,
25.1,
25.1,
25.2,
25.2,
25.3,
25.3,
25.4,
25.5,
25.5,
25.6,
25.6,
25.7,
25.8,
25.8,
25.9,
25.9,
26,
26,
26.1,
26.1,
26.2,
26.2,
26.3,
26.3,
26.3,
26.4,
26.4,
              };

        public static readonly double[] Chol = new double[30] { 5.058,
5.066,
5.08,
5.088,
5.096,
5.108,
5.12,
5.136,
5.148,
5.156,
5.174,
5.196,
5.218,
5.242,
5.266,
5.288,
5.32,
5.344,
5.362,
5.398,
5.404,
5.418,
5.434,
5.456,
5.478,
5.486,
5.504,
5.52,
5.544,
5.556};      
    }
}