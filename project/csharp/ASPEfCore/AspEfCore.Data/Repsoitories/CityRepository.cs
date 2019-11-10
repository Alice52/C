using AspEfCore.Data.Db;
using AspEfCore.Data.Models;
using EntityFramework.DbContextScope.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspEfCore.Data.Repsoitories
{
    public class CityRepository: Repository<City>, ICityRepository
    {
        public CityRepository(IAmbientDbContextLocator dbContextLocator) : base(dbContextLocator)
        {
        }
    }
}
