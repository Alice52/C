using System;
using System.Collections.Generic;

namespace AspEfCore.Data.Models
{
    public partial class Major
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; }
        public int Gender { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
