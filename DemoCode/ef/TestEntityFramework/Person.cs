using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestEntityFramework
{
    public class Person
    {
        [Key]
        public string UserName { get; set; }
        public int Password { get; set; }
    }
}
