using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Business.Models.Base
{
    public abstract class Entity
    {
        protected Entity()
        {
            if (Id != Guid.Empty) Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
