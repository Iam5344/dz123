using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinFormsApp7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadProcesses();
        }

        private void LoadProcesses()
        {
            listBox1.Items.Clear();
            Process[] processes = Process.GetProcesses();
            foreach (Process p in processes)
            {
                listBox1.Items.Add(p.ProcessName + " PID: " + p.Id);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)numericUpDown1.Value * 1000;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;

            string item = listBox1.SelectedItem.ToString();
            int pid = int.Parse(item.Split("PID: ")[1]);
            Process p = Process.GetProcessById(pid);

            label1.Text = "PID: " + p.Id;
            label2.Text = "Потоків: " + p.Threads.Count;
            label3.Text = "Копій: " + Process.GetProcessesByName(p.ProcessName).Length;
            label4.Text = "Старт: " + p.StartTime;
            label5.Text = "CPU: " + p.TotalProcessorTime;

            button1.Tag = pid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Tag == null) return;
            int pid = (int)button1.Tag;
            Process.GetProcessById(pid).Kill();
            LoadProcesses();
        }
    }
}
