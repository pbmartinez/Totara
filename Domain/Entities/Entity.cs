using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Entity : BaseGenericEntity<Guid>
    {

        public override bool IsTransient() => Id == Guid.Empty;

        public override void GenerateIdentity()
        {
            if(IsTransient())
                Id = Guid.NewGuid();
        }

    }
}
