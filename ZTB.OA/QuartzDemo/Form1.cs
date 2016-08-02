using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuartzDemo
{
    public partial class Form1 : Form
    {
        IScheduler scheduler;
        public Form1()
        {
            InitializeComponent();
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (scheduler.IsShutdown)
                scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            scheduler.Shutdown();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            scheduler.PauseAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            scheduler.ResumeAll();
        }
    }
}
