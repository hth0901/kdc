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

        public void updateOutWeightValues(double deltaOutput, double[] hiddenValues, double[] arrWeightOfOutput)
        {
            for (int i = 0; i < arrWeightOfOutput.Length; i++)
            {
                arrWeightOfOutput[i] = arrWeightOfOutput[i] + (learningRate * deltaOutput * hiddenValues[i]);
            }
        }

        public void updateHiddenWeightValues(double[] deltaHiddens, double[][] arrWeightOfHidden, double[] arrInputValues)
        {
            for (int j = 0; j < arrWeightOfHidden.Length; j++)
            {
                for (int i = 0; i < arrWeightOfHidden[0].Length; i++)
                {
                    arrWeightOfHidden[j][i] = arrWeightOfHidden[j][i] + (learningRate * deltaHiddens[j] * valueOfInputNode[0][i]);
                }
            }
        }

        public void updateAllWeights(double[] arrInputValues, double nOutputValue, double nOutputExpect, double[] arrHiddenValues, double[][] arrWeightOfHidden, double[] arrWeightOfOutput)
        {
            double deltaOutput = calDeltaOutput(nOutputValue, nOutputExpect);
            deltaOutput = Math.Round(deltaOutput, 4);

            double[] deltaHidden = new double[numberOfHiddenNode];
            for (int i = 0; i < deltaHidden.Length; i++)
            {
                int index = i + 1;
                deltaHidden[i] = calDelHidden(arrHiddenValues[index], index, deltaOutput, arrWeightOfOutput);
            }

            updateOutWeightValues(deltaOutput, arrHiddenValues, arrWeightOfOutput);
            updateHiddenWeightValues(deltaHidden, arrWeightOfHidden, arrInputValues);
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

        public void trainOnePattern(double[] inputValues, double outputExpect, double[][] arrWeightOfHidden, double[] arrWeightOfOutput)
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
                hiddenValues[i] = Math.Round(hiddenValues[i], 3);
            }

            double netWeightFromHidden = calNetWeight(arrWeightOfOutput, hiddenValues);
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

            updateAllWeights(inputValues, outputValue, outputExpect, hiddenValues, arrWeightOfHidden, arrWeightOfOutput);
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

            double[][] initWeightHidden = new double[numberOfHiddenNode][];
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
            MessageBox.Show("sdfsfsdf");
        }

        private void btnLoadRawData_Click(object sender, EventArgs e)
        {
            /*using (var sr = new StreamReader(@"trainTest.csv"))
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
                    double expect = rec.expect;
                    values.Add(value.ToArray());
                    expectValues.Add(expect);
                }

                numberOfPattern = values.Count;
                valueOfInputNode = values.ToArray();
                valueExpectOutput = expectValues.ToArray();

                MessageBox.Show("Loaded!!");
            }*/
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
                        double expect = rec.expect;
                        values.Add(value.ToArray());
                        expectValues.Add(expect);
                    }

                    numberOfPattern = values.Count;
                    valueOfInputNode = values.ToArray();
                    valueExpectOutput = expectValues.ToArray();

                    MessageBox.Show("Loaded!!");
                }
            }
        }

        private void btnWriteFile_Click(object sender, EventArgs e)
        {
            string fileName = @"hihihaha.csv";
            InputValue[] data = new InputValue[2];
            data[0] = new InputValue();
            data[0].x0 = 1;
            data[0].x1 = 2;
            data[0].x2 = 3;
            data[0].x3 = 4;
            data[0].expect = 5;

            data[1] = new InputValue();
            data[1].x0 = 11;
            data[1].x1 = 21;
            data[1].x2 = 31;
            data[1].x3 = 41;
            data[1].expect = 51;
            IEnumerable<InputValue> test = data.ToList();
            using(var wr = new StreamWriter(fileName))
            {
                var csvWriter = new CsvWriter(wr);
                //csvWriter.WriteHeader<InputValue>();
                //csvWriter.NextRecord();
                csvWriter.WriteRecords(test);
            }
        }
    }
}
