using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace dev.scan_back.Models
{
    public class ScanDevDatabaseInitializer : CreateDatabaseIfNotExists<ScanDbContext>
    {
        protected override void Seed(ScanDbContext context)
        {
            base.Seed(context);
        }
    }
}