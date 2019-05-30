using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VectorAutoregression.Services;

namespace VectorAutoregression.Models
{
    public class VarDataModel
    {
        public List<DataPoint> InputX1 { get; set; }
        public List<DataPoint> InputX2 { get; set; }

        public List<DataPoint> OutputX1 { get; set; }
        public List<DataPoint> OutputX2 { get; set; }

        public double[,] CoefMatrix;

        public VarDataModel(List<double> X1, List<double> X2)
        {
            var result = ComputeService.PredictedVar(X1, X2, out CoefMatrix);
            InputX1 = X1.ToDataPoint();
            InputX2 = X2.ToDataPoint();
            OutputX1 = result[0].ToDataPoint();
            OutputX2 = result[1].ToDataPoint();
        }
    }
}