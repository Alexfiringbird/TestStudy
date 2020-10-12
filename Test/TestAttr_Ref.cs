using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Test
{
    public  class TestAttr_Ref
    {
        
        public object[] attributes;
        public Type mytype;
        public TestAttr_Ref()
        {
            System.Reflection.MemberInfo info = typeof(TestClass);
            attributes = info.GetCustomAttributes(false);
            TestClass testClass = new TestClass();
            testClass.TestInt = 1;
            testClass.TestString = "test world";
            mytype = typeof(TestClass);


        }
   
        public void Run()
        {
            for (int i = 0; i < attributes.Length; i++)
            {
                HelpAttribute helpAttribute = (HelpAttribute)attributes[i];
                Console.WriteLine(helpAttribute.Url);
                Console.WriteLine(helpAttribute.Topic);
                
            }
            Console.ReadKey();

            foreach(MethodInfo m in mytype.GetMethods())
            {
               
                foreach (Attribute a in m.GetCustomAttributes(true))
                {
                    HelpAttribute helpAttribute = a as HelpAttribute;
                    if (helpAttribute != null)
                    {
                        Console.WriteLine($"methodname:{m.Name}");
                        Console.WriteLine($"helpAttribute.Url:{helpAttribute.Url},helpAttribute.Topic:{helpAttribute.Topic} ");
                        Console.ReadKey();
                    }

                }
            }

            foreach (FieldInfo f in mytype.GetFields())
            {

                foreach (Attribute a in f.GetCustomAttributes(true))
                {
                    HelpAttribute helpAttribute = a as HelpAttribute;
                    if (helpAttribute != null)
                    {
                        Console.WriteLine($"methodname:{f.Name}");
                        Console.WriteLine($"helpAttribute.Url:{helpAttribute.Url},helpAttribute.Topic:{helpAttribute.Topic} ");
                        Console.ReadKey();
                    }

                }
            }
        }
    }
    
    [AttributeUsage(AttributeTargets.All)]    
    public class HelpAttribute: System.Attribute
    {
        public readonly string Url;
        private string topic;
        public string Topic
        {
            set { topic = value; }
            get { return topic; }
        }

        public HelpAttribute(string url)
        {
            this.Url = url;
        }

    }

    [HelpAttribute("Test on Attribute & Reflcation",Topic ="Test topic on class")]
    public class TestClass
    {
        [HelpAttribute("www.baidu.com/Test on string property",Topic = "Test Topic on string")]
        public string TestString;

        [HelpAttribute("www.baidu.com/Test on string property", Topic = "Test Topic on int")]
        public int TestInt;

        [HelpAttribute("www.baidu.com/Test on the method", Topic = "Test Topic on Method1")]
        public void TestMethod()
        {

        }

        [HelpAttribute("www.baidu.com/Test on the method", Topic = "Test Topic on Method2")]
        public void TestMethodS()
        {

        }
    }
}
