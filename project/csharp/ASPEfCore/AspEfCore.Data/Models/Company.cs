using System;
using System.Collections.Generic;

namespace AspEfCore.Data.Models
{
    public partial class Company
    {
        public Company()
        {
            CityCompany = new HashSet<CityCompany>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EstablishDate { get; set; }
        public string LegalPerson { get; set; }

        public virtual ICollection<CityCompany> CityCompany { get; set; }
    }
}
