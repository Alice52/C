using EntityFramework.DbContextScope;
using EntityFramework.DbContextScope.Interfaces;
using System.Data;

namespace AspEfCore.Data.Db
{
    public interface ICampDbContextScopeFactory
    {
        ICampDbContextScope Create(string transactionUser,
            DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting);

        ICampDbContextScope CreateWithReadCommittedTransaction(string transactionUser);
        ICampDbContextScope CreateWithReadCommittedTransaction(long transactionUserId);
    }

    public class CampDbContextScopeFactory : ICampDbContextScopeFactory
    {
        protected readonly IAmbientDbContextLocator _dbContextLocator;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public CampDbContextScopeFactory(
            IAmbientDbContextLocator dbContextLocator,
            IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextLocator = dbContextLocator;
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public ICampDbContextScope Create(string transactionUser, DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting)
        {
            var dbContextScope = _dbContextScopeFactory.Create(joiningOption);

            return new CampDbContextScope(dbContextScope, _dbContextLocator);
        }

        public ICampDbContextScope CreateWithReadCommittedTransaction(string transactionUser)
        {
            var dbContextScope = _dbContextScopeFactory.CreateWithTransaction(IsolationLevel.ReadCommitted);

            return new CampDbContextScope(dbContextScope, _dbContextLocator);
        }

        public ICampDbContextScope CreateWithReadCommittedTransaction(long transactionUserId)
        {
            return CreateWithReadCommittedTransaction(transactionUserId.ToString());
        }
    }
}
