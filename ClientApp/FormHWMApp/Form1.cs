﻿using FormHWPApp.Arduino;
using FormHWPApp.HWM;
using FormTestApp.HWM;
using LibreHardwareMonitor.Hardware;
using LibreHardwareMonitor.Hardware.Cpu;
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
        private PeriodicalTask PeriodicalTask;
        private IHWMData HWMData;

        public Form1(HWMService HWMService, IHWMData HWMData, ISerialCommunication arduinoSerial, PeriodicalTask periodicalTask)
        {
            InitializeComponent();
            this.HWMService = HWMService;
            this.serial = arduinoSerial;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.HWMData = HWMData;

            periodicalTask.addAction(Tick);
        }

        public void Tick()
        {
            this.Invoke(new Action(delegate ()
            {
                //Update_Label();
                Update_Tree();
            }));
        }

        private void Update_Label()
        {

            label1.Text = HWMData.getUpdateCounter().ToString();

        }

        private void Update_Tree()
        {
            if (HWMData.GetData() is null)
                return;

            foreach (PCComponent component in HWMData.GetData().data)
            {
                if (!treeView1.Nodes.ContainsKey(component.name))
                {
                    treeView1.Nodes.Add(component.name, component.name);
                }
                TreeNode rootNode = treeView1.Nodes[component.name];

                Dictionary<string, List<ComponentData>> componetesTypes = component.getComponentsByType();

                foreach (string componentType in componetesTypes.Keys)
                {
                    if (!rootNode.Nodes.ContainsKey(componentType))
                    {
                        rootNode.Nodes.Add(componentType, componentType);
                    }
                    TreeNode subNode = rootNode.Nodes[componentType];
                    //subNode.ExpandAll();

                    foreach (ComponentData componentData in componetesTypes[componentType])
                    {
                        if (!subNode.Nodes.ContainsKey(componentData.name))
                        {
                            subNode.Nodes.Add(componentData.name, componentData.name);
                        }
                        TreeNode subSubNode = subNode.Nodes[componentData.name];

                        if (!subSubNode.Nodes.ContainsKey(componentData.name))
                        {
                            subSubNode.Nodes.Add(componentData.name, componentData.name);
                        }
                        subSubNode.Nodes[componentData.name].Text = componentData.value;
                    }
                }
            }
        }

        private void UpdateNodeText(TreeNode node, string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<TreeNode, string>(UpdateNodeText), node, text);
            }
            else
            {
                node.Text = text;
            }
        }

        private void send_new_data()
        {
            if (!serial.isConnected())
                return;

            StringBuilder sb = new StringBuilder();
            sb.Append("D:");
            foreach (ISensor sensor in HWMData.GetLoads())
            {
                String value = sensor.Value.ToString().Replace(",", ".");
                if (value.Length > 4)
                {
                    value = value.Substring(0, 4);
                }
                else if (value.Length == 1)
                {
                    value = value + ".00";
                }
                sb.Append(value + ";");
            }
            serial.sendData(sb.ToString());
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
