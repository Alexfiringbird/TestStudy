using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Unicode;
using System.Web;

namespace Test
{
    public class TestSerialize_Deserialize
    {
        Stopwatch stopwatch = new Stopwatch();
        
        public void Run()
        {
            //自定义序列化测试
            SerWorker.SerializeItem();
            SerWorker.DeserializeItem();

            //Json 序列化测试
            InitalUserData userData = new InitalUserData(100000);
            List<User> users= userData.IntialData();
            #region Test of DataContractJsonSerializer
            stopwatch.Start();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(User));
            MemoryStream ms = new MemoryStream();
            foreach (User u in users)
            {   
                js.WriteObject(ms, u);
            }
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms, Encoding.UTF8);
            string jsusr = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            //Console.WriteLine(jsusr);
            stopwatch.Stop();
           
            #endregion

            #region Test of Newtonsoft.Json
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            string jsondata = JsonConvert.SerializeObject(users);
            //Console.WriteLine(jsondata);
            stopwatch2.Stop();

            Console.WriteLine($"DataContractJsonSerializer take time:{stopwatch.Elapsed.TotalMilliseconds} Milliseconds");
            Console.WriteLine($"Newtonsoft take Time {stopwatch2.Elapsed.TotalMilliseconds} Milliseconds ");

            #endregion
            Console.ReadKey();
        }

    }

    public class SerWorker
    {
        public static void SerializeItem()
        {

            IFormatterConverter converter = new FormatterConverter();
            IFormatter formatter = new BinaryFormatter();
            SeDeClass seDeClass = new SeDeClass();
            SeDeClass t = new SeDeClass();
            SeDeTestClass seDeTestClass = new SeDeTestClass();

            
            StreamingContext context = new StreamingContext(StreamingContextStates.All, "foo");
            IFormatter formatter1 = new BinaryFormatter(null, context);
            
            seDeTestClass._id = 0;
            
            seDeTestClass.MyValue = "Just do a Test";
            seDeClass._id = 0;
            seDeClass.MyValue = "Just do a Test";

            FileStream fileStream1 = new FileStream("Test1.txt", FileMode.Create);
            FileStream fileStream2 = new FileStream("Test2.txt", FileMode.Create);
            FileStream fileStream3 = new FileStream("Test3.txt", FileMode.Create);
            formatter.Serialize(fileStream1, seDeClass);
            formatter.Serialize(fileStream2, seDeTestClass);
            formatter1.Serialize(fileStream3, seDeClass);

            fileStream1.Close();
            fileStream2.Close();
            fileStream3.Close();

        }

        public static void DeserializeItem()
        {
            IFormatter formatter = new BinaryFormatter();
            StreamingContext context = new StreamingContext(StreamingContextStates.File, "foo");
            IFormatter formatter1 = new BinaryFormatter(null, context);

            FileStream fileStream1 = new FileStream("Test1.txt", FileMode.Open);
            FileStream fileStream2 = new FileStream("Test2.txt", FileMode.Open);
            FileStream fileStream3 = new FileStream("Test3.txt", FileMode.Open);

            SeDeClass seDeClass = (SeDeClass)formatter.Deserialize(fileStream1);
            SeDeTestClass seDeTestClass = (SeDeTestClass)formatter.Deserialize(fileStream2);
            SeDeClass seDeClass2 = (SeDeClass)formatter1.Deserialize(fileStream3);

            Console.WriteLine($"for Test1 ,Value is:{seDeClass.MyValue}, Id is :{seDeClass._id}, Test Method value is:{ seDeClass._testmethodvalue}");
            Console.WriteLine($"for Test2 ,Value is:{seDeTestClass.MyValue}, Id is :{seDeTestClass._id}");
            Console.WriteLine($"for Test3 ,Value is:{seDeClass2.MyValue}, Id is :{seDeClass2._id}, Test Method value is:{ seDeClass2._testmethodvalue}");
        }
    }

    [Serializable]
    public class SeDeTestClass
    {
        public int _id;
        private string _propvalue;

        public string MyValue
        {
            get { return _propvalue; }
            set { _propvalue = value; }
        }
    }

    [Serializable]
    public class SeDeClass:ISerializable
    {
        private string _propvalue;
        public int _id;
        public string _testmethodvalue;
        public SeDeClass()
        {

        }
        //反序列化
        [OnDeserializing]
        public void TestDe(StreamingContext context)
        {
            if (context.State == StreamingContextStates.File)
            {
                _testmethodvalue = context.Context.ToString();
            }

            Console.WriteLine("DeSerializing in SeDeClass TestDe method");
        }

        //反序列化，在Void 之后执行
        public SeDeClass(SerializationInfo info,StreamingContext context)
        {
            _propvalue = (string)info.GetValue("props", typeof(string));

            Console.WriteLine("DeSerializing in SeDeClass");
            
        }

        //序列化
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("props", "Test in Serialization", typeof(string));
            Console.WriteLine("Serializing in SeDeClass");
        }

        public string MyValue
        {
            get { return _propvalue; }
            set { _propvalue = value; }
        }
    }


}
