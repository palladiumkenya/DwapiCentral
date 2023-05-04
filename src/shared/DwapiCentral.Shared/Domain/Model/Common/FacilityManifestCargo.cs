using DwapiCentral.Shared.Domain.Entities.Ct;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Domain.Model.Common
{
    public class FacilityManifestCargo : Entity
    {
        public string Items { get; set; }
        public Guid FacilityManifestId { get; set; }
        public CargoType CargoType { get; set; } = CargoType.Patient;

        public FacilityManifestCargo()
        {
        }

        public FacilityManifestCargo(string items, Guid facilityManifestId) : this()
        {
            Items = items;
            FacilityManifestId = facilityManifestId;
        }

        public FacilityManifestCargo(string items, Guid facilityManifestId, CargoType cargoType) : this(items, facilityManifestId)
        {
            CargoType = cargoType;
        }

        public override string ToString()
        {
            return Items;
        }
    }
}
