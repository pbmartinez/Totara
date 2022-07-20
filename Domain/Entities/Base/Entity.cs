using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities.Base
{
    public class Entity : BaseGenericEntity<int>
    {

        public override bool IsTransient() => Id <= 0;

        public override void GenerateIdentity()
        {
            //if(IsTransient())
            //    Id = Guid.NewGuid();
        }

    }
}
