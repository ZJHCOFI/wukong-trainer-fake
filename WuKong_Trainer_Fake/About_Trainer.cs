using System.Diagnostics;
using System.Windows.Forms;

namespace WuKongXiuGaiQi
{
    public partial class About_Trainer : Form
    {
        public About_Trainer()
        {
            InitializeComponent();
        }

        //点击文本框里的链接
        private void richTextBox_about_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
    }
}
