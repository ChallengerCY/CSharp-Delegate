using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDelegate
{
    delegate int AddNewNum(int a);
    delegate void Delegates(int a);

    class Program
    {
        static int num = 10;

        static void Main(string[] args)
        {
            //C#中的委托机制（匿名的方式去调用各种方法，面向对象的，类型是安全的）
            //先定义一个委托，将需要调用的方法，像传递参数一样传递到委托中，之后使用委托的方法类似于方法的使用
            //通过委托调用静态方法
            AddNewNum add = new AddNewNum(Addnum);
            add(25);
            Console.WriteLine(num);
            //c#中委托调用实例化方法,调用多个方法
            NewMethod newMethod = new NewMethod();
            AddNewNum add_ = new AddNewNum(newMethod.addNum);
            add_(35);
            Console.WriteLine(newMethod.num);

            AddNewNum multiply = new AddNewNum(newMethod.multiplyNum);
            multiply(2);
            Console.WriteLine(newMethod.num);

            //Multi-casting delegates
            //c#中的委托是有一个调用列表的，一个委托可以调按照顺序调用多个方法
            //调用类中的静态方法(类名.方法名)
            Delegates d1 = new Delegates(TestMultiCastingDelegates.M1);
            d1(-1);
            Delegates d2 = new Delegates(TestMultiCastingDelegates.M2);
            d2(-2);
            Delegates d3 = d1 + d2;
            d3(-1);
            TestMultiCastingDelegates tcd=new TestMultiCastingDelegates();
            Delegates d4 = new Delegates(tcd.M3);
            d3 += d4;
            d3(10);

            d3 += d1;
            d3(20);
            //可以使用-=来删除委托，如果调用了相同的函数，则从最后添加的函数开始删除
            d3 -= d1;
            d3(30);
            //删除委托可以不管委托列表的顺序，只要委托中有这个方法就可以删除，若果没有对应的委托，或者委托列表为空，删除（-=）也不会报错，但是如果调用列表中没有方法再去调用委托会报错
            d3 -= d1;
            d3(10);

            Console.ReadLine();
        }

        public static int Addnum(int add)
        {
            num += add;
            return num;
        }

        class NewMethod
        {
            public int num = 10;

            public int addNum(int add)
            {
                num += add;
                return num;
            }

            public int multiplyNum(int multiply)
            {
                num *= multiply;
                return num;
            }
        }

        class TestMultiCastingDelegates {
            public static void  M1(int a)
            {
                Console.WriteLine("M1: {0}",a);
            }
            public static void M2(int a)
            {
                Console.WriteLine("M2: {0}", a);
            }

            public void M3(int a)
            {
                Console.WriteLine("M3: {0}", a);
            }

        }
    }
}
