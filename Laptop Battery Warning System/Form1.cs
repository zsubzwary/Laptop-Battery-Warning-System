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
            new Thread(() =>
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

                        MessageBox.Show("Battery charge status : " + batterystatus + " - life" + batterylife);

                    } 
                }
            }).Start();





        }


        private void showNotification(String status, double percentage)
        {
            var notification = new System.Windows.Forms.NotifyIcon()
            {
                Visible = true,
                Icon = System.Drawing.SystemIcons.Information,
                BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info,
                BalloonTipTitle = "Zaki Battery Warning App",
                BalloonTipText = $"Your batter percentage is {percentage}%\nPlease take suitable action for healthier battery life !!",
            };

            // Display for 20 seconds.
            notification.ShowBalloonTip(20000);
            
            notification.Dispose();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Hide();
        }
    }
}
