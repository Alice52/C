using EntityFramework.DbContextScope.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspEfCore.Data.Models
{
    public partial class CampDbContext : DbContext, IDbContext
    {
    }
}
