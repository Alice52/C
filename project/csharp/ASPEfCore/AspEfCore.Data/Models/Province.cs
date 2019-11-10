using System;
using System.Collections.Generic;

namespace AspEfCore.Data.Models
{
    public partial class Province
    {
        public Province()
        {
            City = new List<City>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }

        public virtual IList<City> City { get; set; }
    }
}
