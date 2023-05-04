using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Application.Interfaces.Repository.Ct
{

    public interface IActionRegisterRepository : IRepository<ActionRegister>
    {
        Task<bool> Clear(int siteCode);
        void CreateAction(List<ActionRegister> actionRegisters);
    }
}
