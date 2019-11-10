using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestLinq
{
    class Linq
    {
        public static void TestLinq()
        {
            // 查询出数组中的奇数并排序
            int[] ints = { 5, 2, 0, 66, 4, 32, 7, 1 };

            // 使用LINQ和Lambda表达式查询数组中的偶数
            //int intEvens = ints.Where(p=>p%2 == 0).ToArray();
            int[] intEvens = ints.Where(p => p % 2 == 0).ToArray();
            // 使用LINQ和Lambda表达式查询数组中的奇数
            int[] intOdds = ints.Where(p => p % 2 != 0).ToArray();


            // 输出
            Console.WriteLine("偶数：" + string.Join(",", intEvens));
            Console.WriteLine("奇数：" + string.Join(",", intOdds));

            Console.ReadKey();
        }
    }
}
