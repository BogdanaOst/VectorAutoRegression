using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VectorAutoregression.Models;

namespace VectorAutoregression.Services
{
    public static class InputConverter
    {
        public static VarDataModel ConvertToDM(HttpPostedFileBase file)
        {
            var x1 = new List<double>();
            var x2 = new List<double>();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(file.InputStream))
            {
                while (!reader.EndOfStream)
                {
                    var str = reader.ReadLine().Split(' ', '\t');
                    x1.Add(Convert.ToDouble(str[0].Replace(',','.')));
                    x2.Add(Convert.ToDouble(str[1].Replace(',', '.')));
                }
            }

            return new VarDataModel(x1, x2);
        }
    }
}