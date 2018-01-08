using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANN_API
{
    public class OutputLayer
    {
        public delegate double DeleNet(object obj, double[] input);

        //thuoc tinh la cac trong so cua node
        private double[] weight = new double[11];

        //tao index cho tang xuat
        public double this[int index]
        {
            get
            {
                return weight[index];
            }
            set
            {
                weight[index] = value;
            }
        }

        //khoi tao cac trong so
        public OutputLayer(double[] w)
        {
            for (int i = 0; i < 11; i++)
                weight[i] = w[i];
        }

        //tinh netinput cho tang xuat
        public static double NetInputO(object obj, double[] input) //input o day la output cua tang an
        {
            OutputLayer outp = (OutputLayer)obj;
            double s = 0;
            for (int i = 0; i < outp.weight.Length; i++)
                s += outp.weight[i] * input[i];
            return s;
        }

        public double OutPut(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        ~OutputLayer()
        {
        }
    }
}
