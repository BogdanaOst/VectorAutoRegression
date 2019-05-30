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

        public List<DataPoint> FinInp1 { get; set; }
        public List<DataPoint> FinInp2 { get; set; }

        public List<DataPoint> OutputX1 { get; set; }
        public List<DataPoint> OutputX2 { get; set; }

        public double[,] CoefMatrix;
        public string needToUseDeltas;

        public VarDataModel(List<double> X1, List<double> X2)
        {
            InputX1 = X1.ToDataPoint();
            InputX2 = X2.ToDataPoint();
            List<double>[] result;
            if (NeedDiffs())
            {
                this.FinInp1 = Delta(InputX1);
                this.FinInp2 = Delta(InputX2);
                result = ComputeService.PredictedVar(FinInp1.Select(d=>d.Y.Value).ToList(), FinInp2.Select(d => d.Y.Value).ToList(), out CoefMatrix);
            }
            else
            {
                this.FinInp1 = InputX1;
                this.FinInp2 = InputX2;
                result = ComputeService.PredictedVar(X1, X2, out CoefMatrix);
            }
            OutputX1 = result[0].ToDataPoint();
            OutputX2 = result[1].ToDataPoint();
        }

        private List<DataPoint> Delta(List<DataPoint> input)
        {
            var result = new List<DataPoint>();
            for (int i = 1; i < input.Count; i++)
            {
                result.Add(new DataPoint(input[i].x.Value, input[i].Y.Value - input[i - 1].Y.Value));
            }
            return result;
        }

        private bool NeedDiffs()
        {
            
            needToUseDeltas = "Series are stationar or non-cointegrative. Will build model with original data";
            return false;
        }
    }
}