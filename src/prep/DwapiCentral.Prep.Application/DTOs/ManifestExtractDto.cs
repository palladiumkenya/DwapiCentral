using DwapiCentral.Prep.Domain.Models;

namespace DwapiCentral.Prep.Application.DTOs
{
    public class ManifestExtractDto
    {
        public Manifest Manifest { get; set; }
        public bool AllowSnapshot { get; set; }
    }
}
