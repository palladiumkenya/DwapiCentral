﻿using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository
{
    public class EnhancedAdherenceCounsellingRepository : IEnhancedAdherenceCounsellingRepository
    {
        private readonly CtDbContext _context;

        public EnhancedAdherenceCounsellingRepository(CtDbContext context)
        {
            _context = context;
        }
        public Task MergeAsync(IEnumerable<EnhancedAdherenceCounsellingExtract> enhancedAdherenceCounsellingExtracts)
        {
            _context.Database.GetDbConnection().BulkMerge(enhancedAdherenceCounsellingExtracts);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
      
    }
}