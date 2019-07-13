using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLinq
{
    // 定义委托
    delegate bool FindEven(int item);

    class IntExtension
    {
        public static int[] where(int[] array, FindEven dele)
        {
            int[] result = new int[4];
            int i = 0;
            foreach (int item in array)
            {
                if (dele(item))
                {
                    result[i] = item;
                    i++;
                }
            }

            return result;
        }
    }
    class Program
    {
        public static void TestDelegate()
        {
            // 查询出数组中的奇数并排序
            int[] ints = { 5, 2,1, 0, 66, 4, 32, 7, 1 };

            //delegate(int item){return item % 2 != 0;}表示委托的实现
            List<int> list = IntExtension.where(ints, delegate (int item)
                    {
                        return item % 2 != 0;
                    }).ToList();
            Console.WriteLine(string.Join(",", list));
            // 正序排序
            list.Sort();
            Console.WriteLine(string.Join(",", list));
            // 反转
            list.Reverse();
            // 输出
            Console.WriteLine(string.Join(",", list));

            Console.ReadKey();
        }
    }
}