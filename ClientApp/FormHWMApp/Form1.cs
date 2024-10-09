using FormHWPApp.Arduino;
using FormTestApp.HWM;
using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormTestApp
{
    public partial class Form1 : Form
    {
        private HWMService HWMService;
        private ISerialCommunication serial;
     
        public Form1(HWMService HWMService, ISerialCommunication arduinoSerial)
        {
            InitializeComponent();
            this.HWMService = HWMService;
            this.serial = arduinoSerial;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            HWMService.NewData += Tick;
     
        }

        private void Tick()
        {
            this.Invoke(new Action(delegate ()
            {
                Update_Label();
                Update_Tree();
            }));
        }

        private void Update_Label()
        {
           
           label1.Text = HWMService.pcUpdatedCounter.ToString();
           
        }

        private void Update_Tree()
        {
           

            if (!treeView1.Nodes.ContainsKey("root"))
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
            }
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            serial.sendData(textBox1.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            serial.readData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            serial.startCommunication();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            serial.stopCommunication();
        }
    }
}
