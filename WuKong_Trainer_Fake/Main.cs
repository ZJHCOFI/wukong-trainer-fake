using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WuKongXiuGaiQi.Properties;
using Gma.System.MouseKeyHook;
using System.IO;
using System.Linq;

namespace WuKongXiuGaiQi
{
    public partial class Main : Form
    {
        // 编写时间：2024.08.18
        // 更新时间：2024.08.18 10:00
        // Edit by ZJHCOFI
        // 博客Blog：https://zjhcofi.com
        // Github：https://github.com/zjhcofi
        // 功能：《黑神话：悟空》装13专用
        // 外置插件：MouseKeyHook 5.6.0（下载方法：“Visual Studio”-“项目”-“管理NuGet程序包”）
        // 开源协议：BSD 3-Clause “New” or “Revised” License (https://choosealicense.com/licenses/bsd-3-clause/)
        // 后续更新或漏洞修补通告页面：https://github.com/zjhcofi
        // =====更新日志=====
        // 2024.08.18 10:00
        // 第一个版本发布
        // ==================

        public Main()
        {
            InitializeComponent();
        }

        //窗体移动相关的变量声明及赋值
        private bool bool_Moving = false;
        private Point oldMousePosition;

        //判断声明及赋值
        private bool bool_F1 = false;
        private bool bool_F2 = false;
        private bool bool_F3 = false;
        private bool bool_F4 = false;
        private bool bool_F5 = false;
        private bool bool_F6 = false;
        private bool bool_F7 = false;
        private bool bool_GameStatus = false;

        //键盘监听变量声明
        private IKeyboardMouseEvents m_GlobalHook;

        //Timer定时器变量声明及赋值
        int int_timerBtn = 0;
        int int_timerLabel = 0;

        //事件：窗体加载
        private void Main_Load(object sender, EventArgs e)
        {
            //事件委托
            pictureBox_Btn_Quit.MouseMove += new MouseEventHandler(pictrueBox_MouseMove);
            pictureBox_Btn_Minimized.MouseMove += new MouseEventHandler(pictrueBox_MouseMove);
            pictureBox_WuKong_Monkey.MouseMove += new MouseEventHandler(pictrueBox_MouseMove);
            pictureBox_WuKong_Text.MouseMove += new MouseEventHandler(pictrueBox_MouseMove);
            pictureBox_XiuGaiQi_Logo.MouseMove += new MouseEventHandler(pictrueBox_MouseMove);
            pictureBox_xgq_switch_F1.MouseMove += new MouseEventHandler(pictrueBox_MouseMove);
            pictureBox_xgq_switch_F2.MouseMove += new MouseEventHandler(pictrueBox_MouseMove);
            pictureBox_xgq_switch_F3.MouseMove += new MouseEventHandler(pictrueBox_MouseMove);
            pictureBox_xgq_switch_F4.MouseMove += new MouseEventHandler(pictrueBox_MouseMove);
            pictureBox_xgq_switch_F5.MouseMove += new MouseEventHandler(pictrueBox_MouseMove);
            pictureBox_xgq_switch_F6.MouseMove += new MouseEventHandler(pictrueBox_MouseMove);
            pictureBox_xgq_switch_F7.MouseMove += new MouseEventHandler(pictrueBox_MouseMove);
            pictureBox_xgq_switch_F8.MouseMove += new MouseEventHandler(pictrueBox_MouseMove);
            label_xgq_creditor.MouseMove += new MouseEventHandler(label_MouseMove);
            label_xgq_version.MouseMove += new MouseEventHandler(label_MouseMove);
            //图片加载相关
            pictureBox_WuKong_bg.Controls.Add(label_xgq_title_GameName);
            pictureBox_WuKong_bg.Controls.Add(label_xgq_title_XiuGaiQiName);
            pictureBox_WuKong_bg.Controls.Add(label_SplitLine_1);
            pictureBox_WuKong_bg.Controls.Add(label_xgq_list_KuaiJieJian);
            pictureBox_WuKong_bg.Controls.Add(label_xgq_list_GongNengLieBiao);
            pictureBox_WuKong_bg.Controls.Add(label_xgq_list_F1);
            pictureBox_WuKong_bg.Controls.Add(label_xgq_list_F1_Info);
            pictureBox_WuKong_bg.Controls.Add(pictureBox_WuKong_Monkey);
            pictureBox_WuKong_bg.Controls.Add(pictureBox_WuKong_HeiShenHua);
            pictureBox_WuKong_bg.Controls.Add(pictureBox_WuKong_Text);
            pictureBox_WuKong_bg.Controls.Add(pictureBox_XiuGaiQi_Logo);
            pictureBox_WuKong_bg.Controls.Add(pictureBox_xgq_switch_F1);
            pictureBox_Btn_Quit.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_Btn_Minimized.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_WuKong_bg.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_WuKong_Monkey.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_WuKong_Text.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_WuKong_HeiShenHua.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_XiuGaiQi_Logo.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_xgq_switch_F1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_xgq_switch_F2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_xgq_switch_F3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_xgq_switch_F4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_xgq_switch_F5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_xgq_switch_F6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_xgq_switch_F7.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_xgq_switch_F8.SizeMode = PictureBoxSizeMode.StretchImage;
            //鼠标停留初始提示
            toolTip_info.SetToolTip(pictureBox_Btn_Quit, "退出");
            toolTip_info.SetToolTip(pictureBox_Btn_Minimized, "最小化");
            //启动检测游戏进程的线程
            Thread thread_CheckStatusGame = new Thread(CheckStatus_Game);
            thread_CheckStatusGame.Start();
            //键盘HOOK事件创建
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyDown += M_GlobalHook_KeyDown;
        }

        //【重要】事件：监测键盘按下
        private void M_GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            //小键盘事件（废弃）
            //switch (e.KeyValue)
            //{
            //    case (int)Keys.NumPad1:
            //        F1_PressDownOrClick();
            //        break;
            //    case (int)Keys.NumPad2:
            //        F2_PressDownOrClick();
            //        break;
            //    case (int)Keys.NumPad3:
            //        F3_PressDownOrClick();
            //        break;
            //    case (int)Keys.NumPad4:
            //        F4_PressDownOrClick();
            //        break;
            //    case (int)Keys.NumPad5:
            //        F5_PressDownOrClick();
            //        break;
            //    case (int)Keys.NumPad6:
            //        F6_PressDownOrClick();
            //        break;
            //    case (int)Keys.NumPad7:
            //        F7_PressDownOrClick();
            //        break;
            //    case (int)Keys.NumPad8:
            //        F8_PressDownOrClick();
            //        break;
            //    default:
            //        break;
            //}

            //Ctrl+F* 热键事件
            if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.F1)
            {
                F1_PressDownOrClick();
            }
            else if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.F2)
            {
                F2_PressDownOrClick();
            }
            else if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.F3)
            {
                F3_PressDownOrClick();
            }
            else if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.F4)
            {
                F4_PressDownOrClick();
            }
            else if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.F5)
            {
                F5_PressDownOrClick();
            }
            else if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.F6)
            {
                F6_PressDownOrClick();
            }
            else if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.F7)
            {
                F7_PressDownOrClick();
            }
            else if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.F8)
            {
                F8_PressDownOrClick();
            }
        }

        //【重要】线程：检测游戏进程
        private void CheckStatus_Game()
        {
            while (true)
            {
                if (SearchProc_Game("wukong") == true || (SearchProc_Game("悟空") == true && SearchProc_Game("修改器") == false))
                {
                    bool_GameStatus = true;
                    this.Invoke(new Action(()=>
                    {
                        label_xgq_CheckGame_FuHao.Text = "✔";
                        label_xgq_CheckGame_FuHao.ForeColor = Color.FromArgb(15, 248, 0);
                        label_xgq_CheckGame_FuHao.Visible = true;
                        label_xgq_CheckGame_Text.Visible = true;
                        if (radioButton_cn.Checked == true)
                        {
                            label_xgq_CheckGame_Text.Text = "游戏正在运行，可以使用";
                        }
                        else if(radioButton_tc.Checked == true)
                        {
                            label_xgq_CheckGame_Text.Text = "游戲正在運行，可以使用";
                        }
                        else if (radioButton_en.Checked == true)
                        {
                            label_xgq_CheckGame_Text.Text = "The game is running,just do it";
                        }
                        //停止Timer事件
                        timer_label_xgq_CheckGame.Stop();
                    }));
                }
                else
                {
                    bool_GameStatus = false;
                    this.Invoke(new Action(() =>
                    {
                        pictureBox_xgq_switch_F1.Image = Resources.switch_off;
                        pictureBox_xgq_switch_F2.Image = Resources.switch_off;
                        pictureBox_xgq_switch_F3.Image = Resources.switch_off;
                        pictureBox_xgq_switch_F4.Image = Resources.switch_off;
                        pictureBox_xgq_switch_F5.Image = Resources.switch_off;
                        pictureBox_xgq_switch_F6.Image = Resources.switch_off;
                        pictureBox_xgq_switch_F7.Image = Resources.switch_off;
                        bool_F1 = false;
                        bool_F2 = false;
                        bool_F3 = false;
                        bool_F4 = false;
                        bool_F5 = false;
                        bool_F6 = false;
                        bool_F7 = false;
                        label_xgq_CheckGame_FuHao.Text = "!";
                        label_xgq_CheckGame_FuHao.ForeColor = Color.DarkGray;
                        if (radioButton_cn.Checked == true)
                        {
                            label_xgq_CheckGame_Text.Text = "游戏尚未运行，请启动游戏";
                        }
                        else if (radioButton_tc.Checked == true)
                        {
                            label_xgq_CheckGame_Text.Text = "游戲尚未運行，請啓動游戲";
                        }
                        else if (radioButton_en.Checked == true)
                        {
                            label_xgq_CheckGame_Text.Text = "Waiting for the game to start";
                        }
                        //启动Timer事件
                        timer_label_xgq_CheckGame.Start();
                    }));
                }
                //线程睡觉
                Thread.Sleep(250);
            }
        }

        //方法：判断是否包含此字串的进程
        private bool SearchProc_Game(string str_ProcName)
        {
            try
            {
                //模糊进程名（枚举）
                //Process[] ps = Process.GetProcesses();  //进程集合
                foreach (Process p in Process.GetProcesses())
                {
                    //Console.WriteLine(p.ProcessName + p.Id);
                    if (p.ProcessName.IndexOf(str_ProcName, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        //事件：（Timer）label_xgq_CheckGame文字闪烁
        private void timer_label_xgq_CheckGame_Tick(object sender, EventArgs e)
        {
            if (timer_label_xgq_CheckGame.Interval % 2 == 0)
            {
                label_xgq_CheckGame_FuHao.Visible = false;
                //label_xgq_CheckGame_Text.Visible = false;
            }
            else
            {
                label_xgq_CheckGame_FuHao.Visible = true;
                //label_xgq_CheckGame_Text.Visible = true;
            }
            timer_label_xgq_CheckGame.Interval++;
        }

        //事件：（Timer）label_label_Error文字闪烁
        private void timer_label_Error_Tick(object sender, EventArgs e)
        {
            if (int_timerLabel >= 6)
            {
                int_timerLabel = 0;
                label_Error.Visible = false;
                timer_label_Error.Stop();
            }
            else 
            {
                if (timer_label_Error.Interval % 2 == 0)
                {
                    label_Error.Visible = false;
                }
                else
                {
                    label_Error.Visible = true;
                }
                timer_label_Error.Interval++;
                //label_Error.Visible = true;
                int_timerLabel += 1;
            }   
        }

        //事件：（Timer）Ctrl+F8点击后报错动画展示
        private void timer_F8_Click_Error_Tick(object sender, EventArgs e)
        {
            if (int_timerBtn >= 1)
            {
                pictureBox_xgq_switch_F8.Image = Resources.switch_btn;
                int_timerBtn = 0;
                timer_F8_Click_Error.Stop();
            }
            else
            {
                pictureBox_xgq_switch_F8.Image = Resources.switch_btn_error;
                int_timerBtn += 1;
            }
        }

        //事件：（Timer）Ctrl+F8点击后正确动画展示
        private void timer_F8_Click_OK_Tick(object sender, EventArgs e)
        {
            if (int_timerBtn >= 1)
            {
                pictureBox_xgq_switch_F8.Image = Resources.switch_btn;
                int_timerBtn = 0;
                timer_F8_Click_OK.Stop();
            }
            else
            {
                pictureBox_xgq_switch_F8.Image = Resources.switch_btn_ok;
                int_timerBtn += 1;
            }
        }

        //事件：点击悟空图片
        private void pic_Wukong_Click()
        {
            //重置ListBox
            listBox_GameList.Items.Clear();
            //查找当前目录下的游戏启动程序
            var files = new DirectoryInfo(Environment.CurrentDirectory).EnumerateFiles().AsParallel().Where(s => (s.Name.IndexOf("exe", StringComparison.OrdinalIgnoreCase) >= 0 && s.Name.IndexOf("wukong", StringComparison.OrdinalIgnoreCase) >= 0) || (s.Name.IndexOf("exe", StringComparison.OrdinalIgnoreCase) >= 0 && s.Name.IndexOf("悟空", StringComparison.OrdinalIgnoreCase) >= 0 && s.Name.IndexOf("修改器", StringComparison.OrdinalIgnoreCase) < 0)).ToList();
            //添加到ListBox
            foreach (var file in files)
            {
                listBox_GameList.Items.Add(Environment.CurrentDirectory + "\\" + file.Name);
            }
            //判断并启动游戏程序
            if (listBox_GameList.Items.Count == 0)
            {
                MessageBox.Show("修改器目录下无游戏启动文件\nThere is no game startup file in the trainer directory", "错误 ERROR");
            }
            else if (listBox_GameList.Items.Count == 1)
            {
                Process process_game = Process.Start(listBox_GameList.Items[0].ToString());
            }
            else
            {
                MessageBox.Show("修改器目录下的游戏启动文件过多\nToo many game startup files in the trainer directory", "错误 ERROR");
            }
        }

        //事件：Ctrl+F1按键按下 或 Ctrl+F1开关点击
        private void F1_PressDownOrClick()
        {
            if (bool_F1 == false && bool_GameStatus == true)
            {
                pictureBox_xgq_switch_F1.Image = Resources.switch_on;
                bool_F1 = true;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.open);
                player.Play();
            }
            else if (bool_F1 == true && bool_GameStatus == true)
            {
                pictureBox_xgq_switch_F1.Image = Resources.switch_off;
                bool_F1 = false;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.close);
                player.Play();
            }
            else
            {
                System.Media.SystemSounds.Asterisk.Play();
                timer_label_Error.Stop();
                timer_label_Error.Start();
            }
        }

        //事件：Ctrl+F2按键按下 或 Ctrl+F2开关点击
        private void F2_PressDownOrClick()
        {
            if (bool_F2 == false && bool_GameStatus == true)
            {
                pictureBox_xgq_switch_F2.Image = Resources.switch_on;
                bool_F2 = true;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.open);
                player.Play();
            }
            else if (bool_F2 == true && bool_GameStatus == true)
            {
                pictureBox_xgq_switch_F2.Image = Resources.switch_off;
                bool_F2 = false;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.close);
                player.Play();
            }
            else
            {
                System.Media.SystemSounds.Asterisk.Play();
                timer_label_Error.Stop();
                timer_label_Error.Start();
            }
        }

        //事件：Ctrl+F3按键按下 或 Ctrl+F3开关点击
        private void F3_PressDownOrClick()
        {
            if (bool_F3 == false && bool_GameStatus == true)
            {
                pictureBox_xgq_switch_F3.Image = Resources.switch_on;
                bool_F3 = true;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.open);
                player.Play();
            }
            else if (bool_F3 == true && bool_GameStatus == true)
            {
                pictureBox_xgq_switch_F3.Image = Resources.switch_off;
                bool_F3 = false;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.close);
                player.Play();
            }
            else
            {
                System.Media.SystemSounds.Asterisk.Play();
                timer_label_Error.Stop();
                timer_label_Error.Start();
            }
        }

        //事件：Ctrl+F4按键按下 或 Ctrl+F4开关点击
        private void F4_PressDownOrClick()
        {
            if (bool_F4 == false && bool_GameStatus == true)
            {
                pictureBox_xgq_switch_F4.Image = Resources.switch_on;
                bool_F4 = true;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.open);
                player.Play();
            }
            else if (bool_F4 == true && bool_GameStatus == true)
            {
                pictureBox_xgq_switch_F4.Image = Resources.switch_off;
                bool_F4 = false;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.close);
                player.Play();
            }
            else
            {
                System.Media.SystemSounds.Asterisk.Play();
                timer_label_Error.Stop();
                timer_label_Error.Start();
            }
        }

        //事件：Ctrl+F5按键按下 或 Ctrl+F5开关点击
        private void F5_PressDownOrClick()
        {
            if (bool_F5 == false && bool_GameStatus == true)
            {
                pictureBox_xgq_switch_F5.Image = Resources.switch_on;
                bool_F5 = true;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.open);
                player.Play();
            }
            else if (bool_F5 == true && bool_GameStatus == true)
            {
                pictureBox_xgq_switch_F5.Image = Resources.switch_off;
                bool_F5 = false;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.close);
                player.Play();
            }
            else
            {
                System.Media.SystemSounds.Asterisk.Play();
                timer_label_Error.Stop();
                timer_label_Error.Start();
            }
        }

        //事件：Ctrl+F6按键按下 或 Ctrl+F6开关点击
        private void F6_PressDownOrClick()
        {
            if (bool_F6 == false && bool_GameStatus == true)
            {
                pictureBox_xgq_switch_F6.Image = Resources.switch_on;
                bool_F6 = true;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.open);
                player.Play();
            }
            else if (bool_F6 == true && bool_GameStatus == true)
            {
                pictureBox_xgq_switch_F6.Image = Resources.switch_off;
                bool_F6 = false;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.close);
                player.Play();
            }
            else
            {
                System.Media.SystemSounds.Asterisk.Play();
                timer_label_Error.Stop();
                timer_label_Error.Start();
            }
        }

        //事件：Ctrl+F7按键按下 或 Ctrl+F7开关点击
        private void F7_PressDownOrClick()
        {
            if (bool_F7 == false && bool_GameStatus == true)
            {
                pictureBox_xgq_switch_F7.Image = Resources.switch_on;
                bool_F7 = true;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.open);
                player.Play();
            }
            else if (bool_F7 == true && bool_GameStatus == true)
            {
                pictureBox_xgq_switch_F7.Image = Resources.switch_off;
                bool_F7 = false;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.close);
                player.Play();
            }
            else
            {
                System.Media.SystemSounds.Asterisk.Play();
                timer_label_Error.Stop();
                timer_label_Error.Start();
            }
        }

        //事件：Ctrl+F8按键按下 或 Ctrl+F8开关点击
        private void F8_PressDownOrClick()
        {
            if (bool_GameStatus == true)
            {
                timer_F8_Click_OK.Start();
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Resources.open);
                player.Play();
            }
            else
            {
                System.Media.SystemSounds.Asterisk.Play();
                timer_F8_Click_Error.Stop();
                timer_F8_Click_Error.Start();
                timer_label_Error.Stop();
                timer_label_Error.Start();
                pictureBox_xgq_switch_F8.Image = Resources.switch_btn;
            }
        }

        //【重要】事件：PictureBox点击事件
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
                case "pictureBox_WuKong_Monkey":
                    pic_Wukong_Click();
                    break;
                case "pictureBox_XiuGaiQi_Logo":
                    About_Trainer about_Trainer = new About_Trainer();
                    about_Trainer.ShowDialog();
                    break;
                case "pictureBox_WuKong_Text":
                    Process.Start("https://heishenhua.com");
                    break;
                case "pictureBox_xgq_switch_F1":
                    F1_PressDownOrClick();
                    break;
                case "pictureBox_xgq_switch_F2":
                    F2_PressDownOrClick();
                    break;
                case "pictureBox_xgq_switch_F3":
                    F3_PressDownOrClick();
                    break;
                case "pictureBox_xgq_switch_F4":
                    F4_PressDownOrClick();
                    break;
                case "pictureBox_xgq_switch_F5":
                    F5_PressDownOrClick();
                    break;
                case "pictureBox_xgq_switch_F6":
                    F6_PressDownOrClick();
                    break;
                case "pictureBox_xgq_switch_F7":
                    F7_PressDownOrClick();
                    break;
                case "pictureBox_xgq_switch_F8":
                    F8_PressDownOrClick();
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
                    pictureBox_Btn_Quit.Image = Resources.quit_btn_light;
                    pictureBox_Btn_Quit.Cursor = Cursors.Hand;
                    break;
                case "pictureBox_Btn_Minimized":
                    pictureBox_Btn_Minimized.Image = Resources.minimized_btn_light;
                    pictureBox_Btn_Minimized.Cursor = Cursors.Hand;
                    break;
                case "pictureBox_WuKong_Monkey":
                    pictureBox_WuKong_Monkey.Image = Resources.wukong_monkey_light;
                    label_info_StartGame.Visible = true;
                    pictureBox_WuKong_Monkey.Cursor = Cursors.Hand;
                    break;
                case "pictureBox_XiuGaiQi_Logo":
                    pictureBox_XiuGaiQi_Logo.Image = Resources.xgq_logo_light;
                    label_info_XiuGaiQi.Visible = true;
                    pictureBox_XiuGaiQi_Logo.Cursor = Cursors.Hand;
                    break;
                case "pictureBox_WuKong_Text":
                    if (radioButton_en.Checked == true)
                    {
                        pictureBox_WuKong_Text.Image = Resources.wukong_title_light_en;
                        label_info_WuKongWebsite.Visible = true;
                        pictureBox_WuKong_Text.Cursor = Cursors.Hand;
                    }
                    else
                    {
                        pictureBox_WuKong_Text.Image = Resources.wukong_title_light;
                        label_info_WuKongWebsite.Visible = true;
                        pictureBox_WuKong_Text.Cursor = Cursors.Hand;
                    }
                    break;
                case "pictureBox_xgq_switch_F1":
                    pictureBox_xgq_switch_F1.Cursor = Cursors.Hand;
                    break;
                case "pictureBox_xgq_switch_F2":
                    pictureBox_xgq_switch_F2.Cursor = Cursors.Hand;
                    break;
                case "pictureBox_xgq_switch_F3":
                    pictureBox_xgq_switch_F3.Cursor = Cursors.Hand;
                    break;
                case "pictureBox_xgq_switch_F4":
                    pictureBox_xgq_switch_F4.Cursor = Cursors.Hand;
                    break;
                case "pictureBox_xgq_switch_F5":
                    pictureBox_xgq_switch_F5.Cursor = Cursors.Hand;
                    break;
                case "pictureBox_xgq_switch_F6":
                    pictureBox_xgq_switch_F6.Cursor = Cursors.Hand;
                    break;
                case "pictureBox_xgq_switch_F7":
                    pictureBox_xgq_switch_F7.Cursor = Cursors.Hand;
                    break;
                case "pictureBox_xgq_switch_F8":
                    pictureBox_xgq_switch_F8.Cursor = Cursors.Hand;
                    pictureBox_xgq_switch_F8.Image = Resources.switch_btn_light;
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
                case "pictureBox_WuKong_Monkey":
                    pictureBox_WuKong_Monkey.Image = Resources.wukong_monkey;
                    label_info_StartGame.Visible = false;
                    pictureBox_WuKong_Monkey.Cursor = Cursors.Default;
                    break;
                case "pictureBox_XiuGaiQi_Logo":
                    pictureBox_XiuGaiQi_Logo.Image = Resources.xgq_logo;
                    label_info_XiuGaiQi.Visible = false;
                    pictureBox_XiuGaiQi_Logo.Cursor = Cursors.Default;
                    break;
                case "pictureBox_WuKong_Text":
                    if (radioButton_en.Checked == true)
                    {
                        pictureBox_WuKong_Text.Image = Resources.wukong_title_en;
                        label_info_WuKongWebsite.Visible = false;
                        pictureBox_WuKong_Text.Cursor = Cursors.Default;
                    }
                    else
                    {
                        pictureBox_WuKong_Text.Image = Resources.wukong_title;
                        label_info_WuKongWebsite.Visible = false;
                        pictureBox_WuKong_Text.Cursor = Cursors.Default;
                    }
                    break;
                case "pictureBox_xgq_switch_F1":
                    pictureBox_xgq_switch_F1.Cursor = Cursors.Default;
                    break;
                case "pictureBox_xgq_switch_F2":
                    pictureBox_xgq_switch_F2.Cursor = Cursors.Default;
                    break;
                case "pictureBox_xgq_switch_F3":
                    pictureBox_xgq_switch_F3.Cursor = Cursors.Default;
                    break;
                case "pictureBox_xgq_switch_F4":
                    pictureBox_xgq_switch_F4.Cursor = Cursors.Default;
                    break;
                case "pictureBox_xgq_switch_F5":
                    pictureBox_xgq_switch_F5.Cursor = Cursors.Default;
                    break;
                case "pictureBox_xgq_switch_F6":
                    pictureBox_xgq_switch_F6.Cursor = Cursors.Default;
                    break;
                case "pictureBox_xgq_switch_F7":
                    pictureBox_xgq_switch_F7.Cursor = Cursors.Default;
                    break;
                case "pictureBox_xgq_switch_F8":
                    pictureBox_xgq_switch_F8.Cursor = Cursors.Default;
                    pictureBox_xgq_switch_F8.Image = Resources.switch_btn;
                    break;
                default:
                    break;
            }
        }

        //事件：界面语言单选按钮选中事件
        private void radioBtn_CheckedChange(object sender, EventArgs e)
        {
            if (!((RadioButton)sender).Checked)
            {
                return;
            }
            switch (((RadioButton)sender).Text.ToString())
            {
                case "简体":
                    //控件变化
                    radioButton_cn.ForeColor = Color.FromArgb(255, 128, 0);
                    radioButton_tc.ForeColor = Color.White;
                    radioButton_en.ForeColor = Color.White;
                    toolTip_info.SetToolTip(pictureBox_Btn_Quit, "退出");
                    toolTip_info.SetToolTip(pictureBox_Btn_Minimized, "最小化");
                    label_info_StartGame.Font = new Font("微软雅黑", 36F, FontStyle.Bold, GraphicsUnit.Point);
                    label_info_StartGame.Text = "启动游戏";
                    label_info_WuKongWebsite.Font = new Font("微软雅黑", 36F, FontStyle.Bold, GraphicsUnit.Point);
                    label_info_WuKongWebsite.Text = "游戏官网";
                    label_info_XiuGaiQi.Font = new Font("微软雅黑", 36F, FontStyle.Bold, GraphicsUnit.Point);
                    label_info_XiuGaiQi.Text = "关于修改器";
                    label_xgq_title_GameName.Text = "《黑神话：悟空》";
                    label_xgq_title_XiuGaiQiName.Text = "v1.0 八项修改器";
                    pictureBox_WuKong_HeiShenHua.Visible = true;
                    pictureBox_WuKong_Text.Image = Resources.wukong_title;
                    //修改器功能文字变化
                    label_xgq_CheckGame_Text.Text = "游戏尚未运行，请启动游戏";
                    label_xgq_list_KuaiJieJian.Text = "快捷键";
                    label_xgq_list_GongNengLieBiao.Text = "功能列表";
                    //label_xgq_list_F1.Text = "数字键1";
                    label_xgq_list_F1_Info.Text = "无限生命";
                    //label_xgq_list_F2.Text = "数字键2";
                    label_xgq_list_F2_Info.Text = "无限葫芦";
                    //label_xgq_list_F3.Text = "数字键3";
                    label_xgq_list_F3_Info.Text = "无限法力";
                    //label_xgq_list_F4.Text = "数字键4";
                    label_xgq_list_F4_Info.Text = "无限气力";
                    //label_xgq_list_F5.Text = "数字键5";
                    label_xgq_list_F5_Info.Text = "无限棍势";
                    //label_xgq_list_F6.Text = "数字键6";
                    label_xgq_list_F6_Info.Text = "法术无冷却时间";
                    //label_xgq_list_F7.Text = "数字键7";
                    label_xgq_list_F7_Info.Text = "一击必杀";
                    //label_xgq_list_F8.Text = "数字键8";
                    label_xgq_list_F8_Info.Text = "保存游戏进度";
                    //修改器信息文字变化
                    label_xgq_creditor.Text = "作者：夙炅月影@ZJHCOFI";
                    label_xgq_version.Text = "修改器版本：Build.2024.08.18";
                    label_Error.Text = "开启失败，游戏尚未运行！";
                    break;
                case "繁體":
                    //控件变化
                    radioButton_cn.ForeColor = Color.White;
                    radioButton_tc.ForeColor = Color.FromArgb(255, 128, 0);
                    radioButton_en.ForeColor = Color.White;
                    toolTip_info.SetToolTip(pictureBox_Btn_Quit, "退出");
                    toolTip_info.SetToolTip(pictureBox_Btn_Minimized, "最小化");
                    label_info_StartGame.Font = new Font("微软雅黑", 36F, FontStyle.Bold, GraphicsUnit.Point);
                    label_info_StartGame.Text = "啓動游戲";
                    label_info_WuKongWebsite.Font = new Font("微软雅黑", 36F, FontStyle.Bold, GraphicsUnit.Point);
                    label_info_WuKongWebsite.Text = "游戲官網";
                    label_info_XiuGaiQi.Font = new Font("微软雅黑", 36F, FontStyle.Bold, GraphicsUnit.Point);
                    label_info_XiuGaiQi.Text = "關於作弊器";
                    label_xgq_title_GameName.Text = "《黑神話：悟空》";
                    label_xgq_title_XiuGaiQiName.Text = "v1.0 八項作弊器";
                    pictureBox_WuKong_HeiShenHua.Visible = true;
                    pictureBox_WuKong_Text.Image = Resources.wukong_title;
                    //修改器功能文字变化
                    label_xgq_CheckGame_Text.Text = "游戲尚未運行，請啓動游戲";
                    label_xgq_list_KuaiJieJian.Text = "快捷鍵";
                    label_xgq_list_GongNengLieBiao.Text = "功能列表";
                    //label_xgq_list_F1.Text = "數字鍵1";
                    label_xgq_list_F1_Info.Text = "無限生命";
                    //label_xgq_list_F2.Text = "數字鍵2";
                    label_xgq_list_F2_Info.Text = "無限葫蘆";
                    //label_xgq_list_F3.Text = "數字鍵3";
                    label_xgq_list_F3_Info.Text = "無限法力";
                    //label_xgq_list_F4.Text = "數字鍵4";
                    label_xgq_list_F4_Info.Text = "無限氣力";
                    //label_xgq_list_F5.Text = "數字鍵5";
                    label_xgq_list_F5_Info.Text = "無限棍勢";
                    //label_xgq_list_F6.Text = "數字鍵6";
                    label_xgq_list_F6_Info.Text = "法術無冷卻時間";
                    //label_xgq_list_F7.Text = "數字鍵7";
                    label_xgq_list_F7_Info.Text = "一擊必殺";
                    //label_xgq_list_F8.Text = "數字鍵8";
                    label_xgq_list_F8_Info.Text = "保存游戲進度";
                    //修改器信息文字变化
                    label_xgq_creditor.Text = "作者：夙炅月影@ZJHCOFI";
                    label_xgq_version.Text = "作弊器版本：Build.2024.08.18";
                    label_Error.Text = "啓用失敗，游戲尚未運行";
                    break;
                case "English":
                    //控件变化
                    radioButton_cn.ForeColor = Color.White;
                    radioButton_tc.ForeColor = Color.White;
                    radioButton_en.ForeColor = Color.FromArgb(255, 128, 0);
                    toolTip_info.SetToolTip(pictureBox_Btn_Quit, "Quit");
                    toolTip_info.SetToolTip(pictureBox_Btn_Minimized, "Minimized");
                    label_info_StartGame.Font = new Font("微软雅黑", 28F, FontStyle.Bold, GraphicsUnit.Point);
                    label_info_StartGame.Text = "Play Game";
                    label_info_WuKongWebsite.Font = new Font("微软雅黑", 15F, FontStyle.Bold, GraphicsUnit.Point);
                    label_info_WuKongWebsite.Text = "About\nBLACK MYTH WUKONG";
                    label_info_XiuGaiQi.Font = new Font("微软雅黑", 26F, FontStyle.Bold, GraphicsUnit.Point);
                    label_info_XiuGaiQi.Text = "About Trainer";
                    label_xgq_title_GameName.Text = " BLACK MYTH WUKONG";
                    label_xgq_title_XiuGaiQiName.Text = "v1.0 Plus 8 Trainer";
                    pictureBox_WuKong_HeiShenHua.Visible = false;
                    pictureBox_WuKong_Text.Image = Resources.wukong_title_en;
                    //修改器功能文字变化
                    label_xgq_CheckGame_Text.Text = "Waiting for the game to start";
                    label_xgq_list_KuaiJieJian.Text = "Hotkeys";
                    label_xgq_list_GongNengLieBiao.Text = "Options";
                    //label_xgq_list_F1.Text = "NUM 1";
                    label_xgq_list_F1_Info.Text = "Infinite Health";
                    //label_xgq_list_F2.Text = "NUM 2";
                    label_xgq_list_F2_Info.Text = "Infinite Gourd";
                    //label_xgq_list_F3.Text = "NUM 3";
                    label_xgq_list_F3_Info.Text = "Infinite Magic Power";
                    //label_xgq_list_F4.Text = "NUM 4";
                    label_xgq_list_F4_Info.Text = "Infinite Stamina";
                    //label_xgq_list_F5.Text = "NUM 5";
                    label_xgq_list_F5_Info.Text = "Infinite Stick Power";
                    //label_xgq_list_F6.Text = "NUM 6";
                    label_xgq_list_F6_Info.Text = "Spell No Cool Down Time";
                    //label_xgq_list_F7.Text = "NUM 7";
                    label_xgq_list_F7_Info.Text = "One Hit Kill";
                    //label_xgq_list_F8.Text = "NUM 8";
                    label_xgq_list_F8_Info.Text = "Save Game";
                    //修改器信息文字变化
                    label_xgq_creditor.Text = "        Credit: ZJHCOFI";
                    label_xgq_version.Text = "Trainer Version: Build.2024.08.18";
                    label_Error.Text = "The game is not running, enable failed";
                    break;
                default:
                    break;
            }
        }

        //事件：Label鼠标经过事件
        private void label_MouseMove(object sender, EventArgs e)
        {
            switch (((Label)sender).Name.ToString())
            {
                case "label_xgq_creditor":
                    label_xgq_creditor.ForeColor = Color.FromArgb(255, 128, 0);
                    label_xgq_creditor.Cursor = Cursors.Hand;
                    break;
                case "label_xgq_version":
                    label_xgq_version.ForeColor = Color.FromArgb(255, 128, 0);
                    label_xgq_version.Cursor = Cursors.Hand;
                    break;
                default:
                    break;
            }
        }

        //事件：Label鼠标离开事件
        private void label_MouseLeave(object sender, EventArgs e)
        {
            switch (((Label)sender).Name.ToString())
            {
                case "label_xgq_creditor":
                    label_xgq_creditor.ForeColor = Color.DarkGray;
                    label_xgq_creditor.Cursor = Cursors.Default;
                    break;
                case "label_xgq_version":
                    label_xgq_version.ForeColor = Color.DarkGray;
                    label_xgq_version.Cursor = Cursors.Default;
                    break;
                default:
                    break;
            }
        }

        //事件：Label鼠标点击事件
        private void label_Click(object sender, EventArgs e)
        {
            switch (((Label)sender).Name.ToString())
            {
                case "label_xgq_creditor":
                    Process.Start("https://zjhcofi.com");
                    break;
                case "label_xgq_version":
                    Process.Start("https://github.com/zjhcofi");
                    break;
                default:
                    break;
            }
        }

        //事件：鼠标左键按住窗口上方
        private void panel_head_MouseDown(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                return;
            }
            panel_head.Cursor = Cursors.NoMove2D;
            oldMousePosition = e.Location;
            bool_Moving = true;
        }

        //事件：鼠标松开窗口上方
        private void panel_head_MouseUp(object sender, MouseEventArgs e)
        {
            panel_head.Cursor = Cursors.Default;
            bool_Moving = false;
        }

        //事件：鼠标左键拖动窗口上方
        private void panel_head_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && bool_Moving)
            {
                Point newPosition = new Point(e.Location.X - oldMousePosition.X, e.Location.Y - oldMousePosition.Y);
                Location += new Size(newPosition);
            }
        }

        //事件：窗口关闭前的确认
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("您确认退出吗？\nAre you sure to quit?", "温馨提示 Tips", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Dispose();
                //强制退出（含线程）
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

    }
}
