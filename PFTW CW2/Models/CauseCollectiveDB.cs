using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PFTW_CW2.Models
{
    public class CauseCollectiveDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Cause> Causes { get; set; }
    }

    public class CauseCollectiveDBInitializer : DropCreateDatabaseAlways<CauseCollectiveDB>
    {
        protected override void Seed(CauseCollectiveDB context)
        {
            base.Seed(context);
        }
    }
}