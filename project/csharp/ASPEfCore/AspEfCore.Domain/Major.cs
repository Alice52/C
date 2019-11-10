using AspEfCore.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspEfCore.Domain
{
    public class Major
    {
        public int Id{ get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDay{ get; set; }
        public Gender Gender { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
    }
}
