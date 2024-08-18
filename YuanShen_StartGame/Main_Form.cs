using System;
using System.Drawing;
using System.Windows.Forms;
using WuKong_YuanShen.Properties;

namespace WuKong_YuanShen
{
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();
        }

        //窗体移动相关的变量声明及赋值
        private bool bool_Moving = false;
        private Point oldMousePosition;

        //窗口加载
        private void Main_Form_Load(object sender, EventArgs e)
        {
            pictureBox_Btn_Minimized.MouseMove += new MouseEventHandler(pictrueBox_MouseMove);
            pictureBox_Btn_Quit.MouseMove += new MouseEventHandler(pictrueBox_MouseMove);
            pictureBox_GameBg.Controls.Add(pictureBox_Btn_Minimized);
            pictureBox_GameBg.Controls.Add(pictureBox_Btn_Quit);
            pictureBox_GameBg.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_Btn_Minimized.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_Btn_Quit.SizeMode = PictureBoxSizeMode.StretchImage;
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.yuanshen_wav);
            player.Play();
        }

        //事件：PictureBox点击事件
        private void pictrueBox_Click(object sender, EventArgs e)
        {
            switch (((PictureBox)sender).Name.ToString())
            {
                case "pictureBox_Btn_Quit":
                    Close();
                    break;
                case "pictureBox_Btn_Minimized":
                    if (WindowState != FormWindowState.Minimized)
                    {
                        WindowState = FormWindowState.Minimized;
                    }
                    break;
                default:
                    break;
            }
        }

        //事件：PictureBox鼠标经过事件
        private void pictrueBox_MouseMove(object sender, EventArgs e)
        {
            switch (((PictureBox)sender).Name.ToString())
            {
                case "pictureBox_Btn_Quit":
                    pictureBox_Btn_Quit.Cursor = Cursors.Hand;
                    break;
                case "pictureBox_Btn_Minimized":
                    pictureBox_Btn_Minimized.Cursor = Cursors.Hand;
                    break;
                default:
                    break;
            }
        }

        //事件：PictureBox鼠标离开事件
        private void pictrueBox_MouseLeave(object sender, EventArgs e)
        {
            switch (((PictureBox)sender).Name.ToString())
            {
                case "pictureBox_Btn_Quit":
                    pictureBox_Btn_Quit.Image = Resources.quit_btn;
                    pictureBox_Btn_Quit.Cursor = Cursors.Default;
                    break;
                case "pictureBox_Btn_Minimized":
                    pictureBox_Btn_Minimized.Image = Resources.minimized_btn;
                    pictureBox_Btn_Minimized.Cursor = Cursors.Default;
                    break;
                default:
                    break;
            }
        }

        //事件：鼠标左键按住窗口上方
        private void pictureBox_GameBg_MouseDown(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                return;
            }
            pictureBox_GameBg.Cursor = Cursors.NoMove2D;
            oldMousePosition = e.Location;
            bool_Moving = true;
        }

        //事件：鼠标松开窗口上方
        private void pictureBox_GameBg_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox_GameBg.Cursor = Cursors.Default;
            bool_Moving = false;
        }

        //事件：鼠标左键拖动窗口上方
        private void pictureBox_GameBg_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && bool_Moving)
            {
                Point newPosition = new Point(e.Location.X - oldMousePosition.X, e.Location.Y - oldMousePosition.Y);
                Location += new Size(newPosition);
            }
        }

        //事件：窗口关闭前的确认
        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("您确认退出吗？\nAre you sure to quit?", "温馨提示 Tips", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Dispose();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
