using System;
using System.Collections.Generic;
using System.Text;

namespace TestEntityFramework
{
    class program
    {
        static void Main(string[] args)
        {
            //using (var context = new BookDb())
            //{
            //    Console.WriteLine("数据库中有{0}本书", context.Books.Count());
            //}
            Console.WriteLine("---------------");
            using (var context=new EFDbContext())
            {
                //var result = context.Person.Add(new Person { UserName = "ssiao", Password = 123456 });
                //var result = context.Person.AddAsync(new Person { UserName = "-ssiao", Password = 123456 }).Result;
                //Console.WriteLine(result);
                //var userInfoList = from p in context.Person select p;

                context.SaveChanges();
            }
        }
    }
}


