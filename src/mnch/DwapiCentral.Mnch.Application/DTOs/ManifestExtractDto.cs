using DwapiCentral.Mnch.Domain.Model;

namespace DwapiCentral.Mnch.Application.DTOs
{
    public class ManifestExtractDto
    {
        public Manifest Manifest { get; set; }
        public bool AllowSnapshot { get; set; }
    }
}
