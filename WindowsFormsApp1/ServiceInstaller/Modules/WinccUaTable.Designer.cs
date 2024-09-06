namespace ServiceInstaller.Modules
{
    partial class WinccUaTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinccUaTable));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gB_UA_Main = new System.Windows.Forms.GroupBox();
            this.gB_UA_Son = new System.Windows.Forms.GroupBox();
            this.dgv_UA_Main = new System.Windows.Forms.DataGridView();
            this.dgv_UA_Son = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gB_UA_Main.SuspendLayout();
            this.gB_UA_Son.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_UA_Main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_UA_Son)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1074, 31);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(50, 28);
            this.toolStripButton4.Text = "刷新";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(50, 28);
            this.toolStripButton2.Text = "导入";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 31);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gB_UA_Main);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gB_UA_Son);
            this.splitContainer1.Size = new System.Drawing.Size(1074, 639);
            this.splitContainer1.SplitterDistance = 275;
            this.splitContainer1.TabIndex = 2;
            // 
            // gB_UA_Main
            // 
            this.gB_UA_Main.Controls.Add(this.dgv_UA_Main);
            this.gB_UA_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gB_UA_Main.Location = new System.Drawing.Point(0, 0);
            this.gB_UA_Main.Name = "gB_UA_Main";
            this.gB_UA_Main.Size = new System.Drawing.Size(1074, 275);
            this.gB_UA_Main.TabIndex = 0;
            this.gB_UA_Main.TabStop = false;
            this.gB_UA_Main.Text = "用户归档主表";
            // 
            // gB_UA_Son
            // 
            this.gB_UA_Son.Controls.Add(this.dgv_UA_Son);
            this.gB_UA_Son.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gB_UA_Son.Location = new System.Drawing.Point(0, 0);
            this.gB_UA_Son.Name = "gB_UA_Son";
            this.gB_UA_Son.Size = new System.Drawing.Size(1074, 360);
            this.gB_UA_Son.TabIndex = 1;
            this.gB_UA_Son.TabStop = false;
            this.gB_UA_Son.Text = "用户归档子表";
            // 
            // dgv_UA_Main
            // 
            this.dgv_UA_Main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_UA_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_UA_Main.Location = new System.Drawing.Point(3, 24);
            this.dgv_UA_Main.Name = "dgv_UA_Main";
            this.dgv_UA_Main.RowTemplate.Height = 30;
            this.dgv_UA_Main.Size = new System.Drawing.Size(1068, 248);
            this.dgv_UA_Main.TabIndex = 0;
            // 
            // dgv_UA_Son
            // 
            this.dgv_UA_Son.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_UA_Son.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_UA_Son.Location = new System.Drawing.Point(3, 24);
            this.dgv_UA_Son.Name = "dgv_UA_Son";
            this.dgv_UA_Son.RowTemplate.Height = 30;
            this.dgv_UA_Son.Size = new System.Drawing.Size(1068, 333);
            this.dgv_UA_Son.TabIndex = 1;
            // 
            // WinccUaTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 670);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "WinccUaTable";
            this.Text = "用户归档设置";
            this.Load += new System.EventHandler(this.WinccUaTable_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gB_UA_Main.ResumeLayout(false);
            this.gB_UA_Son.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_UA_Main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_UA_Son)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gB_UA_Main;
        private System.Windows.Forms.GroupBox gB_UA_Son;
        private System.Windows.Forms.DataGridView dgv_UA_Main;
        private System.Windows.Forms.DataGridView dgv_UA_Son;
    }
}