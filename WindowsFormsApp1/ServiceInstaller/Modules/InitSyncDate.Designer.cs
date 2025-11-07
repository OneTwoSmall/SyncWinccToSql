namespace ServiceInstaller.Modules
{
    partial class InitSyncDate
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
            this.endDataT = new System.Windows.Forms.DateTimePicker();
            this.startDateT = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // endDataT
            // 
            this.endDataT.CustomFormat = "yyyy-MM-dd HH:mm";
            this.endDataT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDataT.Location = new System.Drawing.Point(255, 36);
            this.endDataT.Name = "endDataT";
            this.endDataT.Size = new System.Drawing.Size(200, 25);
            this.endDataT.TabIndex = 0;
            // 
            // startDateT
            // 
            this.startDateT.CustomFormat = "yyyy-MM-dd HH:mm";
            this.startDateT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDateT.Location = new System.Drawing.Point(25, 36);
            this.startDateT.Name = "startDateT";
            this.startDateT.Size = new System.Drawing.Size(200, 25);
            this.startDateT.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(102, 102);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 41);
            this.button1.TabIndex = 2;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(255, 102);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 41);
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // InitSyncDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 185);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.startDateT);
            this.Controls.Add(this.endDataT);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InitSyncDate";
            this.Text = "同步历史数据";
            this.Load += new System.EventHandler(this.InitSyncDate_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker endDataT;
        private System.Windows.Forms.DateTimePicker startDateT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}