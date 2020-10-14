using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    public class MyGeneric<T> 
    {
        private T[] array;
        public MyGeneric(int size)
        {
            array = new T[size + 1];
        }

        public T GetItem(int index)
        {
            return array[index];
        }

        public void SetItem(int index, T value)
        {
            array[index] = value;
        }

    }

    public class TestGeneric
    {
        public void Run()
        {
            MyGeneric<int> myIntGeneric = new MyGeneric<int>(5);
            MyGeneric<char> myCharGeneric = new MyGeneric<char>(5);

            for (int c = 0; c < 5; c++)
            {
                myIntGeneric.SetItem(c, c * 2);
                myCharGeneric.SetItem(c, (char)(c + 63));
            }

            
            for (int c = 0; c<5; c++)
            {
                Console.WriteLine("MyIntGeneric:"+myIntGeneric.GetItem(c));
                Console.WriteLine("MyCharGeneric:"+myCharGeneric.GetItem(c));
            }
            Console.Read();


        }
    }
}
