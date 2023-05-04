using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DwapiCentral.Shared.Application.Interfaces.Ct;

namespace DwapiCentral.Shared.Domain.Entities.Ct
{
    public abstract class Entity : IEntity
    {
        [Key]
        public virtual Guid Id { get; set; }
        [Column(Order = 100)]
        public virtual string Emr { get; set; }
        [Column(Order = 101)]
        public virtual string Project { get; set; }
        [Column(Order = 102)]
        public virtual bool Voided { get; set; }
        [Column(Order = 103)]
        public virtual bool Processed { get; set; }


        protected Entity(Guid id)
        {
            Id = id;
        }
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
