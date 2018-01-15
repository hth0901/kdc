using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeralSample_2
{
    public class Helper
    {
        public static double maximum(double x, double y)
        {
            if (x > y)
                return x;
            else
                return y;
        }

        public static int maximum(int x, int y)
        {
            if (x > y)
                return x;
            else
                return y;
        }

        public static double minimum(double x, double y)
        {
            if (x > y)
                return y;
            else
                return x;
        }

        public static int minimum(int x, int y)
        {
            if (x > y)
                return y;
            else
                return x;
        }

        public static double EuclidDistance(double[] p1, double[] p2)
        {
            int length = minimum(p1.Length, p2.Length);
            double r = 0;
            for (int i = 0; i < length; i++)
                r += Math.Pow((p1[i] - p2[i]), 2);
            r = Math.Sqrt(r);
            return r;
        }

        public static int MinIndex(double[] distances)
        {
            int indexofMin = 0;
            double smallDist = distances[0];
            for (int k = 0; k < distances.Length; k++)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k];
                    indexofMin = k;
                }
            }
            return indexofMin;
        }

        public static double Similarity(double[] centerPoint, double[] point)
        {
            int index;
            double offset = 0;
            double res = 0;
            //int length = centerPoint.Length;
            int length = minimum(centerPoint.Length, point.Length);
            for (index = 0; index < length; index++)
                offset += point[index] - centerPoint[index];
            offset /= length;
            for (index = 0; index < length; index++)
                //res += Math.Pow((centerPoint[index] - point[index] + offset), 2);
                res += Math.Pow((centerPoint[index] - point[index] - offset), 2);
            return res;
        }

        public static double[] MinMaxNormalize(double[] rawData)
        {
            double[] result = new double[rawData.Length];
            Array.Copy(rawData, result, rawData.Length);
            //double newMax = 3, newMin = -3;
            double newMax = 10, newMin = -10;
            double min = rawData[0];
            double max = rawData[0];
            for (int i = 0; i < rawData.Length; i++)
            {
                if (rawData[i] > max)
                    max = rawData[i];
                if (rawData[i] < min)
                    min = rawData[i];
            }

            for (int i = 0; i < rawData.Length; i++)
                result[i] = (((rawData[i] - min) / (max - min)) * (newMax - newMin)) + newMin;

            //for (int j = 0; j < rawData[0].Length; j++)
            //{
            //    for (int i = 0; i < result.Length; i++)
            //        result[i][j] = (((rawData[i][j] - min[j]) / (max[j] - min[j])) * (newMax - newMin)) + newMin;
            //}

            return result;
        }
    }
}
