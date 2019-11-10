using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AspEfCore.Data.Db
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : class
    {
        //bool DoesExist(long id);

        bool DoesExist(Expression<Func<TAggregateRoot, bool>> matchingCriteria);

        TAggregateRoot GetById(long id);

        IList<TAggregateRoot> GetAll();

        void BulkInsert(IEnumerable<TAggregateRoot> aggregateRoots);

        void Insert(TAggregateRoot aggregateRoot);

        void Update(TAggregateRoot aggregateRoot);

        void InsertAndSave(TAggregateRoot aggregateRoot);

        void Delete(long id);

        void Delete(TAggregateRoot aggregateRoot);

        void BulkDelete(IEnumerable<TAggregateRoot> aggregateRoots);

        void Save();
    }
}
