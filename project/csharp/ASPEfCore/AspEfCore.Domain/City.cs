using System;
using System.Collections.Generic;
using System.Text;

namespace AspEfCore.Domain
{
    public class City
    {
        public City()
        {
            CityCompanies = new List<CityCompany>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string AreaCode { get; set; }

        public Major Major { get; set; }

        public int ProvinceId { get; set; }
        public Province Province { get; set; }
        public IList<CityCompany> CityCompanies { get; set; }
    }
}
