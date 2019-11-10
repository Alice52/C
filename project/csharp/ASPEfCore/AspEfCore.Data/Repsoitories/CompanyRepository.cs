using AspEfCore.Data.Db;
using AspEfCore.Data.Models;
using EntityFramework.DbContextScope.Interfaces;

namespace AspEfCore.Data.Repsoitories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(IAmbientDbContextLocator dbContextLocator) : base(dbContextLocator)
        {
        }
    }
}
