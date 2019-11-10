using System;
using System.Collections.Generic;
using System.Text;

namespace AspEfCore.Domain
{
    public class Province
    {
        public Province()
        {
            Cities = new List<City>();
        }

        // default: Id[autoincreasement] and ProvinceId will be set as Primary Key
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }

        public List<City> Cities { get; set; }
    }
}
