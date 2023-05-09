using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Interfaces.Repository
{

    public interface IActionRegisterRepository : IRepository<ActionRegister>
    {
        Task<bool> Clear(int siteCode);
        void CreateAction(List<ActionRegister> actionRegisters);
    }
}
