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
            this.btnCluster_2.Location = new System.Drawing.Point(28, 145);
            this.btnCluster_2.Name = "btnCluster_2";
            this.btnCluster_2.Size = new System.Drawing.Size(89, 23);
            this.btnCluster_2.TabIndex = 7;
            this.btnCluster_2.Text = "Open Cluster 2\r\n";
            this.btnCluster_2.UseVisualStyleBackColor = true;
            this.btnCluster_2.Click += new System.EventHandler(this.btnCluster_2_Click);
            // 
            // FormSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
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
    }
}

