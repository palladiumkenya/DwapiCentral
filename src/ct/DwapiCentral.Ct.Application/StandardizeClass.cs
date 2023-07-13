using DwapiCentral.Contracts.Common;
using DwapiCentral.Ct.Application.DTOs.Source;
using DwapiCentral.Ct.Application.Interfaces;
using DwapiCentral.Ct.Domain.Custom;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Models.Stage;
using Infrastracture.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application
{
    public class StandardizeClass<TExtract, TSource>
    {
        private readonly List<TExtract> extracts;
        private readonly TSource sourceBag;

        public StandardizeClass(List<TExtract> extracts, TSource sourceBag)
        {
            this.extracts = extracts;
            this.sourceBag = sourceBag;
        }

        public void StandardizeExtracts()
        {
                extracts.ForEach(x =>
                {
                    StandardizeExtract(x);
                });
            
        }
        private void StandardizeExtract(TExtract extract)
        {
            dynamic dynamicExtract = extract;
            dynamic dynamicSourceBag = sourceBag;

            
            dynamicExtract.LiveSession = dynamicSourceBag.ManifestId;
            dynamicExtract.FacilityId = dynamicSourceBag.FacilityId;
        }
    }
}
