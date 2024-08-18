namespace WuKong_YuanShen
{
    partial class Main_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
            this.pictureBox_Btn_Quit = new System.Windows.Forms.PictureBox();
            this.pictureBox_Btn_Minimized = new System.Windows.Forms.PictureBox();
            this.pictureBox_GameBg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Btn_Quit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Btn_Minimized)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_GameBg)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Btn_Quit
            // 
            this.pictureBox_Btn_Quit.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Btn_Quit.Image = global::WuKong_YuanShen.Properties.Resources.quit_btn;
            this.pictureBox_Btn_Quit.Location = new System.Drawing.Point(1220, 26);
            this.pictureBox_Btn_Quit.Name = "pictureBox_Btn_Quit";
            this.pictureBox_Btn_Quit.Size = new System.Drawing.Size(28, 28);
            this.pictureBox_Btn_Quit.TabIndex = 3;
            this.pictureBox_Btn_Quit.TabStop = false;
            this.pictureBox_Btn_Quit.Click += new System.EventHandler(this.pictrueBox_Click);
            this.pictureBox_Btn_Quit.MouseLeave += new System.EventHandler(this.pictrueBox_MouseLeave);
            // 
            // pictureBox_Btn_Minimized
            // 
            this.pictureBox_Btn_Minimized.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Btn_Minimized.Image = global::WuKong_YuanShen.Properties.Resources.minimized_btn;
            this.pictureBox_Btn_Minimized.Location = new System.Drawing.Point(1177, 26);
            this.pictureBox_Btn_Minimized.Name = "pictureBox_Btn_Minimized";
            this.pictureBox_Btn_Minimized.Size = new System.Drawing.Size(28, 28);
            this.pictureBox_Btn_Minimized.TabIndex = 2;
            this.pictureBox_Btn_Minimized.TabStop = false;
            this.pictureBox_Btn_Minimized.Click += new System.EventHandler(this.pictrueBox_Click);
            this.pictureBox_Btn_Minimized.MouseLeave += new System.EventHandler(this.pictrueBox_MouseLeave);
            // 
            // pictureBox_GameBg
            // 
            this.pictureBox_GameBg.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_GameBg.Image = global::WuKong_YuanShen.Properties.Resources.yuanshen;
            this.pictureBox_GameBg.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_GameBg.Name = "pictureBox_GameBg";
            this.pictureBox_GameBg.Size = new System.Drawing.Size(1280, 720);
            this.pictureBox_GameBg.TabIndex = 0;
            this.pictureBox_GameBg.TabStop = false;
            this.pictureBox_GameBg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_GameBg_MouseDown);
            this.pictureBox_GameBg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_GameBg_MouseMove);
            this.pictureBox_GameBg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_GameBg_MouseUp);
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.pictureBox_Btn_Quit);
            this.Controls.Add(this.pictureBox_Btn_Minimized);
            this.Controls.Add(this.pictureBox_GameBg);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Main_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "猿神，启动！";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_Form_FormClosing);
            this.Load += new System.EventHandler(this.Main_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Btn_Quit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Btn_Minimized)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_GameBg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_GameBg;
        private System.Windows.Forms.PictureBox pictureBox_Btn_Minimized;
        private System.Windows.Forms.PictureBox pictureBox_Btn_Quit;
    }
}

