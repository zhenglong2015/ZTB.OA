using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace ZTB.OA.Wrokflow
{
    /// <summary>
    /// 在工作流中暂停只能创建代码工作流并继承NativeActivity实现CanInduceIdle属性
    /// 生成后直接从工具栏拖放即可
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class WaitForInputActivity<T> : NativeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public InArgument<string> InBookMarkName { get; set; }

        public OutArgument<T> OutPutData { get; set; }

        protected override bool CanInduceIdle
        {
            get { return true; }
        }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 并从 Execute 方法返回该值。
        protected override void Execute(NativeActivityContext context)
        {
            // 获取 Text 输入参数的运行时值
            string text = context.GetValue(this.InBookMarkName);
            context.CreateBookmark(text, new BookmarkCallback(MethodCallBack));
        }

        private void MethodCallBack(NativeActivityContext context, Bookmark bookmark, object value)
        {
            context.SetValue(OutPutData, (T)value);
        }
    }
}
