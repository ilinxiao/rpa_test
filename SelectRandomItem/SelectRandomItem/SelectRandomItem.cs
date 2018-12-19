using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using System.Activities;
using System.ComponentModel;
    
namespace SelectRandomItem
{
    public class SelectRandomItem : CodeActivity
    {
        //参数类型，输入或者输出，或者两者都是
        [Category("Input")]
        //必须参数
        [RequiredArgument]
        public InArgument<String> FullText { get; set; }

        [Category("Input")]
        //参数默认值
        [DefaultValue("\r\n")]
        public InArgument<String> Separator { get; set; }

        [Category("Output")]
        public OutArgument<String> ChoiceResult { get; set; }

        /**
         * Execute是CodeActivity必须重载的方法
         * 处理逻辑根据Separator指定的分割符分割FullText
         * 然后随机返回其中一个
         * 
         **/
        protected override void Execute(CodeActivityContext context)
        {
            //所有的参数取值、赋值都是通过context
            var fullText = FullText.Get(context);
            var separator = Separator.Get(context);
            string[] items = Regex.Split(fullText, separator, RegexOptions.IgnoreCase);
            Random ran = new Random();
            var result = items[ran.Next(items.Length)];
            ChoiceResult.Set(context, result);
        }
    }

}
