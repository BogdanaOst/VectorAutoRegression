using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MathNet.Numerics.Statistics;
using VectorAutoregression.Services;

namespace VectorAutoregression.Models
{
    public class PreProcessDataModel
    {
        public List<DataPoint> InputX1 { get; set; }
        public List<DataPoint> InputX2 { get; set; }

        public List<DataPoint> AkfX1 { get; set; }
        public List<DataPoint> AkfX2 { get; set; }

        public List<DataPoint> Delta1X1 { get; set; }
        public List<DataPoint> Delta1X2 { get; set; }

        public List<DataPoint> DeltaX1Akf { get; set; }
        public List<DataPoint> DeltaX2Akf { get; set; }

        public bool x1IsStat, x2IsStat, dx1IsStat, dx2IsStat;

        #region Constructors

        public PreProcessDataModel(List<double> inputX1, List<double> inputX2)
        {
            this.InputX1 = inputX1.ToDataPoint();
            this.InputX2 = inputX2.ToDataPoint();
            FillAkfs();
            FillDeltas();
        }

        #endregion

        #region Methods

        private void FillAkfs()
        {
            var akf1 = Correlation.Auto(InputX1.Select(s => s.Y.Value).ToArray());
            var akf2 = Correlation.Auto(InputX2.Select(s => s.Y.Value).ToArray());
            this.AkfX1 = akf1.ToList().ToDataPoint();
            this.AkfX2 = akf2.ToList().ToDataPoint();
        }

        private void FillDeltas()
        {
            if (!x1IsStat)
            {
                this.Delta1X1 = Delta1(InputX1);
                this.DeltaX1Akf = Correlation.Auto(Delta1X1.Select(s => s.Y.Value).ToArray()).ToList().ToDataPoint();
            }

            if (!x2IsStat)
            {
                this.Delta1X2 = Delta1(InputX2);
                this.DeltaX2Akf = Correlation.Auto(Delta1X2.Select(s => s.Y.Value).ToArray()).ToList().ToDataPoint();
            }
        }

        private List<DataPoint> Delta1(List<DataPoint> input)
        {
            var result = new List<DataPoint>();
            for (int i = 1; i < input.Count; i++)
            {
                result.Add(new DataPoint(input[i].x.Value, input[i].Y.Value - input[i - 1].Y.Value));
            }
            return result;
        }

        //private bool CheckIfStatByAkf(List<double> input)
        //{
        //    var pm = new List<double>();

        //}

        #endregion
    }
}