using AspEfCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspEfCore.Data.Db
{
    public class ReadDbFacade : IReadDbFacade, IDisposable
    {
        private readonly CampDbContext _readDbContext;

        public ReadDbFacade(CampDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public IQueryable<City> Cities => _readDbContext.City;

        public IQueryable<CityCompany> CityCompanies => _readDbContext.CityCompany;

        public IQueryable<Company> Companies => _readDbContext.Company;

        public IQueryable<Major> Majors => _readDbContext.Major;

        public IQueryable<Province> Provinces => _readDbContext.Province;

        public void Dispose()
        {
            _readDbContext.Dispose();
        }
    }
}
