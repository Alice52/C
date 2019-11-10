using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AspEfCore.Data.Db;
using AspEfCore.Data.Models;
using AspEfCore.Data.Repsoitories;
using EntityFramework.DbContextScope.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AspEfCore.Web.Controllers
{
    [Route("api/EFCoreController")]
    [ApiController]
    public class EFCoreController : Controller
    {
        private readonly IReadDbFacade _readDbFacade;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        private readonly IProvinceRepository _provinceRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICityRepository _cityRepository;

        public EFCoreController(IProvinceRepository provinceRepository
            , ICityRepository cityRepository
            , ICompanyRepository companyRepository
            , IReadDbFacade readDbFacade
            , IDbContextScopeFactory dbContextScopeFactory)
        {
            // Inject
            _dbContextScopeFactory = dbContextScopeFactory;
            _readDbFacade = readDbFacade;

            _provinceRepository = provinceRepository;
            _companyRepository = companyRepository;
            _cityRepository = cityRepository;
        }

        [HttpPost("save")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Save Province succuessfully.")]
        public ActionResult Save()
        {
            var province = new Province()
            {
                Population = 3000000,
                Name = "BeiJing",
            };

            var province1 = new Province()
            {
                Population = 1000000,
                Name = "jiangsu",
            };

            var company = new Company()
            {
                Name = "Augumentum",
                EstablishDate = DateTime.Now,
                LegalPerson ="zack",
            };
           

            using (var dbScope = _dbContextScopeFactory.CreateWithTransaction(IsolationLevel.ReadCommitted))
            {
                _provinceRepository.BulkInsert(new List<Province>
                    {
                        province, province1
                    });

                dbScope.SaveChanges();
            }



            return Ok();
        }

    }
}