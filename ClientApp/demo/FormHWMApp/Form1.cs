using FormTestApp.HWM;
using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormTestApp
{
    public partial class Form1 : Form
    {
        private Timer timer;
        private HWMService HWMService;
        public Form1(HWMService HWMService)
        {
            InitializeComponent();
            this.HWMService = HWMService;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            label1.Text = HWMService.pcUpdatedCounter.ToString();
            //treeView1.Nodes.Clear();
            
            if(!treeView1.Nodes.ContainsKey("root"))
            {
                treeView1.Nodes.Add("root", "CPU");
            }
            TreeNode rootNode = treeView1.Nodes["root"];
            
            foreach (ISensor sensor in HWMService.GetLoads())
            {
                if (!rootNode.Nodes.ContainsKey(sensor.Name))
                {
                    rootNode.Nodes.Add(sensor.Name, sensor.Name);
                }
                TreeNode subNode = rootNode.Nodes[sensor.Name];
                subNode.ExpandAll();

                if (!subNode.Nodes.ContainsKey(sensor.Name))
                {
                    subNode.Nodes.Add(sensor.Name, sensor.Value.ToString());
                }
                subNode.Nodes[sensor.Name].Text = sensor.Value.ToString();

              

                /*if (!treeView1.Nodes["root"].Nodes.ContainsKey(sensor.Name) )
                {
                    treeView1.Nodes["root"].Nodes.Add(sensor.Name, sensor.Name + ": " + sensor.Value.ToString());
                }
                treeView1.Nodes["root"].Nodes[sensor.Name].Text = sensor.Name + ": " + sensor.Value.ToString();*/

                //TreeNode subNode = node.Nodes.Add(sensor.Name, sensor.Value.ToString());

            }

            //treeView1.ExpandAll();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, EventArgs e)
        {
            // Show the form again
            this.Show();
            this.WindowState = FormWindowState.Normal;
            // Hide the tray icon
            notifyIcon1.Visible = false;
            this.ShowInTaskbar = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            // Show the tray icon
            notifyIcon1.Visible = true;
            // Optional: Show a balloon tip
            notifyIcon1.ShowBalloonTip(1000, "Minimized", "The application is minimized to the tray.", ToolTipIcon.Info);
        }

        // Optional: Add a context menu for the tray icon with an "Exit" option
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HWMService.StopThread();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HWMService.StopThread();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HWMService.StartThread();   
        }
    }
}
