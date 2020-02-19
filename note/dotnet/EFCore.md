## EFCore

### Database Migration

#### Visual Studio

1.  model => databse

    ```shell
    ## pre-requirement, which can install by command or edit .csproj file
    1. Microsoft.EntityFrameworkCore.Tools
    2. Microsoft.EntityFrameworkCore.Design

    ## step
    1. set parent project as startup
    2. Open Package Manager Console: Tools -> NuGet Package  Manager -> Package Manager Console.
    3. choose the project contains CustomDbContext as Default Project
    4. do migration and update database

    ## sample: get-help entityframeworkcore
    1. dotnet restore # after add to .csproj
    2. DbContext register
    3. Add-Migration MIGRATIONNAME[UNIQUE]
    4. Update-Database -Verbose
    ```

2.  databse => model

    ```shell
    # syntax
    Scaffold-DbContext [-Connection] <String> [-Provider] <String> [-OutputDir <String>] [-Context <String>] [-Schemas <String>] [-Tables <String>] [-DataAnnotations] [-Force] [-Project <String>] [-StartupProject <String>] [-Environment <String>] [<CommonParameters>]

    # sample1: name[connection], TBALE_NAME, CustomContext, Models ara all vars
    Scaffold-DbContext name=demo Microsoft.EntityFrameworkCore.SqlServer -Force -t TBALE_NAME  -OutputDir Models -ContextDir Db -Context CampDbContext -Verbose

    # sample2:
    Scaffold-DbContext "Server=.\;Database=DB_NAME;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -Force -t TBALE_NAME  -OutputDir Models -ContextDir Db -Context CampDbContext -Verbose
    ```

#### VS Code

1. model => databse

   ```shell
   1. open contains CustomDbContext project
   2. write code
       - Model: entity model
       - Configuration: entity create code
       - CustomDbContext: add Models and point out datasource in OnConfiguring
   3. dotnet ef migrations add MIGRATIONNAME --context=CustomDbContext # add add-velo-mapping
   4. dotnet ef database update --context=CustomDbContext # this command will create DB and tables
   5. dotnet ef migrations script -o Custom_Directory # this will generate sql
   ```

2. databse => model

   ```shell
   # name, TBALE_NAME, CampDbContext, Models ara all vars
   dotnet ef dbcontext scaffold "Server=T0xx-111\SQLEXPRESS;Database=DATABASE_NAME;User Id=sa; Password=sa" Microsoft.EntityFrameworkCore.SqlServer -f -t Gaming_TableCountedEventWinLoss -t Gaming_TableJunketWinLoss -t Gaming_TableWinLoss  --output-dir Models --context-dir Db --context CustomContext --verbose
   ```

#### Tools

1. [package manage ef-core](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/index?tabs=dotnet-core-cli)

|        VSCode Comand         |     VS Comand      |                      Description                       |
| :--------------------------: | :----------------: | :----------------------------------------------------: |
|   dotnet ef migrations add   |   Add-Migration    |                 Adds a new migration.                  |
|                              |   Drop-Database    |                  Drops the database.                   |
|                              |   Get-DbContext    |        Gets information about a DbContext type.        |
|                              |  Remove-Migration  |              Removes the last migration.               |
| dotnet ef dbcontext scaffold | Scaffold-DbContext | Scaffolds a DbContext and entity types for a database. |
| dotnet ef migrations script  |  Script-Migration  |        Generates a SQL script from migrations.         |
|                              |  Update-Database   |     Updates the database to a specified migration.     |

### Syntax

1. Insert

   ```c#
   //_compDbContext.Add(province);
   _compDbContext.AddRange(province, company);
   _compDbContext.Province.AddRange(
       new List<Province>
       {
               province2, province
       });
   _compDbContext.SaveChanges();
   ```

2. Query

   - LINQ will not execute immediately, just executed when use

   ```c#
   // aslo asyc version
   foreach
   Find // if dbContext has this Id Onject, EFCore will return it without qury DB
   ToList
   First FirstOrDefault
   Single SingleOrDefault
   Last LastOrDefault // Used with OderBy
   Count LongCount
   Min Max Average
   ```

   ```c#
   // SP
   //DbSet.FromSql("SP");
   var provinces = _compDbContext.Province.FirstOrDefault(x=>x.Name.Equals("BeiJing"));
   // without OrderBy will query all data, EFCore will set it to Memery and select top1
   // with OrderBy will select top1 in db
   var provinces0 = _compDbContext.Province
       .OrderBy(x=>x.Name)
       .LastOrDefault(x=>x.Name.Equals("BeiJing"));

   var provinces1 = _compDbContext.Province.Where(x => x.Name.Contains("BeiJing")).ToList();
   var provinces2 = _compDbContext.Province.Where(x => EF.Functions.Like(x.Name, "%BeiJing%")).ToList();

   // will not execute query, just foreach and ToList, Find,
   var provinces3 = from province in _compDbContext.Province
                       where province.Name.Equals("BeiJing")
                       select province;

   var provinces4 = (from province in _compDbContext.Province
                       where province.Name.Equals("BeiJing")
                       select province).ToList();

   _compDbContext.SaveChanges();
   ```

3. Update: same idea with JPA Object status[4]

   ```c#
   var province = _compDbContext.Province.FirstOrDefault();
   province.Population += 2000;

   _compDbContext.SaveChanges();
   ```

4. Delete

   ```c#
   var province = _compDbContext.Province.FirstOrDefault();
   _compDbContext.Province.Remove(province);
   _compDbContext.SaveChanges();
   ```

5. SP

   ```c#
   // execute comand
   _compDbContext.Database.ExecuteSqlCommand("SP");
   // execute select
   _dbSet.FromSql("SP");
   ```

6. Relation

   ```c#
   // 1. insert
   var province = new Province()
   {
       Population = 3000000,
       Name = "BeiJing",
       City = new List<City>()
       {
           new City()
           {
               AreaCode="1024",
               Name= "pudong"
           }
       }
   };
   _compDbContext.Province.Add(province);

   var province2 = _compDbContext.Province.FirstOrDefault();
   province2.City.Add(
       new City()
       {
           AreaCode = "1025",
           Name = "yangpu"
       });

   _compDbContext.SaveChanges();
   ```

   ```c#
   // 2. query
   // Eager Loading[Include]: query relative data immediately
   // Query Projections: difine wanted result, then execute query
   // Explicit Loading: some data in memery, and want loading relative data in db
   // Lazy Loading:

   // Eager Loading[Include]: query relative data immediately
   var province = _compDbContext.Province
       .Include(x => x.City)
       .ThenInclude(x=>x.CityCompany)
       .ThenInclude(x=>x.Company)
       .ToList();

   var city = _compDbContext.City
       .Include(x => x.Province)
       .Include(x=>x.Major)
       .Include(x=>x.CityCompany)
       .Where(x=>!x.Name.Equals(null))
       .ToList();

   // Query Projections: difine wanted result, then execute query
   var provinceInfo  = QueryProvinceInfo();
   var provinceInfo1 = _compDbContext.Province
       .Select(x => new
       {
           x.Name,
           x.Id,
           City = x.City.Where(y => y.Name.Equals("BeiJing")).ToList()
       }
   ).ToList();

   // this query result has no city info
   var provinces = _compDbContext.Province
       .Where(x => x.City.Any(y => y.Name.Equals("BeiJing")))
       .ToList();
   ```

   ```c#
   // 3. update
   var provinceInfo = _compDbContext.Province
       .Include(x => x.City)
       .First(x => x.City.Any());

   var city = provinceInfo.City[0];
   city.Name += "updated";

   // offline
   // _compDbContext.City.Update(city); EFCore will update all relative data
   // Entry will ignore relatiive data
   // _compDbContext2.Entry(city).State = EntityState.Modified;
   ```

   ```c#
   var provinceInfo = _compDbContext.Province
       .Include(x => x.City)
       .First(x => x.City.Any());

   var city = provinceInfo.City[0];
   _compDbContext.City.Remove(city);

   _compDbContext.SaveChanges();
   ```

### SeekData

1. generate

   ```c#
   // seed data: should privide Primary Key
   modelBuilder.Entity<City>().HasData(
       // new City() { Id = 1, Name = "BeiJing", AreaCode = "1024", Major= new Major() { Id = 1, FirstName = "zack" } }); //error
       new City() { Id = 1, Name = "BeiJing", AreaCode = "1024" });

   modelBuilder.Entity<Major>().HasData(
       new Major() { Id = 1, FirstName = "zack", CityId = 1 },
       new { Id =2, FirstName = "zack", CityId = 1 } // if no PK Id);
   ```

---

## Reference

1. EFCore migration: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/index?tabs=dotnet-core-cli
