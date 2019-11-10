using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using AspEfCore.Data.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Hangfire;

namespace AspEfCore.Web.Controllers
{
    [Route("api/EfCoreControllerBasic")]
    [ApiController]
    public class EfCoreControllerBasic : ControllerBase
    {
        private readonly CampDbContext _compDbContext;
        private readonly IBackgroundJobClient _backgroungJobClient;

        public EfCoreControllerBasic(CampDbContext compDbContext, IBackgroundJobClient backgroungJobClient)
        {
            // Inject
            _compDbContext = compDbContext;
            _backgroungJobClient = backgroungJobClient;
        }




        [HttpPost("save")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Save succuessfully.")]
        public ActionResult Save()
        {
            var province = new Province()
            {
                Population = 3000000,
                Name = "BeiJing"
            };

            var province1 = new Province()
            {
                Population = 1000000,
                Name = "jiangsu",
            };

            var province2 = new Province()
            {
                Population = 2000000,
                Name = "shanghai",
            };
            var company = new Company()
            {
                Name = "Augumentum",
                EstablishDate = DateTime.Now,
                LegalPerson = "zack",
            };

            //_compDbContext.Add(province);
            _compDbContext.AddRange(province, company);

            _compDbContext.Province.AddRange(new List<Province>
            {
                 province2, province
            });

            _compDbContext.SaveChanges();
            return Ok();
        }

        [HttpGet("query")]
        [SwaggerResponse((int)HttpStatusCode.OK, "query succuessfully.")]
        public ActionResult query()
        {
            // SP
            //DbSet.FromSql("SP");

            var provinces = _compDbContext.Province.FirstOrDefault(x => x.Name.Equals("BeiJing"));
            // without OrderBy will query all data, EFCore will set it to Memery and select top1
            // with OrderBy will select top1 in db
            var provinces0 = _compDbContext.Province
                .OrderBy(x => x.Name)
                .LastOrDefault(x => x.Name.Equals("BeiJing"));

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

            return Ok();
        }

        [HttpPut("query")]
        [SwaggerResponse((int)HttpStatusCode.OK, "update succuessfully.")]
        public ActionResult update()
        {

            var province = _compDbContext.Province.FirstOrDefault();

            if(province != null)
            {
                province.Population += 2000;
                _compDbContext.SaveChanges();
            }


            _compDbContext.SaveChanges();

            return Ok();
        }



        [HttpDelete("query")]
        [SwaggerResponse((int)HttpStatusCode.OK, "delete succuessfully.")]
        public ActionResult delete()
        {

            var province = _compDbContext.Province.FirstOrDefault();

            if (province != null)
            {
                _compDbContext.Province.Remove(province);
                _compDbContext.SaveChanges();
            }

            _compDbContext.Database.ExecuteSqlCommand("SP");

            _compDbContext.SaveChanges();

            return Ok();
        }



        // relations
        [HttpPost("insertRelations")]
        [SwaggerResponse((int)HttpStatusCode.OK, "insertRelations succuessfully.")]
        public ActionResult insertRelations()
        {
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

            return Ok();
        }

        // relations
        [HttpGet("getRelations")]
        [SwaggerResponse((int)HttpStatusCode.OK, "getRelations succuessfully.")]
        public ActionResult getRelations()
        {
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


            return Ok();
        }


        // relations
        [HttpPut("updateRelations")]
        [SwaggerResponse((int)HttpStatusCode.OK, "updateRelations succuessfully.")]
        public ActionResult updateRelations()
        {
            var provinceInfo = _compDbContext.Province
                 .Include(x => x.City)
                 .First(x => x.City.Any());
            
            var city = provinceInfo.City[0];
            city.Name += "updated";

            // offline
            // _compDbContext.City.Update(city); EFCore will update all relative data
            // Entry will ignore relatiive data
            // _compDbContext2.Entry(city).State = EntityState.Modified;

            _compDbContext.SaveChanges();

            return Ok();
        }

        // relations
        [HttpPut("deleteRelations")]
        [SwaggerResponse((int)HttpStatusCode.OK, "deleteRelations succuessfully.")]
        public ActionResult deleteRelations()
        {
            var provinceInfo = _compDbContext.Province
                 .Include(x => x.City)
                 .First(x => x.City.Any());

            var city = provinceInfo.City[0];
            _compDbContext.City.Remove(city);

            _compDbContext.SaveChanges();
            return Ok();
        }


        private List<dynamic> QueryProvinceInfo()
        {
            var provinceInfo = _compDbContext.Province.Select(x =>
                    new
                    {
                        x.Name,
                        x.Id,
                        x.City.Count
                    }
            ).ToList<dynamic>();

            return provinceInfo;
        }
    }
}
