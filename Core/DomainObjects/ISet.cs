using System;
using System.Collections.Generic;

namespace DomainObjects
{
    public interface ISet : IEntity
    {
        Guid SetId { get; set; }

        short? SetNumber { get;  }

        bool Encore { get;  }

        string Notes { get;  }

        Guid? ShowId { get; set; }

    }
}
