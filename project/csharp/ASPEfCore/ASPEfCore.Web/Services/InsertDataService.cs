using AspEfCore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspEfCore.Web.Services
{
    public class InsertDataService : IInsertDataService
    {
        private readonly CampDbContext _compDbContext;

        public InsertDataService(CampDbContext compDbContext)
        {
            // Inject
            _compDbContext = compDbContext;
        }

        public Province query()
        {
            var provinces = _compDbContext.Province.FirstOrDefault(x => x.Name.Equals("BeiJing"));
            
            var provinces0 = _compDbContext.Province
                .OrderBy(x => x.Name)
                .LastOrDefault(x => x.Name.Equals("BeiJing"));
 
            return provinces0;
        }
    }
}
