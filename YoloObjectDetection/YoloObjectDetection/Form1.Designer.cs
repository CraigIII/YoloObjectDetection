namespace ONNXObjectDetection
{
    partial class Form1
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
            btnSelect = new Button();
            picOriginal = new PictureBox();
            btnDetect = new Button();
            picDetected = new PictureBox();
            openFileDialog1 = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)picOriginal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picDetected).BeginInit();
            SuspendLayout();
            // 
            // btnSelect
            // 
            btnSelect.Location = new Point(29, 29);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(94, 29);
            btnSelect.TabIndex = 0;
            btnSelect.Text = "選取圖片";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += btnSelect_Click;
            // 
            // picOriginal
            // 
            picOriginal.BorderStyle = BorderStyle.Fixed3D;
            picOriginal.Location = new Point(29, 77);
            picOriginal.Name = "picOriginal";
            picOriginal.Size = new Size(567, 534);
            picOriginal.SizeMode = PictureBoxSizeMode.Zoom;
            picOriginal.TabIndex = 1;
            picOriginal.TabStop = false;
            // 
            // btnDetect
            // 
            btnDetect.Location = new Point(618, 29);
            btnDetect.Name = "btnDetect";
            btnDetect.Size = new Size(94, 29);
            btnDetect.TabIndex = 2;
            btnDetect.Text = "偵測";
            btnDetect.UseVisualStyleBackColor = true;
            btnDetect.Click += btnDetect_Click;
            // 
            // picDetected
            // 
            picDetected.BorderStyle = BorderStyle.Fixed3D;
            picDetected.Location = new Point(618, 77);
            picDetected.Name = "picDetected";
            picDetected.Size = new Size(566, 553);
            picDetected.SizeMode = PictureBoxSizeMode.Zoom;
            picDetected.TabIndex = 3;
            picDetected.TabStop = false;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "圖形檔案(*.jpg;*.png)|*.jpg;*.png";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1196, 642);
            Controls.Add(picDetected);
            Controls.Add(btnDetect);
            Controls.Add(picOriginal);
            Controls.Add(btnSelect);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)picOriginal).EndInit();
            ((System.ComponentModel.ISupportInitialize)picDetected).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnSelect;
        private PictureBox picOriginal;
        private Button btnDetect;
        private PictureBox picDetected;
        private OpenFileDialog openFileDialog1;
    }
}