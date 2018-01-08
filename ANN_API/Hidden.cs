using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANN_API
{
    public class HiddenLayer
    {
        //trong so cho node cua lop hidden(thuoc tinh)
        private double[] weight = new double[25];

        //tao index
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

        //khoi tao trong so
        public HiddenLayer(double[] w)
        {
            for (int i = 0; i < 25; i++)
                weight[i] = w[i];
        }

        //netinput cho node hidden
        public static double NetInputH(object obj, double[] input)
        {
            HiddenLayer hd = (HiddenLayer)obj;
            double s = 0;
            for (int i = 0; i < hd.weight.Length; i++)
                s += hd.weight[i] * input[i];
            return s;
        }

        public double OutPut(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        ~HiddenLayer()
        {
        }
    }
}
