using AspEfCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspEfCore.Data.Db
{
    public interface IReadDbFacade
    {
        IQueryable<City> Cities { get; }

        IQueryable<CityCompany> CityCompanies { get; }

        IQueryable<Company> Companies { get; }

        IQueryable<Major> Majors { get; }

        IQueryable<Province> Provinces { get; }
    }
}
