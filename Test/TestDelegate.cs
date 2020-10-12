using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{

    // Func is Added in C# 3.5 
    class TestDelegate
    {
        private delegate string UpLetter(string inletter);
        public string temp = "abc";
        public string temp2 = "";
        public string temp3 = "";
        public string TUpLetter(string inletter)
        {
            return "Uppered by delegate: " + inletter.ToUpper();
        }

        public void AUpLetter(string inletter)
        {
            Console.WriteLine("Uppered by action: " + inletter.ToUpper());
        }
        public void Run()
        {
            //Delegate
            var TestUp = new UpLetter(TUpLetter);
            temp2 = TestUp(temp);

            //Action引用返回类型为void的方法
            Action<string> UpLetterAction = AUpLetter;

            //Func
            Func<string, string> UpLetterFunc = new Func<string, string>(TUpLetter);
            temp3 = UpLetterFunc(temp);

            UpLetterAction(temp);
            Console.WriteLine("Test Delegate: " + temp2);
            Console.WriteLine("Test Func: " + temp3);

            Console.ReadKey();

        }
    }
}
