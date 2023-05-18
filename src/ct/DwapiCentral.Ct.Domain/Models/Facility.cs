using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DwapiCentral.Contracts.Manifest;

namespace DwapiCentral.Ct.Domain.Models
{
    public class Facility : IFacility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
