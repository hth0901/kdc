namespace NeralSample_2
{
    partial class FormSample
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTest = new System.Windows.Forms.Button();
            this.btnLoadRawData = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnTrain = new System.Windows.Forms.Button();
            this.btnOpenJson = new System.Windows.Forms.Button();
            this.btnCluster_1 = new System.Windows.Forms.Button();
            this.btnCluster_2 = new System.Windows.Forms.Button();
            this.btnCalOutput = new System.Windows.Forms.Button();
            this.btnOpenNet_1 = new System.Windows.Forms.Button();
            this.btnOpenNet_2 = new System.Windows.Forms.Button();
            this.btnCluster_3 = new System.Windows.Forms.Button();
            this.btnOpenNet_3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(197, 12);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnLoadRawData
            // 
            this.btnLoadRawData.Location = new System.Drawing.Point(28, 22);
            this.btnLoadRawData.Name = "btnLoadRawData";
            this.btnLoadRawData.Size = new System.Drawing.Size(75, 23);
            this.btnLoadRawData.TabIndex = 1;
            this.btnLoadRawData.Text = "Load Raw Data";
            this.btnLoadRawData.UseVisualStyleBackColor = true;
            this.btnLoadRawData.Click += new System.EventHandler(this.btnLoadRawData_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnTrain
            // 
            this.btnTrain.Location = new System.Drawing.Point(28, 63);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(75, 23);
            this.btnTrain.TabIndex = 3;
            this.btnTrain.Text = "Train";
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // btnOpenJson
            // 
            this.btnOpenJson.Location = new System.Drawing.Point(197, 206);
            this.btnOpenJson.Name = "btnOpenJson";
            this.btnOpenJson.Size = new System.Drawing.Size(75, 23);
            this.btnOpenJson.TabIndex = 5;
            this.btnOpenJson.Text = "OpenJson";
            this.btnOpenJson.UseVisualStyleBackColor = true;
            this.btnOpenJson.Click += new System.EventHandler(this.btnOpenJson_Click);
            // 
            // btnCluster_1
            // 
            this.btnCluster_1.Location = new System.Drawing.Point(28, 103);
            this.btnCluster_1.Name = "btnCluster_1";
            this.btnCluster_1.Size = new System.Drawing.Size(89, 23);
            this.btnCluster_1.TabIndex = 6;
            this.btnCluster_1.Text = "Open Cluster 1";
            this.btnCluster_1.UseVisualStyleBackColor = true;
            this.btnCluster_1.Click += new System.EventHandler(this.btnCluster_1_Click);
            // 
            // btnCluster_2
            // 
            this.btnCluster_2.Location = new System.Drawing.Point(28, 139);
            this.btnCluster_2.Name = "btnCluster_2";
            this.btnCluster_2.Size = new System.Drawing.Size(89, 23);
            this.btnCluster_2.TabIndex = 7;
            this.btnCluster_2.Text = "Open Cluster 2\r\n";
            this.btnCluster_2.UseVisualStyleBackColor = true;
            this.btnCluster_2.Click += new System.EventHandler(this.btnCluster_2_Click);
            // 
            // btnCalOutput
            // 
            this.btnCalOutput.Location = new System.Drawing.Point(28, 205);
            this.btnCalOutput.Name = "btnCalOutput";
            this.btnCalOutput.Size = new System.Drawing.Size(75, 23);
            this.btnCalOutput.TabIndex = 8;
            this.btnCalOutput.Text = "Cal Output";
            this.btnCalOutput.UseVisualStyleBackColor = true;
            this.btnCalOutput.Click += new System.EventHandler(this.btnCalOutput_Click);
            // 
            // btnOpenNet_1
            // 
            this.btnOpenNet_1.Location = new System.Drawing.Point(142, 103);
            this.btnOpenNet_1.Name = "btnOpenNet_1";
            this.btnOpenNet_1.Size = new System.Drawing.Size(90, 23);
            this.btnOpenNet_1.TabIndex = 9;
            this.btnOpenNet_1.Text = "Open Neural 1";
            this.btnOpenNet_1.UseVisualStyleBackColor = true;
            this.btnOpenNet_1.Click += new System.EventHandler(this.btnOpenNet_1_Click);
            // 
            // btnOpenNet_2
            // 
            this.btnOpenNet_2.Location = new System.Drawing.Point(142, 139);
            this.btnOpenNet_2.Name = "btnOpenNet_2";
            this.btnOpenNet_2.Size = new System.Drawing.Size(90, 23);
            this.btnOpenNet_2.TabIndex = 10;
            this.btnOpenNet_2.Text = "Open Neural 2";
            this.btnOpenNet_2.UseVisualStyleBackColor = true;
            this.btnOpenNet_2.Click += new System.EventHandler(this.btnOpenNet_2_Click);
            // 
            // btnCluster_3
            // 
            this.btnCluster_3.Location = new System.Drawing.Point(28, 176);
            this.btnCluster_3.Name = "btnCluster_3";
            this.btnCluster_3.Size = new System.Drawing.Size(89, 23);
            this.btnCluster_3.TabIndex = 11;
            this.btnCluster_3.Text = "Open Cluster 3";
            this.btnCluster_3.UseVisualStyleBackColor = true;
            this.btnCluster_3.Click += new System.EventHandler(this.btnCluster_3_Click);
            // 
            // btnOpenNet_3
            // 
            this.btnOpenNet_3.Location = new System.Drawing.Point(142, 176);
            this.btnOpenNet_3.Name = "btnOpenNet_3";
            this.btnOpenNet_3.Size = new System.Drawing.Size(90, 23);
            this.btnOpenNet_3.TabIndex = 12;
            this.btnOpenNet_3.Text = "Open Neural 3";
            this.btnOpenNet_3.UseVisualStyleBackColor = true;
            this.btnOpenNet_3.Click += new System.EventHandler(this.btnOpenNet_3_Click);
            // 
            // FormSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnOpenNet_3);
            this.Controls.Add(this.btnCluster_3);
            this.Controls.Add(this.btnOpenNet_2);
            this.Controls.Add(this.btnOpenNet_1);
            this.Controls.Add(this.btnCalOutput);
            this.Controls.Add(this.btnCluster_2);
            this.Controls.Add(this.btnCluster_1);
            this.Controls.Add(this.btnOpenJson);
            this.Controls.Add(this.btnTrain);
            this.Controls.Add(this.btnLoadRawData);
            this.Controls.Add(this.btnTest);
            this.Name = "FormSample";
            this.Text = "Form Sample";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnLoadRawData;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.Button btnOpenJson;
        private System.Windows.Forms.Button btnCluster_1;
        private System.Windows.Forms.Button btnCluster_2;
        private System.Windows.Forms.Button btnCalOutput;
        private System.Windows.Forms.Button btnOpenNet_1;
        private System.Windows.Forms.Button btnOpenNet_2;
        private System.Windows.Forms.Button btnCluster_3;
        private System.Windows.Forms.Button btnOpenNet_3;
    }
}

