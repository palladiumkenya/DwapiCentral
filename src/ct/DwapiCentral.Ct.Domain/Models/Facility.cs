using System.ComponentModel.DataAnnotations;
using DwapiCentral.Contracts.Manifest;

namespace DwapiCentral.Ct.Domain.Models
{
    public class Facility : IFacility
    {
        [Key]
        public int Code { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;

        public Facility(int code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
