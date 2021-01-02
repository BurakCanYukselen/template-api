using System;

namespace API.Base.Data.RequestModels.Base
{
    public class AbstractEntity
    {
        public Guid Id { get; set; }

        public AbstractEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}