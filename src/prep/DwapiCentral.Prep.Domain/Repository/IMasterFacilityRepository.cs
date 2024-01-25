using DwapiCentral.Prep.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Domain.Repository
{
    public interface IMasterFacilityRepository
    {
        Task<MasterFacility?> GetByCode(int code);
    }
}
