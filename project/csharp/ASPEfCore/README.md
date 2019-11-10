---

## Questions

1. exception
   - log
   ```log
   System.InvalidOperationException: Cannot resolve scoped service 'Microsoft.EntityFrameworkCore.DbContextOptions`1[AspEfCore.Data.Models.CampDbContext]' from root provider.
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteValidator.ValidateResolution(Type serviceType, IServiceScope scope, IServiceScope rootScope)
   ```
   - solution
   ```c#
   // Program.cs: temp solution
   .UseDefaultServiceProvider(options => { options.ValidateScopes = false; })
   ```
