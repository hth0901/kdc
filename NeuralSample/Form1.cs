using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NeuralSample
{    
    public partial class Form1 : Form
    {
        private static int vNumberOfPattern = 1;
        private static int vNumberOfInputNode = 3;      
        private static int vNumberOfHiddenNode = 2;
        private static int vNumberOfOutputNode = 1;

        private double[][] rawInput = new double[vNumberOfPattern][];
        private double[][] rawOutput = new double[vNumberOfPattern][];
        private double[][] hWeight = new double[2][];
        private double[] oWeight = new double[3];

        private double[][] vTest;

        public void trainANN(double[][] lstRawInput, double[][] lstRawOutput)
        {
            double lRate = 0.9;
            double[] arrNetInput = new double[oWeight.Length];
            for (int i = 0; i < arrNetInput.Length; i++)
            {
                if (i == 0)
                    arrNetInput[i] = 1;
                else
                {
                    arrNetInput[i] = netInput(hWeight[i - 1], lstRawInput[0]);
                }
            }

            double[] outputFromHidden = new double[arrNetInput.Length];
            for (int i = 0; i < outputFromHidden.Length; i++)
            {
                if (i == 0)
                    outputFromHidden[i] = 1;
                else
                {
                    outputFromHidden[i] = calNodeValue(arrNetInput[i]);
                    outputFromHidden[i] = Math.Round(outputFromHidden[i], 3);
                }
            }

            double netOutput = netInput(oWeight, outputFromHidden);
            double outPutForward = calNodeValue(netOutput);

            double deltaOutPut = deltaOutCal(outPutForward, rawOutput[0][0]);

            double[] deltaHidden = new double[2];
            for (int i = 0; i < deltaHidden.Length; i++)
            {
                int index = i + 1;
                deltaHidden[i] = deltaHiddenCal(outputFromHidden[index], index, deltaOutPut);
            }

            MessageBox.Show("ok");
        }

        public double deltaOutCal(double valCal, double valReal)
        {
            double result = new double();
            result = valCal * (1 - valCal) * (valReal - valCal);
            return result;
        }

        public double deltaHiddenCal(double outHidden, int nIndex, double nDeltaOut)
        {
            double result = new double();
            result = outHidden * (1 - outHidden) * (oWeight[nIndex] * nDeltaOut);
            return result;
        }

        public double netInput(double[] weight, double[] input)
        {
            double result = new double();
            for (int i = 0; i < input.Length; i++)
                result = result + weight[i] * input[i];
            return result;
        }

        public double calNetWeight(double[] weightValue, double[] nodeValue)
        {
            double result = 0;
            for (int i = 0; i < nodeValue.Length; i++)
                result = result + weightValue[i] * nodeValue[i];
            return result;
        }

        public double calNodeValue(double netValue)
        {
            return 1 / (1 + Math.Exp(-netValue)); 
        }

        public static double Sigmod(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        } 
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < hWeight.Length; i++)
                hWeight[i] = new double[4];
            hWeight[0][0] = -0.4;
            hWeight[0][1] = 0.2;
            hWeight[0][2] = 0.4;
            hWeight[0][3] = -0.5;

            hWeight[1][0] = 0.2;
            hWeight[1][1] = -0.3;
            hWeight[1][2] = 0.1;
            hWeight[1][3] = 0.2;

            oWeight[0] = 0.1;
            oWeight[1] = -0.3;
            oWeight[2] = -0.2;

            rawInput[0] = new double[4];
            rawInput[0][0] = 1;
            rawInput[0][1] = 1;
            rawInput[0][2] = 0;
            rawInput[0][3] = 1;

            rawOutput[0] = new double[1];
            rawOutput[0][0] = 1;

            //trainANN(rawInput, rawOutput);
            MessageBox.Show("asdfsdf");
        }
    }
}
