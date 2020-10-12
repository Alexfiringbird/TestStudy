using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;


namespace Test
{
    class TestConvert
    {
        private RandomNumberGenerator _rng;
        
        public TestConvert()
        {
            _rng = new RNGCryptoServiceProvider();
        }
        public void Run()
        {
            var data = new byte[sizeof(int)];
            Console.WriteLine(_rng.GetType().ToString());
            _rng.GetBytes(data);

            for(int i=0; i < data.Length; i++)
            {
                Console.WriteLine( $" 第{i}位为: {data[i]},二进制：{Convert.ToString(data[i], 2)}");
            }


            Console.WriteLine(Convert.ToString(BitConverter.ToInt32(data, 0),2));
            Console.WriteLine(BitConverter.ToInt32(data, 0));
            int b = BitConverter.ToInt32(data, 0) & (int.MaxValue - 1);
            Console.WriteLine(b);

            var randUint = BitConverter.ToUInt32(data, 0);
            Console.WriteLine(randUint / (uint.MaxValue + 1.0));

            Console.ReadKey();

        }
    }
}
