using AspEfCore.Data.Db;
using AspEfCore.Data.Models;
using EntityFramework.DbContextScope.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspEfCore.Data.Repsoitories
{
    public class ProvinceRepository : Repository<Province>, IProvinceRepository
    {
        // Repository's constructor has args, and no no-args constructor
        public ProvinceRepository(IAmbientDbContextLocator dbContextLocator) : base(dbContextLocator)
        {
        }
    }
}
