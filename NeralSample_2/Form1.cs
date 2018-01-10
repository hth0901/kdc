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

namespace NeralSample_2
{
    public partial class Form1 : Form
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

        public double calDelHidden(double hiddenValue, int indexOfHiddenNode, double nDeltaOut)
        {
            double result = 0;
            result = hiddenValue * (1 - hiddenValue) * outputWeight[indexOfHiddenNode] * nDeltaOut;
            return result;
        }
 
        public void updateOutWeightValues(double deltaOutput, double[] hiddenValues)
        {
            for (int i = 0; i < outputWeight.Length; i++)
            {
                outputWeight[i] = outputWeight[i] + (learningRate * deltaOutput * hiddenValues[i]);
            }
        }

        public void updateHiddenWeightValues(double[] deltaHiddens)
        {
            for (int i = 0; i < hiddenWeight[0].Length; i++)
            {
                hiddenWeight[0][i] = hiddenWeight[0][i] + (learningRate * deltaHiddens[0] * valueOfInputNode[0][i]);
            }
        }

        public void updateAllWeights(double nOutputValue, double nOutputExpect, double[] arrHiddenValues)
        {
            double deltaOutput = calDeltaOutput(nOutputValue, nOutputExpect);
            deltaOutput = Math.Round(deltaOutput, 4);

            double[] deltaHidden = new double[numberOfHiddenNode];
            for (int i = 0; i < deltaHidden.Length; i++)
            {
                int index = i + 1;
                deltaHidden[i] = calDelHidden(arrHiddenValues[index], index, deltaOutput);
            }

            updateOutWeightValues(deltaOutput, arrHiddenValues);
            updateHiddenWeightValues(deltaHidden);
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
                hiddenValues[i] = Math.Round(hiddenValues[i], 3);
            }

            double netWeightFromHidden = calNetWeight(outputWeight, hiddenValues);
            double outputValue = calNodeValue(netWeightFromHidden);
            outputValue = Math.Round(outputValue, 3);
            result = outputValue;
            return outputValue;
        }

        public void trainOnePattern(double[] inputValues, double outputExpect)
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
                hiddenValues[i] = Math.Round(hiddenValues[i], 3);
            }

            double netWeightFromHidden = calNetWeight(outputWeight, hiddenValues);
            double outputValue = calNodeValue(netWeightFromHidden);
            outputValue = Math.Round(outputValue, 3);

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

            updateAllWeights(outputValue, outputExpect, hiddenValues);
        }
        public Form1()
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

            valueOfInputNode[0] = new double[4];
            valueOfInputNode[0][0] = 1;
            valueOfInputNode[0][1] = 1;
            valueOfInputNode[0][2] = 0;
            valueOfInputNode[0][3] = 1;

            valueExpectOutput[0] = 1;

            //trainOnePattern(valueOfInputNode[0], valueExpectOutput[0]);
            int n = 0;
            do
            {
                for (int i = 0; i < numberOfPattern; i++)
                {
                    trainOnePattern(valueOfInputNode[i], valueExpectOutput[i]);
                }
                n = n + 1;
            }
            while (n < 1);
            double test = calOutPut(valueOfInputNode[0]);
            MessageBox.Show("sdfsfsdf");
        }

        private void btnLoadRawData_Click(object sender, EventArgs e)
        {
            using (var sr = new StreamReader(@"trainTest.csv"))
            {
                var reader = new CsvReader(sr);
                IEnumerable<InputValue> records = reader.GetRecords<InputValue>();
                List<double[]> values = new List<double[]>();
                foreach (InputValue rec in records)
                {
                    //values.Add(rec.x10);
                    List<double> value = new List<double>();
                    value.Add(rec.x0);
                    value.Add(rec.x1);
                    value.Add(rec.x2);
                    value.Add(rec.x3);
                    values.Add(value.ToArray());
                }

                MessageBox.Show("");
            }
        }
    }
}
