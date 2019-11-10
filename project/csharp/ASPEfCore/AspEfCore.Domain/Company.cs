using System;
using System.Collections.Generic;
using System.Text;

namespace AspEfCore.Domain
{
    public class Company
    {
        public Company()
        {
            CityCompanies = new List<CityCompany>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EstablishDate { get; set; }
        public string LegalPerson { get; set; }

        public IList<CityCompany> CityCompanies { get; set; }
    }
}
