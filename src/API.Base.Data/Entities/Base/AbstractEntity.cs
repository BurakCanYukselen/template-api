using System;

namespace API.Base.Data.Entities.Base
{
    public abstract class AbstractEntity
    {
        public AbstractEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}