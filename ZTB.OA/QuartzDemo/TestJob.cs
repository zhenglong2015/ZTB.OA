// ==========================================
// Author                  :  ZTB 
// Create Time           :    2016/8/1 17:34:47
// ==========================================
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzDemo
{
    public sealed class TestJob : IJob
    {
        
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("定时任务1。。。。");
        }
    }
}
