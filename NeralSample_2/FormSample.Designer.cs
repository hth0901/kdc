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
            this.btnWriteFile = new System.Windows.Forms.Button();
            this.btnTrain = new System.Windows.Forms.Button();
            this.btnJson = new System.Windows.Forms.Button();
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
            // btnWriteFile
            // 
            this.btnWriteFile.Location = new System.Drawing.Point(53, 98);
            this.btnWriteFile.Name = "btnWriteFile";
            this.btnWriteFile.Size = new System.Drawing.Size(75, 23);
            this.btnWriteFile.TabIndex = 2;
            this.btnWriteFile.Text = "Write";
            this.btnWriteFile.UseVisualStyleBackColor = true;
            this.btnWriteFile.Click += new System.EventHandler(this.btnWriteFile_Click);
            // 
            // btnTrain
            // 
            this.btnTrain.Location = new System.Drawing.Point(53, 160);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(75, 23);
            this.btnTrain.TabIndex = 3;
            this.btnTrain.Text = "Train";
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // btnJson
            // 
            this.btnJson.Location = new System.Drawing.Point(179, 144);
            this.btnJson.Name = "btnJson";
            this.btnJson.Size = new System.Drawing.Size(75, 23);
            this.btnJson.TabIndex = 4;
            this.btnJson.Text = "JSON";
            this.btnJson.UseVisualStyleBackColor = true;
            this.btnJson.Click += new System.EventHandler(this.btnJson_Click);
            // 
            // FormSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnJson);
            this.Controls.Add(this.btnTrain);
            this.Controls.Add(this.btnWriteFile);
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
        private System.Windows.Forms.Button btnWriteFile;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.Button btnJson;
    }
}

