using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Domain.Model.Common;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.DTOs;


namespace DwapiCentral.Shared.Application.Interfaces.Repository.Ct
{
    public interface IFacilityRepository : IRepository<Facility>
    {
        Guid? GetFacilityIdByCode(int code);
        Guid? SyncNew(Facility facility);

        Guid? GetFacilityIdBCode(int code);
        MasterFacility GetFacilityByCode(int code);
        Guid? Sync(Facility facility);

       // IEnumerable<StatsDto> GetFacStats(IEnumerable<Guid> facilityIds);
        //StatsDto GetFacStats(Guid facilityId);
        void Enroll(MasterFacility masterFacility, string emr, bool allowSnapshot);
        void EndSession(Guid session);
        //IEnumerable<HandshakeDto> GetSessionHandshakes(Guid session);
        List<FacilityCacheDto> ReadFacilityCache();
    }
}
