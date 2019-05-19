using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VectorAutoregression.Models;

namespace VectorAutoregression.Services
{
    public static class VarComputeService
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


        public static List<double>[] Predicted(List<double> X1, List<double> X2, out double[,] B, /*out double[] eps*/)
        {
            var M = Matrix<double>.Build;
            var V = Vector<double>.Build;
            X1.Reverse();
            X2.Reverse();
            var Y1 = M.DenseOfColumnArrays(X1.ToArray());
            var Y2 = M.DenseOfColumnArrays(X2.ToArray());
            var Z = M.DenseOfColumnArrays(ones(X1.Count), X1.ToArray(), X2.ToArray());
            X1.Reverse();
            X2.Reverse();
            var B1 = (Z.TransposeThisAndMultiply(Z)).Inverse() * Z.TransposeThisAndMultiply(Y1);
            var B2 = (Z.TransposeThisAndMultiply(Z)).Inverse() * Z.TransposeThisAndMultiply(Y2);

            var X1Pr = new double[X1.Count];
            var X2Pr = new double[X1.Count];
            X1Pr[0] = X1[0];
            X2Pr[0] = X2[0];
            for (int i = 1; i < X1.Count; i++)
            {
                X1Pr[i] = B1[0, 0] + B1[1, 0] * X1Pr[i - 1] + B1[2, 0] * X2Pr[i - 1];
                X2Pr[i] = B2[0, 0] + B2[1, 0] * X1Pr[i - 1] + B2[2, 0] * X2Pr[i - 1];
            }

            B = (B1.Append(B2)).Transpose().ToArray();
            //eps = new double[2] { };
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
    }
}