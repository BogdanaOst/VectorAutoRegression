using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using VectorAutoregression.Models;
using Extreme.Statistics;

namespace VectorAutoregression.Services
{
    public static class ComputeService
    {
        #region Extentions
        public static List<DataPoint> ToDataPoint(this List<double> set)
        {
            var result = new List<DataPoint>();
            for (int i = 0; i < set.Count; i++)
            {
                result.Add(new DataPoint(i, set[i]));
            }
            return result;
        }

        #endregion

        #region Methods

        public static List<double>[] PredictedVar(List<double> X1, List<double> X2, out double[,] B, out double[,] Q)
        {
            var M = Matrix<double>.Build;
            var V = Vector<double>.Build;
            var Y = M.DenseOfColumnArrays(X1.Skip(1).ToArray(), X2.Skip(1).ToArray());
            var Z = M.DenseOfColumnArrays(ones(X1.Count-1), X1.Take(X1.Count - 1).ToArray(), X2.Take(X1.Count - 1).ToArray());

            var X1Pr = new double[X1.Count];
            var X2Pr = new double[X1.Count];
            X1Pr[0] = X1[0];
            X2Pr[0] = X2[0];

            var bM = ((Z.TransposeThisAndMultiply(Z)).Inverse() * Z.TransposeThisAndMultiply(Y));
            B = bM.Transpose().ToArray();
            for (int i = 1; i < X1.Count; i++)
            {
                X1Pr[i] = B[0, 0] + B[0, 1] * X1Pr[i - 1] + B[0, 2] * X2Pr[i - 1];
                X2Pr[i] = B[1, 0] + B[1, 1] * X1Pr[i - 1] + B[1, 2] * X2Pr[i - 1];
            }
            double Fk1, Fn1, Fk2, Fn2;
            var o1 = IsModelOk(X1, X1Pr.ToList(), out Fk1, out Fn1);
            var o2 = IsModelOk(X2, X2Pr.ToList(), out Fk2, out Fn2);
            Q = new double[2, 3] { { Convert.ToInt32(o1), Fk1, Fn1 },{Convert.ToInt32(o2), Fk2, Fn2 } };
            return new List<double>[2]
            {
                X1Pr.ToList(),
                X2Pr.ToList()
            };
        }
        
        private static Func<int, double[]> ones = new Func<int, double[]>(n =>
        {
            var res = new double[n];
            for (int i = 0; i < n; i++)
                res[i] = 1;
            return res;
        });

        public static bool IsModelOk(List<double> Orig, List<double> Pred, out double Fk, out double Fn)
        {
            var eps = new double[Orig.Count];
            double n = Orig.Count - 2.0;
            for (int i=0;i<Orig.Count;i++)
            {
                eps[i] = Orig[i] - Pred[i];
            }
            var M = Matrix<double>.Build;
            var epsM = M.DenseOfColumnArrays(eps);
            var Sa = (epsM.TransposeThisAndMultiply(epsM) / (n))[0,0];
            var Sy = Statistics.Variance(Orig);
            Fn = Sa / Sy;
            Fk = MathNet.Numerics.Distributions.FisherSnedecor.InvCDF(n - 2, n - 1, 0.05);
            return Fk > Fn;
        }
        #endregion
    }
}