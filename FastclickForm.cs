using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fastclick
{
    public partial class FastclickForm : Form
    {
        public FastclickForm()
        {
            InitializeComponent();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            testProgressBar.Value = testProgressBar.Value == testProgressBar.Maximum
                ? testProgressBar.Minimum
                : testProgressBar.Value + 1;
        }

        private int hotKeyHandler;
        private void Form1_Load(object sender, EventArgs e)
        {
            hotKeyHandler = HotKeyManager.RegisterHotKey(Keys.F, KeyModifiers.Shift | KeyModifiers.Windows);
            HotKeyManager.HotKeyPressed += new EventHandler<HotKeyEventArgs>(HotKeyManager_HotKeyPressed);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            HotKeyManager.UnregisterHotKey(hotKeyHandler);
        }

        ClickThread clickThread = new ClickThread();
        private void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)(() =>
            {
                clickThread.runOrStop();
                Icon = notifyIcon.Icon = clickThread.IsWorking
                    ? Properties.Resources.fireRed
                    : Properties.Resources.fire;
            }));
        }

        private void FastclickForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                notifyIcon.Visible = true;
                Hide();
            }
            else if (FormWindowState.Normal == WindowState)
            {
                notifyIcon.Visible = false;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
    }
}
