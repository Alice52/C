using System;
using System.Collections.Generic;

namespace AspEfCore.Data.Models
{
    public partial class City
    {
        public City()
        {
            CityCompany = new HashSet<CityCompany>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string AreaCode { get; set; }
        public int ProvinceId { get; set; }

        public virtual Province Province { get; set; }
        public virtual Major Major { get; set; }
        public virtual ICollection<CityCompany> CityCompany { get; set; }
    }
}
