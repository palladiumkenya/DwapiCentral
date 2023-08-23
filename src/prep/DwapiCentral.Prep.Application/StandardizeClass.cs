using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Application
{
    public class StandardizeClass<TExtract>
    {
        private readonly List<TExtract> extracts;
        private Guid manifestId;

        public StandardizeClass(List<TExtract> extracts, Guid manifestId)
        {
            this.extracts = extracts;
            this.manifestId = manifestId;

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



            dynamicExtract.ManifestId = manifestId;

        }
    }
}
