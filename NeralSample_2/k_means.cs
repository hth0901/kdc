using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeralSample_2
{
    public class k_means
    {
        private static double[][] Normalized(double[][] rawData)
        {
            double[][] result = new double[rawData.Length][];
            for (int i = 0; i < rawData.Length; i++)
            {
                result[i] = new double[rawData[i].Length];
                Array.Copy(rawData[i], result[i], rawData[i].Length);
            }

            for (int j = 0; j < result[0].Length; j++)
            {
                double colSum = 0.0;
                for (int i = 0; i < result.Length; i++)
                    colSum += result[i][j];
                double mean = colSum / result.Length;
                double sum = 0.0;
                for (int i = 0; i < result.Length; i++)
                    sum += ((result[i][j] - mean) * (result[i][j] - mean));
                double sd = sum / result.Length;
                for (int i = 0; i < result.Length; i++)
                    result[i][j] = (result[i][j] - mean) / sd;
            }
            return result;
        }

        public static double[][] MinMaxNormalized(double[][] rawData)
        {
            double[][] result = new double[rawData.Length][];
            for (int i = 0; i < rawData.Length; i++)
            {
                result[i] = new double[rawData[i].Length];
                Array.Copy(rawData[i], result[i], rawData[i].Length);
            }
            //double newMax = 3, newMin = -3;
            double newMax = 10, newMin = -10;
            double[] min = new double[rawData[0].Length];
            double[] max = new double[rawData[0].Length];
            for (int i = 0; i < min.Length; i++)
            {
                min[i] = rawData[0][i];
                max[i] = rawData[0][i];
            }

            for (int j = 0; j < rawData[0].Length; j++)
            {
                for (int i = 0; i < rawData.Length; i++)
                {
                    if (rawData[i][j] > max[j])
                        max[j] = rawData[i][j];
                    if (rawData[i][j] < min[j])
                        min[j] = rawData[i][j];
                }
            }

            for (int j = 0; j < rawData[0].Length; j++)
            {
                for (int i = 0; i < result.Length; i++)
                    result[i][j] = (((rawData[i][j] - min[j]) / (max[j] - min[j])) * (newMax - newMin)) + newMin;
            }

            return result;
        }

        private static int[] InitClusterting(int numTuples, int numClusters, int seed)
        {
            Random random = new Random(seed);
            int[] clustering = new int[numTuples];
            for (int i = 0; i < numClusters; i++)
                clustering[i] = i;
            for (int i = numClusters; i < clustering.Length; i++)
                clustering[i] = random.Next(0, numClusters);
            return clustering;
        }

        private static double[][] Allocate(int numClusters, int numColumns)
        {
            double[][] result = new double[numClusters][];
            for (int k = 0; k < numClusters; k++)
                result[k] = new double[numColumns];
            return result;
        }

        private static bool UpdateMeans(double[][] data, int[] clustering, double[][] means)
        {
            //scan clustering input array parameter and đếm số bộ dữ liệu được gán vào mỗi cụm
            /*
             * Nếu bất kỳ cụm nào ko có bộ dữ liệu được gán, hàm sẽ thoát và trả về false
             * Có thể bỏ qua nếu những phương thức có khởi tạo việc gom cụm và update việc gom cụm đều không có cụm nào có số lượng bằng 0
             */
            int numClusters = means.Length;
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Length; i++)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numClusters; k++)
                if (clusterCounts[k] == 0)
                    return false;

            for (int k = 0; k < means.Length; k++)
                for (int j = 0; j < means[k].Length; j++)
                    means[k][j] = 0.0;

            for (int i = 0; i < data.Length; i++)
            {
                int cluster = clustering[i];
                for (int j = 0; j < data[i].Length; j++)
                    means[cluster][j] += data[i][j]; //accumulate sum
            }

            for (int k = 0; k < means.Length; k++)
                for (int j = 0; j < means[k].Length; j++)
                    means[k][j] /= clusterCounts[k]; //danger of div by 0
            return true;
        }

        private static bool UpdateClustering(double[][] data, int[] clustering, double[][] means)
        {
            int numCluster = means.Length;
            bool changed = false;

            int[] newClustering = new int[clustering.Length];
            Array.Copy(clustering, newClustering, clustering.Length);

            double[] distances = new double[numCluster];

            for (int i = 0; i < data.Length; i++)
            {
                for (int k = 0; k < numCluster; k++)
                    distances[k] = Helper.EuclidDistance(data[i], means[k]);
                //distances[k] = KmeanHelper.Distance(data[i], means[k]);
                //distances[k] = KmeanHelper.Similarity(data[i], means[k]);

                //int newClusterID = KmeanHelper.MinIndex(distances);
                int newClusterID = Helper.MinIndex(distances);
                if (newClusterID != newClustering[i])
                {
                    changed = true;
                    newClustering[i] = newClusterID;
                }
            }

            if (changed == false)
                return false;
            int[] clusterCounts = new int[numCluster];
            for (int i = 0; i < data.Length; i++)
            {
                int cluster = newClustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numCluster; k++)
                if (clusterCounts[k] == 0)
                    return false;

            Array.Copy(newClustering, clustering, newClustering.Length);
            return true; //no zero-counts and at least one change
        }

        public static double[][] MeansCal(double[][] rawdata, int[] clustering, int numCluster)
        {
            double[][] result = new double[numCluster][];
            for (int i = 0; i < result.Length; i++)
                result[i] = new double[rawdata[0].Length];
            int[] clusterCounts = new int[numCluster];
            for (int i = 0; i < rawdata.Length; i++)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < result.Length; k++)
                for (int j = 0; j < result[k].Length; j++)
                    result[k][j] = 0.0;

            for (int i = 0; i < rawdata.Length; i++)
            {
                int cluster = clustering[i];
                for (int j = 0; j < rawdata[i].Length; j++)
                    result[cluster][j] += rawdata[i][j];
            }

            for (int k = 0; k < result.Length; k++)
                for (int j = 0; j < result[k].Length; j++)
                    result[k][j] /= clusterCounts[k];

            return result;
        }

        public static double[][] MeansCal(double[][] rawdata, int numCluster, out int[] clustering)
        {
            double[][] result = new double[numCluster][];
            //int[] clustering = Cluster(rawdata, numCluster);
            clustering = Cluster(rawdata, numCluster);
            for (int i = 0; i < result.Length; i++)
                result[i] = new double[rawdata[0].Length];
            int[] clusterCounts = new int[numCluster];
            for (int i = 0; i < rawdata.Length; i++)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < result.Length; k++)
                for (int j = 0; j < result[k].Length; j++)
                    result[k][j] = 0.0;

            for (int i = 0; i < rawdata.Length; i++)
            {
                int cluster = clustering[i];
                for (int j = 0; j < rawdata[i].Length; j++)
                    result[cluster][j] += rawdata[i][j];
            }

            for (int k = 0; k < result.Length; k++)
                for (int j = 0; j < result[k].Length; j++)
                    result[k][j] /= clusterCounts[k];

            return result;
        }

        public static int[] Cluster(double[][] rawData, int numCluster)
        {
            double[][] data = Normalized(rawData);
            //double[][] data = MinMaxNormalized(rawData);
            //double[][] data = rawData;
            bool changed = true;
            bool success = true;
            int[] clustering = InitClusterting(data.Length, numCluster, 0);
            double[][] means = Allocate(numCluster, data[0].Length);
            int maxcount = data.Length * 10;
            int ct = 0;
            while (changed == true && success == true && ct < maxcount)
            {
                ++ct;
                success = UpdateMeans(data, clustering, means);
                changed = UpdateClustering(data, clustering, means);
            }
            return clustering;
        }

        public static void GetAllCluster(out List<double[]> cluster0, out List<double[]> cluster1)
        {
            cluster0 = new List<double[]>();
            cluster1 = new List<double[]>();
        }
    }
}
