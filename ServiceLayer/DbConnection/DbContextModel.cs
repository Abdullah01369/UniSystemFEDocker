using Microsoft.EntityFrameworkCore;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DbConnection
{
    public class DbContextModel : DbContext
    {
        public DbContextModel(DbContextOptions<DbContextModel> options) : base(options)
        {
        }

        public DbSet<UserInfo> UserInfos { get; set; }

    }
}
