using System;
using System.Collections.Generic;

namespace AspEfCore.Data.Models
{
    public partial class CityCompany
    {
        public int CityId { get; set; }
        public int CompanyId { get; set; }

        public virtual City City { get; set; }
        public virtual Company Company { get; set; }
    }
}
