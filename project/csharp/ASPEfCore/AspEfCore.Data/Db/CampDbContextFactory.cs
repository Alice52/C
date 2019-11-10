using EntityFramework.DbContextScope.Interfaces;
using System;
using System.Linq;

namespace AspEfCore.Data.Db
{
    public class CampDbContextFactory : IDbContextFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CampDbContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TDbContext CreateDbContext<TDbContext>() where TDbContext : class, IDbContext
        {
            var type = typeof(TDbContext);

            var constructors = type.GetConstructors();
            //if (constructors.Length != 1)
            //{
            //    throw new InvalidOperationException($"The type '{type.Name}' should have only one constructor.");
            //}

            var constructor = constructors.LastOrDefault();
            var parameters =
                constructor.GetParameters().Select(pi => _serviceProvider.GetService(pi.ParameterType)).ToArray();

            return (TDbContext)Activator.CreateInstance(type, parameters);
        }
    }
}
