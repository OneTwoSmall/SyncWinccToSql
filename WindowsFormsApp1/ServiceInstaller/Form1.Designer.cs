namespace ServiceInstaller
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Lb_Version = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lb_ServiceState = new System.Windows.Forms.ToolStripStatusLabel();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_unistall = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txt_Info = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.btn_StartSync = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.btn_ManuSync = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripExit = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Lb_Version,
            this.toolStripStatusLabel1,
            this.lb_ServiceState});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 576);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(727, 31);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Lb_Version
            // 
            this.Lb_Version.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Lb_Version.Margin = new System.Windows.Forms.Padding(8, 3, 0, 2);
            this.Lb_Version.Name = "Lb_Version";
            this.Lb_Version.Size = new System.Drawing.Size(92, 26);
            this.Lb_Version.Text = "程序版本";
            this.Lb_Version.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(8, 3, 0, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(98, 26);
            this.toolStripStatusLabel1.Text = "服务状态:";
            // 
            // lb_ServiceState
            // 
            this.lb_ServiceState.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lb_ServiceState.Name = "lb_ServiceState";
            this.lb_ServiceState.Size = new System.Drawing.Size(87, 25);
            this.lb_ServiceState.Text = "初始化...";
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(12, 126);
            this.btn_stop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(100, 28);
            this.btn_stop.TabIndex = 12;
            this.btn_stop.Text = "停止服务";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_unistall
            // 
            this.btn_unistall.Location = new System.Drawing.Point(12, 229);
            this.btn_unistall.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_unistall.Name = "btn_unistall";
            this.btn_unistall.Size = new System.Drawing.Size(100, 28);
            this.btn_unistall.TabIndex = 10;
            this.btn_unistall.Text = "卸载服务";
            this.btn_unistall.UseVisualStyleBackColor = true;
            this.btn_unistall.Visible = false;
            this.btn_unistall.Click += new System.EventHandler(this.btn_unistall_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(596, 110);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 57);
            this.button1.TabIndex = 15;
            this.button1.Text = "初始化过程归档";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(596, 31);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 60);
            this.button2.TabIndex = 14;
            this.button2.Text = "初始化过程变量";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(600, 186);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(86, 57);
            this.button3.TabIndex = 16;
            this.button3.Text = "设定同步时间";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txt_Info
            // 
            this.txt_Info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Info.Location = new System.Drawing.Point(132, 31);
            this.txt_Info.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Info.Multiline = true;
            this.txt_Info.Name = "txt_Info";
            this.txt_Info.Size = new System.Drawing.Size(439, 545);
            this.txt_Info.TabIndex = 17;
            this.txt_Info.Text = "安装服务前请先初始化。。。";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 313);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 60);
            this.button5.TabIndex = 19;
            this.button5.Text = "编辑配置文档";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(600, 335);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(86, 57);
            this.button6.TabIndex = 20;
            this.button6.Text = "更新程序";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // btn_StartSync
            // 
            this.btn_StartSync.Location = new System.Drawing.Point(12, 47);
            this.btn_StartSync.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_StartSync.Name = "btn_StartSync";
            this.btn_StartSync.Size = new System.Drawing.Size(100, 28);
            this.btn_StartSync.TabIndex = 21;
            this.btn_StartSync.Text = "启动任务";
            this.btn_StartSync.UseVisualStyleBackColor = true;
            this.btn_StartSync.Click += new System.EventHandler(this.btn_StartSync_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Wincc数据同步服务";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // btn_ManuSync
            // 
            this.btn_ManuSync.Location = new System.Drawing.Point(13, 195);
            this.btn_ManuSync.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_ManuSync.Name = "btn_ManuSync";
            this.btn_ManuSync.Size = new System.Drawing.Size(100, 28);
            this.btn_ManuSync.TabIndex = 22;
            this.btn_ManuSync.Text = "手工执行";
            this.btn_ManuSync.UseVisualStyleBackColor = true;
            this.btn_ManuSync.Click += new System.EventHandler(this.btn_ManuSync_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(727, 27);
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripExit
            // 
            this.toolStripExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripExit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripExit.Image")));
            this.toolStripExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripExit.Name = "toolStripExit";
            this.toolStripExit.Size = new System.Drawing.Size(73, 24);
            this.toolStripExit.Text = "退出程序";
            this.toolStripExit.Click += new System.EventHandler(this.toolStripExit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 607);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btn_ManuSync);
            this.Controls.Add(this.btn_StartSync);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.txt_Info);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_unistall);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "WINCC数据收集服务";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lb_ServiceState;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_unistall;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txt_Info;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ToolStripStatusLabel Lb_Version;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button btn_StartSync;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button btn_ManuSync;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripExit;
    }
}

