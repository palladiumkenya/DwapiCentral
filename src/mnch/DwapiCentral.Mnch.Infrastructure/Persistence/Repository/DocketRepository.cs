﻿using CSharpFunctionalExtensions;
using DwapiCentral.Mnch.Domain.Model;
using DwapiCentral.Mnch.Domain.Repository;
using DwapiCentral.Mnch.Infrastructure.Persistence.Context;
using DwapiCentral.Shared.Exceptions;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Infrastructure.Persistence.Repository
{
    public class DocketRepository : IDocketRepository
    {
        private readonly MnchDbContext _context;

        public DocketRepository(MnchDbContext context)
        {
            _context = context;
        }

        public  Task<Docket?> GetDocketId(string docket)
        {

            
                var Result =  _context.Dockets
                       .Include(x => x.Subscribers)
                       .AsTracking()
                       .FirstOrDefaultAsync(x => x.Id == docket);

                return Result;

        }
    }
}
