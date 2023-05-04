using DwapiCentral.Shared.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Application.Interfaces.Repository.Common
{
    public interface IDocketRepository : IRepository<Docket, string>
    {
        Task<Docket> FindAsync(string docket);
    }
}
