using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using CsvHelper;
using System.IO;
using System.Diagnostics;

namespace ANN_API
{
    public delegate double DelOut(double[] wei, double[] test);
    public delegate void TangQuaDelegate(string strQua);

    //tinh net output
    

    public partial class Form1 : Form
    {
        public void tangQua(string strQua)
        {
            MessageBox.Show("Da tang " + strQua);
        }
        public void oNha(string vo, TangQuaDelegate func)
        {
            string qua = "Qua da nhan";
            func(qua);
        }

        private Excel.Application ExcelObj = null;
        private Excel.Workbook Test = null;
        private string[] strArray;

        //mang cac trong so
        private static double[][] hds = new double[11][];
        private static double[][] ops = new double[2][];
        private double[][] rawData = new double[20][];
        private double[][] testData;
        private double[][] trainData;

        //tinh netinput cho hidden
        public static double NetInputH(double[] wei, double[] input)
        {
            
            double s = 0;
            for (int i = 0; i < 25; i++)
                s += wei[i] * input[i];
            return s;
        }


        //tinh netinput cho tang xuat
        public static double NetInputO(double[] wei, double[] input) //input o day la output cua tang an
        {
            
            double s = 0;
            for (int i = 0; i < 11; i++)
                s += wei[i] * input[i];
            return s;
        }



        string[] ConvertToStringArray(System.Array values)
        {
            //create a new string array
            string[] theArray = new string[values.Length];

            //loop through the 2-D System.array and populate the 1-D string array
            for (int i = 1; i <= values.Length; i++)
            {
                if (values.GetValue(1, i) == null)
                    theArray[i - 1] = "";
                else
                    theArray[i - 1] = (string)values.GetValue(1, i).ToString();
            }
            return theArray;

        }

        


        public static double Sigmod(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }



        public Form1()
        {
            InitializeComponent();
            ExcelObj = new Excel.Application();

            if (ExcelObj == null)
            {
                MessageBox.Show("Error: Excel couldn't be started!!!");
                System.Windows.Forms.Application.Exit();
            }
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //he so hoc
            double Lrate = 0.9;
            //khoitao cac trng so cho lop an
            double[][] weightH = { new double[1], new double[] { -0.4,  0.2,  0.4, -0.5,  0.3,  0.1,  0.6,  0.8,   0.5,  1.5,  -1.6,   0.45, -0.35,  0.61,  0.75, -1.2,   0.5,  1.5, -1.6,   0.45, -0.35,  0.61, 0.75, -1.2,  0.5 },                                                   
                                                  new double[] {  0.3,  0.1,  0.6,  0.8,  0.5,  1.5, -1.6,  0.45, -0.35, 0.61,  0.75, -1.2,   0.5,   1.5,  -1.6,   0.45, -0.12, 0.25, 0.23, -0.71,  0.35, -0.15, 0.31,  0.5, -0.67 },
                                                  new double[] { -0.4,  0.2,  0.4, -0.5,  0.3,  0.1,  0.6,  0.8,   0.5,  1.5,  -1.6,   0.45, -0.35,  0.61,  0.75, -1.2,   0.5,  1.5, -1.6,   0.45, -0.35,  0.61, 0.75, -1.2,  0.5 },                                                   
                                                  new double[] { -0.4,  0.2,  0.4, -0.5,  0.3,  0.1,  0.6,  0.8,   0.5,  1.5,  -1.6,   0.45, -0.35,  0.61,  0.75, -1.2,   0.5,  1.5, -1.6,   0.45, -0.35,  0.61, 0.75, -1.2,  0.5 },                                                   
                                                  new double[] {  0.3,  0.1,  0.6,  0.8,  0.5,  1.5, -1.6,  0.45, -0.35, 0.61,  0.75, -1.2,   0.5,   1.5,  -1.6,   0.45, -0.12, 0.25, 0.23, -0.71,  0.35, -0.15, 0.31,  0.5, -0.67 },
                                                  new double[] { -0.4,  0.2,  0.4, -0.5,  0.3,  0.1,  0.6,  0.8,   0.5,  1.5,  -1.6,   0.45, -0.35,  0.61,  0.75, -1.2,   0.5,  1.5, -1.6,   0.45, -0.35,  0.61, 0.75, -1.2,  0.5 },                                                   
                                                  new double[] {  0.3,  0.1,  0.6,  0.8,  0.5,  1.5, -1.6,  0.45, -0.35, 0.61,  0.75, -1.2,   0.5,   1.5,  -1.6,   0.45, -0.12, 0.25, 0.23, -0.71,  0.35, -0.15, 0.31,  0.5, -0.67 },
                                                  new double[] { -0.4,  0.2,  0.4, -0.5,  0.3,  0.1,  0.6,  0.8,   0.5,  1.5,  -1.6,   0.45, -0.35,  0.61,  0.75, -1.2,   0.5,  1.5, -1.6,   0.45, -0.35,  0.61, 0.75, -1.2,  0.5 },                                                   
                                                  new double[] {  0.3,  0.1,  0.6,  0.8,  0.5,  1.5, -1.6,  0.45, -0.35, 0.61,  0.75, -1.2,   0.5,   1.5,  -1.6,   0.45, -0.12, 0.25, 0.23, -0.71,  0.35, -0.15, 0.31,  0.5, -0.67 },
                                                  new double[] {  0.2, -0.3,  0.1,  0.2,  1.4, -1.3,  0.3,  0.1,   1.5, -1.6,   0.45, -0.35,  0.61,  0.75, -1.2,   0.5,   0.6,  0.8,  0.5,   0.45, -1.8,   0.8,  0.4,  -0.7,  0.5 } };
            //khoi tao cac trong so cho lop xuat
            double[][] weightO = { new double[1], new double[] { 0.1, 0.2, 0.6, -0.7, -0.3, -0.2, 0.25, 0.1, -0.3, -0.2, 0.25 } };
            //mau huan luyen dau tien
            //double[][] train = { new double[] { Convert.ToInt32(textBox1.Text.ToString()), Convert.ToInt32(textBox2.Text.ToString()), Convert.ToInt32(textBox3.Text.ToString()), Convert.ToInt32(textBox4.Text.ToString()) } };
            //so input = so tham so + 1
            double[][] train = new double[listView1.Items.Count][];
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                train[i] = new double[25];

                for (int j = 0; j < 25; j++)
                {
                    if (listView1.Items[i].SubItems[j].Text != "")
                        train[i][j] = Double.Parse(listView1.Items[i].SubItems[j].Text);
                }
            }

            
            
            int soluotlatp = 0;
            double[] target = { 0 };
            Array.Resize(ref target, listView1.Items.Count + 1);
            
            for (int i = 1; i < target.Length; i++)
                target[i] = double.Parse(listView1.Items[i - 1].SubItems[26].Text);
            double[] netout = new double[11];
            netout[0] = 1; //tham so cho w0
            double mse = 0;
            double[][] realOut = new double[listView1.Items.Count][];
            for (int i = 0; i < listView1.Items.Count; i++)
                realOut[i] = new double[2];

            //tao mang cac node hidden            
            for (int i = 0; i < 11; i++)
                hds[i] = new double[25]; //co 25 tham so
            for (int i = 1; i < 11; i++)
                for (int j = 0; j < 25; j++)  //j = so tham so + 1
                    hds[i][j] = weightH[i][j];
            DelOut dellHidden = new DelOut(NetInputH);
            DelOut delOut = new DelOut(NetInputO);


            //khoi tao trong so cho tang xuat            
            for (int i = 1; i < ops.Length; i++)
                ops[i] = new double[11];
            for (int i = 1; i < 2; i++)
                for (int j = 0; j < 11; j++)
                    ops[i][j] = weightO[i][j];


            textBox1.Text = NetInputH(hds[1], train[0]).ToString();
            
            do
            {

                mse = 0;

                //delta cho tang xuat
                double[] deltaOut = new double[2];

                //delta cho tang an
                double[] deltaHidden = new double[11];
                //foreach (double[] tr in train)


                for (int k = 0; k < train.Length; k++)
                {

                    

                    //tinh toan netout cho tang an
                    for (int i = 1; i < hds.Length; i++)
                        netout[i] = Sigmod(dellHidden(hds[i], train[k]));                   
                    
                    realOut[k][1] = Sigmod(delOut(ops[1], netout));
                    
                    //tinh toan cac delta
                    //delta cho tang xuat
                    
                    for (int i = 1; i < deltaOut.Length; i++)
                    {
                        deltaOut[i] = realOut[k][i] * (1 - realOut[k][i]) * (target[k+1] - realOut[k][i]);
                    }
                    
                    for (int i = 1; i < hds.Length; i++)
                    {
                        deltaHidden[i] = netout[i] * (1 - netout[i]);
                        double sum = 0;
                        for (int j = 1; j < deltaOut.Length; j++)
                            sum += ops[j][i] * deltaOut[j];
                        deltaHidden[i] = deltaHidden[i] * sum;
                        
                    }
                    
                    //cap nhat lai trong so
                    //cap nhat trong so cho tang xuat
                    for (int i = 0; i < 11; i++)
                    {
                        ops[1][i] = ops[1][i] + (Lrate * deltaOut[1] * netout[i]);
                        
                    }

                    

                    //cap nhat trong so cho tang an
                    for (int j = 1; j < 11; j++)
                    {
                        for (int i = 0; i < 25; i++)
                        {
                            hds[j][i] = hds[j][i] + (Lrate * deltaHidden[j] * train[k][i]);
                            
                        }
                    }
                    double hihihaha = 1.2;
                }
                
                for (int i = 0; i < listView1.Items.Count; i++)
                    mse += Math.Pow((realOut[i][1]) - (target[i + 1]), 2) / listView1.Items.Count;
                soluotlatp++;
                lbl_mse.Text = Math.Round(mse, 4).ToString();
                
            }
            while (soluotlatp<Convert.ToInt32(txt_soluotlap.Text));
            label6.Text = soluotlatp.ToString();
            label5.Text = realOut[0][1].ToString();
            label6.Text = target[1].ToString();

            /*
            // xem trong so cuoi cung
            for (int i = 0; i < 11; i++)
            {
                textBox1.Text += Math.Round(hds[1][i], 6).ToString() + "   ";
            }*/

            
            for (int i = 0; i < listView1.Items.Count; i++)
                listView1.Items[i].SubItems[25].Text = Math.Round(realOut[i][1], 0).ToString();
            
            System.Windows.Forms.Label[] test = { label1, label2, label3 };
            for (int i = 0; i < 3; i++)
            {
                
                test[i].Text = Math.Round(weightO[1][i], 6).ToString();
            }

            for (int i = 0; i < 4; i++)
            {
                //weightH[1][i] = weightH[1][i] + (Lrate * deltaHidden[1] * train[0][i]);
                //MessageBox.Show("Trong so tang an sau khi cap nhat " + weightH[1][i].ToString());
            }

            for (int i = 0; i < 4; i++)
            {
                //weightH[2][i] = weightH[2][i] + (Lrate * deltaHidden[2] * train[0][i]);
                //MessageBox.Show("Trong so node an thu 2 sau khi cap nhat " + weightH[2][i].ToString());
            }
            
            //Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            //them cot
            /*listView1.Columns.Add("x0", 50);
            listView1.Columns.Add("x1", 50);
            listView1.Columns.Add("x2", 50);
            listView1.Columns.Add("x3", 50);
            listView1.Columns.Add("x4", 50);
            listView1.Columns.Add("x5", 50);
            listView1.Columns.Add("x6", 50);
            listView1.Columns.Add("x7", 50);
            listView1.Columns.Add("x8", 50);
            listView1.Columns.Add("x9", 50);
            listView1.Columns.Add("x10", 50);
            listView1.Columns.Add("x11", 50);
            listView1.Columns.Add("x12", 50);
            listView1.Columns.Add("x13", 50);
            listView1.Columns.Add("x14", 50);
            listView1.Columns.Add("x15", 50);
            listView1.Columns.Add("x16", 50);
            listView1.Columns.Add("x17", 50);
            listView1.Columns.Add("x18", 50);
            listView1.Columns.Add("x19", 50);
            listView1.Columns.Add("x20", 50);
            listView1.Columns.Add("x21", 50);
            listView1.Columns.Add("x22", 50);
            listView1.Columns.Add("x23", 50);
            listView1.Columns.Add("x24", 50);
            listView1.Columns.Add("tong", 50);
            listView1.Columns.Add("tai/xiu", 50);
            listView1.Columns.Add("dudoan", 50);*/

            for (int i = 1; i < 10; i++)
            {
                string title = "x" + i.ToString();
                listView1.Columns.Add(title, 50);
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
             
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            //prepare open file dialog to only search for excel file (had trouble setting this in design view)
            this.openFileDialog1.FileName = "*.xls";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //here is the call to open a workbook in excel
                //it uses most of the default values (except for the read only which we set to true)
                //Excel.Workbook theWorkbook = ExcelObj.Workbooks.Open(openFileDialog1.FileName, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true);
                Test = ExcelObj.Workbooks.Open(openFileDialog1.FileName, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true);

                //get the collection of sheet in the workbook
                //Excel.Sheets sheets = theWorkbook.Worksheets;
                Excel.Sheets sheets = Test.Worksheets;

                //get the first and only worksheet from the collection of worksheets
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);

                //loop through 10 rows of the spreadsheet and place each row in the list vies

                for (int i = 1; i <= 110; i++)
                {
                    Excel.Range range = worksheet.get_Range("A" + i.ToString(), "AA" + i.ToString());
                    System.Array myvalues = (System.Array)range.Cells.Value;
                    strArray = ConvertToStringArray(myvalues);
                    listView1.Items.Add(new ListViewItem(strArray));
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
            if (Test != null)
                Test.Close();
        }

        //load du lieu de test
        private void button1_Click_1(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            //prepare open file dialog to only search for excel file (had trouble setting this in design view)
            this.openFileDialog1.FileName = "*.xls";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //here is the call to open a workbook in excel
                //it uses most of the default values (except for the read only which we set to true)
                //Excel.Workbook theWorkbook = ExcelObj.Workbooks.Open(openFileDialog1.FileName, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true);
                Test = ExcelObj.Workbooks.Open(openFileDialog1.FileName, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true);

                //get the collection of sheet in the workbook
                //Excel.Sheets sheets = theWorkbook.Worksheets;
                Excel.Sheets sheets = Test.Worksheets;

                //get the first and only worksheet from the collection of worksheets
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);

                //loop through 10 rows of the spreadsheet and place each row in the list vies
                for (int i = 1; i <= 10; i++)
                {
                    Excel.Range range = worksheet.get_Range("A" + i.ToString(), "AA" + i.ToString());
                    System.Array myvalues = (System.Array)range.Cells.Value;
                    strArray = ConvertToStringArray(myvalues);
                    listView1.Items.Add(new ListViewItem(strArray));
                }
            }
        }

        private void btn_test_Click(object sender, EventArgs e)
        {

            
            //dua du lieu vao mang
            double[][] datatest = new double[listView1.Items.Count][];
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                datatest[i] = new double[25];

                for (int j = 0; j < 25; j++)
                {
                    datatest[i][j] = Double.Parse(listView1.Items[i].SubItems[j].Text);
                }
            }

            //target
            double[] target = { 0 };
            Array.Resize(ref target, listView1.Items.Count + 1);

            for (int i = 1; i < target.Length; i++)
                target[i] = double.Parse(listView1.Items[i - 1].SubItems[26].Text);
            double[] netout = new double[11];
            netout[0] = 1; //tham so cho w0
            double[][] realOut = new double[listView1.Items.Count][];
            for (int i = 0; i < listView1.Items.Count; i++)
                realOut[i] = new double[2];


            DelOut dellHidden = new DelOut(NetInputH);
            DelOut delOut = new DelOut(NetInputO);

            for (int k = 0; k < datatest.Length; k++)
            {
                //tinh toan netout cho tang an
                for (int i = 1; i < hds.Length; i++)
                    netout[i] = Sigmod(dellHidden(hds[i], datatest[k]));
                realOut[k][1] = Sigmod(delOut(ops[1], netout));
            }

            for (int i = 0; i < listView1.Items.Count; i++)
                listView1.Items[i].SubItems[25].Text = Math.Round(realOut[i][1], 0).ToString();
        }

        private void btnKmeans_Click(object sender, EventArgs e)
        {
            //double[][] rawData = new double[20][];
            rawData[0] = new double[] { 65.0, 220.0, 4.0 };
            rawData[1] = new double[] { 73.0, 160.0, 2.9 };
            rawData[2] = new double[] { 59.0, 110.0, 1.0 };
            rawData[3] = new double[] { 61.0, 120.0, 8.1 };
            rawData[4] = new double[] { 75.0, 150.0, 7.2 };
            rawData[5] = new double[] { 67.0, 240.0, 6.3 };
            rawData[6] = new double[] { 68.0, 230.0, 5.4 };
            rawData[7] = new double[] { 70.0, 220.0, 1.8 };
            rawData[8] = new double[] { 62.0, 130.0, 2.7 };
            rawData[9] = new double[] { 66.0, 210.0, 3.6 };
            rawData[10] = new double[] { 77.0, 190.0, 4.5 };
            rawData[11] = new double[] { 75.0, 180.0, 5.0 };
            rawData[12] = new double[] { 74.0, 170.0, 9.1 };
            rawData[13] = new double[] { 70.0, 210.0, 1.0 };
            rawData[14] = new double[] { 61.0, 110.0, 2.9 };
            rawData[15] = new double[] { 58.0, 100.0, 2.6 };
            rawData[16] = new double[] { 66.0, 230.0, 5.3 };
            rawData[17] = new double[] { 59.0, 120.0, 4.2 };
            rawData[18] = new double[] { 68.0, 210.0, 1.3 };
            rawData[19] = new double[] { 61.0, 130.0, 5.0 };
            //double[][] arrMeans = k_means.MeansCal(rawData, 3);
        }

        private void LoadCSV_Click(object sender, EventArgs e)
        {
            using(var sr = new StreamReader(@"trainTest.csv"))
            {
                var reader = new CsvReader(sr);
                IEnumerable<input> records = reader.GetRecords<input>();
                //List<double> values = new List<double>();
                List<double[]> values = new List<double[]>();
                foreach (input rec in records)
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
                    values.Add(value.ToArray());
                }
                int[] arrCluster;
                List<double[]> cluster_1 = new List<double[]>();
                List<double[]> cluster_2 = new List<double[]>();
                double[][] arrMeans = k_means.MeansCal(values.ToArray(), 2, out arrCluster);
                for (int i = 0; i < arrCluster.Length; i++)
                {
                    if (arrCluster[i] == 0)
                        cluster_1.Add(values.ToArray()[i]);
                    else
                        cluster_2.Add(values.ToArray()[i]);
                }
                string[][] strValues = new string[values.ToArray().Length][];
                trainData = new double[values.ToArray().Length][];
                for (int i = 0; i < strValues.Length; i++)
                {
                    //listView1.Items.Add(new ListViewItem(values.ToArray()[i]));
                    trainData[i] = values.ToArray()[i];
                    //strValues[i] = new string[values.ToArray()[1].Length];
                    strValues[i] = new string[trainData[i].Length];
                    for (int j =0;j<strValues[i].Length;j++)
                    {
                        //strValues[i][j] = values.ToArray()[i][j].ToString();
                        strValues[i][j] = trainData[i][j].ToString();
                    }
                    listView1.Items.Add(new ListViewItem(strValues[i]));
                }
                MessageBox.Show("load traing data finished");
            }
        }

        private void btnTrainNew_Click(object sender, EventArgs e)
        {
            double[][] weight_init_H = new double[10][];
            Random random = new Random();
            for (int i = 0; i < weight_init_H.Length; i++)
            {
                weight_init_H[i] = new double[18];
                for (int j = 0; j < weight_init_H[i].Length; j++)
                {
                    if (i == 0)
                        weight_init_H[i][j] = 1;
                    else
                    {
                        double rn = random.NextDouble();
                        weight_init_H[i][j] = random.NextDouble() * (1.5 - (-1.5)) + (-1.5);
                    }
                }
            }
            MessageBox.Show("init weight finished");
        }

        private void btnDelegate_Click(object sender, EventArgs e)
        {
            TangQuaDelegate test = new TangQuaDelegate(tangQua);
            oNha("", test);
        }

        
    }
}
