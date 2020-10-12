using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Test
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Sex { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember]
        public bool status { get; set; }
    }

    public class InitalUserData
    {
        private int _DataCount;
        public InitalUserData(int count)
        {
            _DataCount = count;
        }

        public List<User> IntialData()
        {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<User> users = new List<User>();
            for (int i = 0; i < _DataCount;i++)
            {
                string sexstring;
                string name="";
                int age = new Random().Next(0, 80);
                int sexi = new Random().Next(0, 1);
                for (int j=0;j<=2;j++)
                {
                    int a = new Random().Next(0, 20902);
                    char tmpchar;
                    a = '\u4e00' + a;
                    tmpchar = (char)a;
                    string ns1 = tmpchar.ToString();
                    name = name + ns1;
                }
             
                if (sexi == 0)
                    sexstring = "女";
                else
                    sexstring = "男";

                User user = new User { ID = i, Name = name, Sex = sexstring,Age=age };
                users.Add(user);
            }
            stopwatch.Stop();
            Console.WriteLine($"took {stopwatch.Elapsed.TotalMilliseconds} Milliseconds to generated {_DataCount} users");
            return users;
        }

    }
}
