using System;
using System.Runtime.Serialization;

namespace Test
{

    class Program
    {
        static void Main(string[] args)
        {
            #region All kinds of my Tests
            // Test Enum  
            // TestEnumClass Tec = new TestEnumClass();

            // Test Delegate
            // TestDelegate Tec = new TestDelegate();

            // Test Expressions
            //TestExpression Tec = new TestExpression();

            //Test Generic
            //TestGeneric Tec = new TestGeneric();

            //Test Attr_Ref
            //TestAttr_Ref Tec = new TestAttr_Ref();

            //Test Convert
            //TestConvert Tec = new TestConvert();
            #endregion

            //TestSerialize_Deserialize Tec = new TestSerialize_Deserialize();

            TestAutofac Tec = new TestAutofac();
            Tec.Run();
        }
    }
}
