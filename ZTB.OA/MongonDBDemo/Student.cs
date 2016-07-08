// ==========================================
// Author                  :    WIN-JH13BJM99UM 
// Create Time           :    2016/7/8 9:54:58
// Update Time          :    2016/7/8 9:54:58
// ==========================================
// Class Description     :    
// ==========================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongonDBDemo
{
    public class Student : EntityBase
    {
        /// <summary>
        /// 获取 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 获取 状态
        /// </summary>
        public State State { get; set; }
    }
    /// <summary>
    /// 状态
    /// </summary>
    public enum State
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,

        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 未使用
        /// </summary>
        Unused = 2,
    }
}
