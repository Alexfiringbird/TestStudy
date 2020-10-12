using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection.Metadata;

namespace Test
{
    public class TestExpression
    {
        
        
        public void Run()
        {
            ParameterExpression para1 = Expression.Parameter(typeof(int), "x");
            ParameterExpression para2 = Expression.Parameter(typeof(int), "y");
            //组装表达式树
            var paras = new ParameterExpression[] { para1, para2 };
            BinaryExpression body = Expression.Add(para1, para2);
            var expression = Expression.Lambda(body, paras);
            //编译
            var func = expression.Compile() as Func<int,int,int>;
            //调用
            var res = func(1, 2);

            // $ C# 6.0 中添加对string.format()的简化
            Console.WriteLine($"表达式树的输出结果：(1+2)=>{res}");
            Console.ReadKey();

            Console.WriteLine("====表达式树part2====");

            ParameterExpression paraName = Expression.Parameter(typeof(string), "Name");
            ParameterExpression paraScore = Expression.Parameter(typeof(int), "Score");
            ParameterExpression paraStandard = Expression.Parameter(typeof(int), "Standard");

            ParameterExpression paraDesc = Expression.Parameter(typeof(string), "Desc");


        }

        public string GetScore(string Name, int score, int standard)
        {
            string desc = "";
            if (score > standard)
            {
                desc = "及格";
            }
            else if (score == standard)
            {
                desc = "刚刚及格";
            }
            else
            {
                desc = "未及格";
            }
            return Name + desc;
        }
   
    }



}
