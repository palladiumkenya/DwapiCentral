using DwapiCentral.Ct.Application.DTOs.Extract;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Shared.Domain.Model.Common;
using System;
using System.Collections.Generic;

namespace DwapiCentral.Ct.Application.Interfaces.Repository.Base
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
