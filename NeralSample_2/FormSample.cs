using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CsvHelper;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NeralSample_2
{
    public partial class FormSample : Form
    {
        private static int numberOfPattern = 1;
        private static int numberOfInputNode = 3;
        private static int numberOfHiddenNode = 2;
        private static int numberOfOutputNode = 1;

        private static double learningRate = 0.9;

        private double[][] valueOfInputNode = new double[numberOfPattern][];
        private double[] valueExpectOutput = new double[numberOfPattern];
        private double[][] hiddenWeight = new double[numberOfHiddenNode][];
        private double[] outputWeight = new double[numberOfHiddenNode + 1];
        private double[] cluster_1;
        private double[] cluster_2;
        private NeuralNetwork neural_1 = new NeuralNetwork();
        private NeuralNetwork neural_2 = new NeuralNetwork();

        public double calNetWeight(double[] weightValues, double[] nodeValues)
        {
            double result = 0;
            for (int i = 0; i < nodeValues.Length; i++)
                result = result + weightValues[i] * nodeValues[i];
            return result;
        }
        public double calNodeValue(double netValue)
        {
            return 1 / (1 + Math.Exp(-netValue));
        }

        public double calDeltaOutput(double outputValue, double expectValue)
        {
            double result = 0;
            result = outputValue * (1 - outputValue) * (expectValue - outputValue);
            return result;
        }

        public double calDelHidden(double hiddenValue, int indexOfHiddenNode, double nDeltaOut, double[] arrWeightOfOutput)
        {
            double result = 0;
            result = hiddenValue * (1 - hiddenValue) * arrWeightOfOutput[indexOfHiddenNode] * nDeltaOut;
            return result;
        }

        public void updateOutWeightValues(double deltaOutput, double[] hiddenValues, ref double[] arrWeightOfOutput)
        {
            for (int i = 0; i < arrWeightOfOutput.Length; i++)
            {
                arrWeightOfOutput[i] = arrWeightOfOutput[i] + (learningRate * deltaOutput * hiddenValues[i]);
            }
        }

        public void updateHiddenWeightValues(double[] deltaHiddens, ref double[][] arrWeightOfHidden, double[] arrInputValues)
        {
            for (int j = 0; j < arrWeightOfHidden.Length; j++)
            {
                for (int i = 0; i < arrWeightOfHidden[0].Length; i++)
                {
                    arrWeightOfHidden[j][i] = arrWeightOfHidden[j][i] + (learningRate * deltaHiddens[j] * valueOfInputNode[0][i]);
                }
            }
        }

        public void updateAllWeights(double[] arrInputValues, double nOutputValue, double nOutputExpect, double[] arrHiddenValues, ref double[][] arrWeightOfHidden, ref double[] arrWeightOfOutput)
        {
            double deltaOutput = calDeltaOutput(nOutputValue, nOutputExpect);
            deltaOutput = Math.Round(deltaOutput, 6);

            double[] deltaHidden = new double[numberOfHiddenNode];
            for (int i = 0; i < deltaHidden.Length; i++)
            {
                int index = i + 1;
                deltaHidden[i] = calDelHidden(arrHiddenValues[index], index, deltaOutput, arrWeightOfOutput);
            }

            updateOutWeightValues(deltaOutput, arrHiddenValues, ref arrWeightOfOutput);
            updateHiddenWeightValues(deltaHidden, ref arrWeightOfHidden, arrInputValues);
            double e = 0;
        }

        public double calOutPut(double[] inputValues)
        {
            double result = 0;

            double[] netWeightFromInput = new double[numberOfHiddenNode];
            for (int i = 0; i < netWeightFromInput.Length; i++)
            {
                netWeightFromInput[i] = calNetWeight(hiddenWeight[i], inputValues);
            }

            double[] hiddenValues = new double[numberOfHiddenNode + 1];
            for (int i = 0; i < hiddenValues.Length; i++)
            {
                if (i == 0)
                    hiddenValues[i] = 1;
                else
                {
                    hiddenValues[i] = calNodeValue(netWeightFromInput[i - 1]);
                }
                hiddenValues[i] = Math.Round(hiddenValues[i], 6);
            }

            double netWeightFromHidden = calNetWeight(outputWeight, hiddenValues);
            double outputValue = calNodeValue(netWeightFromHidden);
            outputValue = Math.Round(outputValue, 6);
            result = outputValue;
            return outputValue;
        }

        //public void trainOnePattern(double[] inputValues, double outputExpect, double[][] arrWeightOfHidden, double[] arrWeightOfOutput)
        public double trainOnePattern(double[] inputValues, double outputExpect, ref double[][] arrWeightOfHidden, ref double[] arrWeightOfOutput)
        {
            double result = 0;

            double[] netWeightFromInput = new double[numberOfHiddenNode];
            for (int i = 0; i < netWeightFromInput.Length; i++)
            {
                netWeightFromInput[i] = calNetWeight(arrWeightOfHidden[i], inputValues);
            }

            double[] hiddenValues = new double[numberOfHiddenNode + 1];
            for (int i = 0; i < hiddenValues.Length; i++)
            {
                if (i == 0)
                    hiddenValues[i] = 1;
                else
                {
                    hiddenValues[i] = calNodeValue(netWeightFromInput[i - 1]);
                }
                hiddenValues[i] = Math.Round(hiddenValues[i], 6);
            }

            double netWeightFromHidden = calNetWeight(arrWeightOfOutput, hiddenValues);
            double outputValue = calNodeValue(netWeightFromHidden);
            outputValue = Math.Round(outputValue, 6);

            result = outputValue;

            /*double deltaOutput = calDeltaOutput(outputValue, outputExpect);
            deltaOutput = Math.Round(deltaOutput, 4);

            double[] deltaHidden = new double[numberOfHiddenNode];
            for (int i = 0; i < deltaHidden.Length; i++)
            {
                int index = i + 1;
                deltaHidden[i] = calDelHidden(hiddenValues[index], index, deltaOutput);
            }

            updateOutWeightValues(deltaOutput, hiddenValues);
            updateHiddenWeightValues(deltaHidden);*/

            updateAllWeights(inputValues, outputValue, outputExpect, hiddenValues, ref arrWeightOfHidden, ref arrWeightOfOutput);
            return result;
        }
        public FormSample()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < hiddenWeight.Length; i++)
                hiddenWeight[i] = new double[numberOfInputNode + 1];

            hiddenWeight[0][0] = -0.4;
            hiddenWeight[0][1] = 0.2;
            hiddenWeight[0][2] = 0.4;
            hiddenWeight[0][3] = -0.5;

            hiddenWeight[1][0] = 0.2;
            hiddenWeight[1][1] = -0.3;
            hiddenWeight[1][2] = 0.1;
            hiddenWeight[1][3] = 0.2;

            outputWeight[0] = 0.1;
            outputWeight[1] = -0.3;
            outputWeight[2] = -0.2;

            /*valueOfInputNode[0] = new double[4];
            valueOfInputNode[0][0] = 1;
            valueOfInputNode[0][1] = 1;
            valueOfInputNode[0][2] = 0;
            valueOfInputNode[0][3] = 1;

            valueExpectOutput[0] = 1;*/

            //trainOnePattern(valueOfInputNode[0], valueExpectOutput[0]);

            /*double[][] initWeightHidden = new double[numberOfHiddenNode][];
            for (int i = 0; i < initWeightHidden.Length; i++)
                initWeightHidden[i] = new double[numberOfInputNode + 1];
            initWeightHidden[0][0] = -0.4;
            initWeightHidden[0][1] = 0.2;
            initWeightHidden[0][2] = 0.4;
            initWeightHidden[0][3] = -0.5;

            initWeightHidden[1][0] = 0.2;
            initWeightHidden[1][1] = -0.3;
            initWeightHidden[1][2] = 0.1;
            initWeightHidden[1][3] = 0.2;

            double[] initWeightOutput = new double[numberOfHiddenNode + 1];
            initWeightOutput[0] = 0.1;
            initWeightOutput[1] = -0.3;
            initWeightOutput[2] = -0.2;

            int n = 0;
            do
            {
                for (int i = 0; i < numberOfPattern; i++)
                {
                    //trainOnePattern(valueOfInputNode[i], valueExpectOutput[i]);
                    trainOnePattern(valueOfInputNode[i], valueExpectOutput[i], initWeightHidden, initWeightOutput);
                }
                n = n + 1;
            }
            while (n < 1);
            //double test = calOutPut(valueOfInputNode[0]);
            MessageBox.Show("sdfsfsdf");*/
            //trainSetOfPattern(valueOfInputNode, valueExpectOutput);
        }

        private void trainSetOfPattern(double[][] setPatternInput, double[] setPatternOutput, int indexOfSet)
        {
            double min = -1.2;
            double max = 1.2;
            numberOfHiddenNode = 10;
            /*double[][] initWeightHidden = new double[numberOfHiddenNode][];
            Random rnd = new Random();
            for (int i = 0; i < initWeightHidden.Length; i++)
            {
                initWeightHidden[i] = new double[numberOfInputNode + 1];
                for (int k = 0; k < initWeightHidden[i].Length; k++)
                {
                    double rndNum = rnd.NextDouble() * (max - min) + min;
                    //initWeightHidden[i][k] = rnd.NextDouble();
                    initWeightHidden[i][k] = Math.Round(rndNum, 5);
                }
            }*/

            double[][] initWeightHidden = { 
                new double[] { -0.4,  0.2,  0.4, -0.5,  0.3,  0.1,  0.6,  0.8,   0.5,  1.5,  -1.6,   0.45, -0.35,  0.61,  0.75, -1.2,   0.5,  1.5, -1.6,   0.45, -0.35,  0.61, 0.75, -1.2,  0.5 },                                                   
                new double[] {  0.3,  0.1,  0.6,  0.8,  0.5,  1.5, -1.6,  0.45, -0.35, 0.61,  0.75, -1.2,   0.5,   1.5,  -1.6,   0.45, -0.12, 0.25, 0.23, -0.71,  0.35, -0.15, 0.31,  0.5, -0.67 },
                new double[] { -0.4,  0.2,  0.4, -0.5,  0.3,  0.1,  0.6,  0.8,   0.5,  1.5,  -1.6,   0.45, -0.35,  0.61,  0.75, -1.2,   0.5,  1.5, -1.6,   0.45, -0.35,  0.61, 0.75, -1.2,  0.5 },                                                   
                new double[] { -0.4,  0.2,  0.4, -0.5,  0.3,  0.1,  0.6,  0.8,   0.5,  1.5,  -1.6,   0.45, -0.35,  0.61,  0.75, -1.2,   0.5,  1.5, -1.6,   0.45, -0.35,  0.61, 0.75, -1.2,  0.5 },                                                   
                new double[] {  0.3,  0.1,  0.6,  0.8,  0.5,  1.5, -1.6,  0.45, -0.35, 0.61,  0.75, -1.2,   0.5,   1.5,  -1.6,   0.45, -0.12, 0.25, 0.23, -0.71,  0.35, -0.15, 0.31,  0.5, -0.67 },
                new double[] { -0.4,  0.2,  0.4, -0.5,  0.3,  0.1,  0.6,  0.8,   0.5,  1.5,  -1.6,   0.45, -0.35,  0.61,  0.75, -1.2,   0.5,  1.5, -1.6,   0.45, -0.35,  0.61, 0.75, -1.2,  0.5 },                                                   
                new double[] {  0.3,  0.1,  0.6,  0.8,  0.5,  1.5, -1.6,  0.45, -0.35, 0.61,  0.75, -1.2,   0.5,   1.5,  -1.6,   0.45, -0.12, 0.25, 0.23, -0.71,  0.35, -0.15, 0.31,  0.5, -0.67 },
                new double[] { -0.4,  0.2,  0.4, -0.5,  0.3,  0.1,  0.6,  0.8,   0.5,  1.5,  -1.6,   0.45, -0.35,  0.61,  0.75, -1.2,   0.5,  1.5, -1.6,   0.45, -0.35,  0.61, 0.75, -1.2,  0.5 },                                                   
                new double[] {  0.3,  0.1,  0.6,  0.8,  0.5,  1.5, -1.6,  0.45, -0.35, 0.61,  0.75, -1.2,   0.5,   1.5,  -1.6,   0.45, -0.12, 0.25, 0.23, -0.71,  0.35, -0.15, 0.31,  0.5, -0.67 },
                new double[] {  0.2, -0.3,  0.1,  0.2,  1.4, -1.3,  0.3,  0.1,   1.5, -1.6,   0.45, -0.35,  0.61,  0.75, -1.2,   0.5,   0.6,  0.8,  0.5,   0.45, -1.8,   0.8,  0.4,  -0.7,  0.5 }
            };
            /*initWeightHidden[0][0] = -0.4;
            initWeightHidden[0][1] = 0.2;
            initWeightHidden[0][2] = 0.4;
            initWeightHidden[0][3] = -0.5;

            initWeightHidden[1][0] = 0.2;
            initWeightHidden[1][1] = -0.3;
            initWeightHidden[1][2] = 0.1;
            initWeightHidden[1][3] = 0.2;*/

            

            /*double[] initWeightOutput = new double[numberOfHiddenNode + 1];
            //initWeightOutput[0] = 0.1;
            //initWeightOutput[1] = -0.3;
            //initWeightOutput[2] = -0.2;
            for (int i = 0; i < initWeightOutput.Length; i++)
            {
                double rndNum = rnd.NextDouble() * (max - min) + min;
                initWeightOutput[i] = Math.Round(rndNum, 5);
            }*/

            double[] initWeightOutput = { 0.1, 0.2, 0.6, -0.7, -0.3, -0.2, 0.25, 0.1, -0.3, -0.2, 0.25 };

            int n = 0;
            int numbetOfSet = setPatternInput.Length;

            double[] arrOutput = new double[numbetOfSet];

            do
            {
                for (int index = 0; index < numbetOfSet; index++)
                {
                    //trainOnePattern(valueOfInputNode[i], valueExpectOutput[i]);
                    //trainOnePattern(setPatternInput[i], setPatternOutput[i], initWeightHidden, initWeightOutput);
                    //arrOutput[index] = trainOnePattern(setPatternInput[index], setPatternOutput[index], ref initWeightHidden, ref initWeightOutput);
                    //double hihihaha = 1.2;

                    //forward phase
                    double result = 0;
                    double[] netWeightFromInput = new double[numberOfHiddenNode];
                    for (int i = 0; i < netWeightFromInput.Length; i++)
                    {
                        netWeightFromInput[i] = calNetWeight(initWeightHidden[i], setPatternInput[index]);
                    }

                    double[] hiddenValues = new double[numberOfHiddenNode + 1];
                    for (int i = 0; i < hiddenValues.Length; i++)
                    {
                        if (i == 0)
                            hiddenValues[i] = 1;
                        else
                            hiddenValues[i] = calNodeValue(netWeightFromInput[i - 1]);
                    }

                    double netWeightFromHidden = calNetWeight(initWeightOutput, hiddenValues);
                    double outputValue = calNodeValue(netWeightFromHidden);

                    //backward phase
                    //cal delta values
                    double deltaOutPut = calDeltaOutput(outputValue, setPatternOutput[index]);
                    double[] deltaHidden = new double[numberOfHiddenNode];
                    for (int i = 0; i < deltaHidden.Length; i++)
                    {
                        deltaHidden[i] = hiddenValues[i + 1] * (1 - hiddenValues[i + 1]) * initWeightOutput[i + 1] * deltaOutPut;
                    }
                    //update all weights
                    //update output weights
                    for (int i = 0; i < initWeightOutput.Length; i++)
                    {
                        initWeightOutput[i] = initWeightOutput[i] + learningRate * deltaOutPut * hiddenValues[i];
                    }
                    //update hidden weights
                    for (int i = 0; i < initWeightHidden.Length; i++)
                        for (int j = 0; j < initWeightHidden[i].Length; j++)
                        {
                            initWeightHidden[i][j] = initWeightHidden[i][j] + (learningRate * deltaHidden[i] * setPatternInput[index][j]);
                        }

                    arrOutput[index] = outputValue;
                }
                n++;
            }
            while (n < 1000);

            HiddenWeight[] arrHiddenWeights = new HiddenWeight[numberOfHiddenNode];
            for (int i = 0; i < numberOfHiddenNode; i++)
            {
                arrHiddenWeights[i] = new HiddenWeight();
                arrHiddenWeights[i].w0 = Math.Round(initWeightHidden[i][0], 6);
                arrHiddenWeights[i].w1 = Math.Round(initWeightHidden[i][1], 6);
                arrHiddenWeights[i].w2 = Math.Round(initWeightHidden[i][2], 6);
                arrHiddenWeights[i].w3 = Math.Round(initWeightHidden[i][3], 6);
                arrHiddenWeights[i].w4 = Math.Round(initWeightHidden[i][4], 6);
                arrHiddenWeights[i].w5 = Math.Round(initWeightHidden[i][5], 6);
                arrHiddenWeights[i].w6 = Math.Round(initWeightHidden[i][6], 6);
                arrHiddenWeights[i].w7 = Math.Round(initWeightHidden[i][7], 6);
                arrHiddenWeights[i].w8 = Math.Round(initWeightHidden[i][8], 6);
                arrHiddenWeights[i].w9 = Math.Round(initWeightHidden[i][9], 6);
                arrHiddenWeights[i].w10 = Math.Round(initWeightHidden[i][10], 6);
                arrHiddenWeights[i].w11 = Math.Round(initWeightHidden[i][11], 6);
                arrHiddenWeights[i].w12 = Math.Round(initWeightHidden[i][12], 6);
                arrHiddenWeights[i].w13 = Math.Round(initWeightHidden[i][13], 6);
                arrHiddenWeights[i].w14 = Math.Round(initWeightHidden[i][14], 6);
                arrHiddenWeights[i].w15 = Math.Round(initWeightHidden[i][15], 6);
                arrHiddenWeights[i].w16 = Math.Round(initWeightHidden[i][16], 6);
                arrHiddenWeights[i].w17 = Math.Round(initWeightHidden[i][17], 6);
                arrHiddenWeights[i].w18 = Math.Round(initWeightHidden[i][18], 6);
                arrHiddenWeights[i].w19 = Math.Round(initWeightHidden[i][19], 6);
                arrHiddenWeights[i].w20 = Math.Round(initWeightHidden[i][20], 6);
                arrHiddenWeights[i].w21 = Math.Round(initWeightHidden[i][21], 6);
                arrHiddenWeights[i].w22 = Math.Round(initWeightHidden[i][22], 6);
                arrHiddenWeights[i].w23 = Math.Round(initWeightHidden[i][23], 6);
                arrHiddenWeights[i].w24 = Math.Round(initWeightHidden[i][24], 6);
            }

            OutWeight outputWeight = new OutWeight();
            outputWeight.w0 = Math.Round(initWeightOutput[0], 6);
            outputWeight.w1 = Math.Round(initWeightOutput[1], 6);
            outputWeight.w2 = Math.Round(initWeightOutput[2], 6);
            outputWeight.w3 = Math.Round(initWeightOutput[3], 6);
            outputWeight.w4 = Math.Round(initWeightOutput[4], 6);
            outputWeight.w5 = Math.Round(initWeightOutput[5], 6);
            outputWeight.w6 = Math.Round(initWeightOutput[6], 6);
            outputWeight.w7 = Math.Round(initWeightOutput[7], 6);
            outputWeight.w8 = Math.Round(initWeightOutput[8], 6);
            outputWeight.w9 = Math.Round(initWeightOutput[9], 6);
            outputWeight.w10 = Math.Round(initWeightOutput[10], 6);

            NeuralNetwork neuralTest = new NeuralNetwork();

            neuralTest.lstHiddenWeights = arrHiddenWeights;
            neuralTest.outputWeight = outputWeight;

            using (StreamWriter file = File.CreateText(@"neural_" + indexOfSet.ToString() + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serializer.Serialize(file, o);
                //serializer.Serialize(file, test_2);
                //serializer.Serialize(file, test_3);
                serializer.Serialize(file, neuralTest);
            }

            MessageBox.Show("sdfsfsdf");
        }

        private void btnLoadRawData_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.FileName = "*.csv";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = this.openFileDialog1.FileName;
                using (var sr = new StreamReader(fileName))
                {
                    var reader = new CsvReader(sr);
                    IEnumerable<InputValue> records = reader.GetRecords<InputValue>();
                    List<double[]> values = new List<double[]>();
                    List<double> expectValues = new List<double>();
                    foreach (InputValue rec in records)
                    {
                        //values.Add(rec.x10);
                        List<double> value = new List<double>();
                        value.Add(rec.x0);
                        value.Add(rec.x1);
                        value.Add(rec.x2);
                        value.Add(rec.x3);
                        value.Add(rec.x4);
                        value.Add(rec.x5);
                        value.Add(rec.x6);
                        value.Add(rec.x7);
                        value.Add(rec.x8);
                        value.Add(rec.x9);
                        value.Add(rec.x10);
                        value.Add(rec.x11);
                        value.Add(rec.x12);
                        value.Add(rec.x13);
                        value.Add(rec.x14);
                        value.Add(rec.x15);
                        value.Add(rec.x16);
                        value.Add(rec.x17);
                        value.Add(rec.x18);
                        value.Add(rec.x19);
                        value.Add(rec.x20);
                        value.Add(rec.x21);
                        value.Add(rec.x22);
                        value.Add(rec.x23);
                        value.Add(rec.x24);
                        double expect = rec.expect;
                        values.Add(value.ToArray());
                        expectValues.Add(expect);
                    }

                    numberOfPattern = values.Count;
                    valueOfInputNode = values.ToArray();
                    valueExpectOutput = expectValues.ToArray();

                    numberOfInputNode = 24;

                    MessageBox.Show("Loaded!!");
                }
            }
        }
        
        private void writeAllMeanClusters(double[][] arrCluster)
        {
            for (int i = 0; i < arrCluster.Length; i++)
            {
                int clusterIndex = i + 1;
                string fileName = @"cluster_" + clusterIndex.ToString() + ".csv";
                MeanCluster[] arrMean = new MeanCluster[1];
                arrMean[0] = new MeanCluster();
                arrMean[0].x0 = Math.Round(arrCluster[i][0], 6);
                arrMean[0].x1 = arrCluster[i][1];
                arrMean[0].x2 = arrCluster[i][2];
                arrMean[0].x3 = arrCluster[i][3];
                arrMean[0].x4 = arrCluster[i][4];
                arrMean[0].x5 = arrCluster[i][5];
                arrMean[0].x6 = arrCluster[i][6];
                arrMean[0].x7 = arrCluster[i][7];
                arrMean[0].x8 = arrCluster[i][8];
                arrMean[0].x9 = arrCluster[i][9];
                arrMean[0].x10 = arrCluster[i][10];
                arrMean[0].x11 = arrCluster[i][11];
                arrMean[0].x12 = arrCluster[i][12];
                arrMean[0].x13 = arrCluster[i][13];
                arrMean[0].x14 = arrCluster[i][14];
                arrMean[0].x15 = arrCluster[i][15];
                arrMean[0].x16 = arrCluster[i][16];
                arrMean[0].x17 = arrCluster[i][17];
                arrMean[0].x18 = arrCluster[i][18];
                arrMean[0].x19 = arrCluster[i][19];
                arrMean[0].x20 = arrCluster[i][20];
                arrMean[0].x21 = arrCluster[i][21];
                arrMean[0].x22 = arrCluster[i][22];
                arrMean[0].x23 = arrCluster[i][23];
                arrMean[0].x24 = arrCluster[i][24];

                IEnumerable<MeanCluster> data = arrMean.ToList();
                using(var wr = new StreamWriter(fileName))
                {
                    var csvWriter = new CsvWriter(wr);
                    csvWriter.WriteRecords(data);
                }
            }
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            int[] arrCluster;
            List<double[]> cluster_1 = new List<double[]>();
            List<double[]> cluster_2 = new List<double[]>();

            List<double> lstOutPut_1 = new List<double>();
            List<double> lstOutPut_2 = new List<double>();

            double[][] arrMeans = k_means.MeansCal(valueOfInputNode, 2, out arrCluster);

            writeAllMeanClusters(arrMeans);

            for (int i = 0; i < arrCluster.Length; i++)
            {
                if (arrCluster[i] == 0)
                {
                    cluster_1.Add(valueOfInputNode[i]);
                    lstOutPut_1.Add(valueExpectOutput[i]);
                }
                else
                {
                    cluster_2.Add(valueOfInputNode[i]);
                    lstOutPut_2.Add(valueExpectOutput[i]);
                }
            }

            MessageBox.Show("cluster finished!!");
            trainSetOfPattern(cluster_1.ToArray(), lstOutPut_1.ToArray(), 1);
            trainSetOfPattern(cluster_2.ToArray(), lstOutPut_2.ToArray(), 2);
            //trainSetOfPattern(valueOfInputNode, valueExpectOutput, 1);
        }

        private void btnOpenJson_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.FileName = "*.json";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = this.openFileDialog1.FileName;
                using(StreamReader file = File.OpenText(fileName))
                {
                    JsonSerializer ser = new JsonSerializer();
                    NeuralNetwork test = (NeuralNetwork)ser.Deserialize(file, typeof(NeuralNetwork));
                    MessageBox.Show("sfsdf");
                }
            }


        }

        private void btnCluster_1_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.FileName = "*.csv";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = this.openFileDialog1.FileName;
                using (var sr = new StreamReader(fileName))
                {
                    var reader = new CsvReader(sr);
                    IEnumerable<MeanCluster> records = reader.GetRecords<MeanCluster>();
                    List<double[]> values = new List<double[]>();
                    List<double> expectValues = new List<double>();
                    foreach (MeanCluster rec in records)
                    {
                        //values.Add(rec.x10);
                        List<double> value = new List<double>();
                        value.Add(rec.x0);
                        value.Add(rec.x1);
                        value.Add(rec.x2);
                        value.Add(rec.x3);
                        value.Add(rec.x4);
                        value.Add(rec.x5);
                        value.Add(rec.x6);
                        value.Add(rec.x7);
                        value.Add(rec.x8);
                        value.Add(rec.x9);
                        value.Add(rec.x10);
                        value.Add(rec.x11);
                        value.Add(rec.x12);
                        value.Add(rec.x13);
                        value.Add(rec.x14);
                        value.Add(rec.x15);
                        value.Add(rec.x16);
                        value.Add(rec.x17);
                        value.Add(rec.x18);
                        value.Add(rec.x19);
                        value.Add(rec.x20);
                        value.Add(rec.x21);
                        value.Add(rec.x22);
                        value.Add(rec.x23);
                        value.Add(rec.x24);
                        values.Add(value.ToArray());
                    }

                    MessageBox.Show("Loaded!!");
                }
            }
        }

        private void btnCluster_2_Click(object sender, EventArgs e)
        {

        }
    }
}
