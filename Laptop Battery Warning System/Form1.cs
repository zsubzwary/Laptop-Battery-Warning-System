using System;
using System.Threading;
using System.Windows.Forms;

namespace Laptop_Battery_Warning_System
{
    public partial class Form1 : Form
    {
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
            notification.ShowBalloonTip(20 * 1000);

            notification.Dispose();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Hide();

            while (true)
            {
                String batterystatus;

                PowerStatus pwr = SystemInformation.PowerStatus;
                batterystatus = SystemInformation.PowerStatus.BatteryChargeStatus.ToString();

                String batterylife;
                batterylife = SystemInformation.PowerStatus.BatteryLifePercent.ToString();

                double bt = double.Parse(batterylife);
                bt *= 100;

                if ((bt >= 90 && pwr.PowerLineStatus == PowerLineStatus.Online) || (bt <= 35 && pwr.PowerLineStatus == PowerLineStatus.Offline))
                {
                    showNotification(batterystatus, bt);

                    MessageBox.Show($"Battery Status is {batterystatus}, and currently the battery is at {bt}%");
                }

                //gets time in minutes
                sleepWithoutFreezingUI(2); // will sleep Approximately till that time limit
            }
        }

        private void sleepWithoutFreezingUI(int minutes)
        {
            int end = (minutes * 60) * 4;

            for (int i = 0; i < end; i++)
            {
                Thread.Sleep(220); // leaving 30ms after each 250ms for DoEvents()
                Application.DoEvents();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}