using System;
using System.Threading;
using System.Windows.Forms;

namespace Laptop_Battery_Warning_System
{
    public partial class Form1 : Form
    {
        private Thread thread;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void showNotification(String status, double percentage)
        {
            var notification = new System.Windows.Forms.NotifyIcon()
            {
                Visible = true,
                Icon = System.Drawing.SystemIcons.Information,
                BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info,
                BalloonTipTitle = "Zaki Battery Warning App",
                BalloonTipText = $"Your battery percentage is {percentage}%\nPlease take suitable action for healthier battery life !!",
            };

            // Display for 20 seconds
            notification.ShowBalloonTip(20000);

            notification.Dispose();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Hide();

            thread = new Thread(() =>
            {
                while (true)
                {
                    String batterystatus;

                    PowerStatus pwr = SystemInformation.PowerStatus;
                    batterystatus = SystemInformation.PowerStatus.BatteryChargeStatus.ToString();

                    String batterylife;
                    batterylife = SystemInformation.PowerStatus.BatteryLifePercent.ToString();

                    double bt = double.Parse(batterylife);
                    bt *= 100;

                    if (bt >= 90 || bt <= 35)
                    {
                        showNotification(batterystatus, bt);

                        MessageBox.Show($"Battery Status is ");
                    }
                    Thread.Sleep(2000);
                }
            });
            thread.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
        }
    }
}