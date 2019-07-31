using System;

namespace TestDeledate
{
    public delegate int Call(int num1, int num2);//第一步：定义委托类型
    //定义委托
    delegate string lookMe(string s);
    delegate void xmethod(int a, int b);

    class SimpleMath
    {
        // 乘法方法
        public int Multiply(int num1, int num2)
        {
            System.Console.WriteLine("Multiply");
            return num1 * num2;
        }

        // 除法方法
        public int Divide(int num1, int num2)
        {
            System.Console.WriteLine("Divide");
            return num1 / num2;
        }
    }

    class Test
    {
        static void Main(string[] args)
        {
            test();

            test2();

            test3();

            test4();

            Console.ReadKey();
        }

        protected static void test()
        {
            Call objCall;//第二步：声明委托变量
            // Math 类的对象
            SimpleMath objMath = new SimpleMath();
            // 第三步：初始化委托变量，将方法与委托关联起来
            //objCall = new Call(objMath.Multiply);
            objCall = objMath.Multiply;


            objCall += objMath.Divide;//向委托增加一个方法
            //objCall -=  objMath.Divide;//向委托减去一个方法

            // 调用委托实例,先执行objMath.Multiply，然后执行objMath.Divide
            int result = objCall(5, 3);
            System.Console.WriteLine("结果为 {0}", result);
        }

        protected static void test2()
        {
            //匿名委托
            lookMe lm = delegate (string name) { return "----- " + name + "-----"; };

            string str = lm("zack");
            System.Console.WriteLine(str);
        }

        protected static void test3()
        {
            xmethod process = (int a, int b) => { System.Console.WriteLine(a * b); };

            process(2,3);
        }


        protected static void test4() => System.Console.WriteLine("sw");

        private Action a = () => { Console.WriteLine("Hello");};
    }
}
