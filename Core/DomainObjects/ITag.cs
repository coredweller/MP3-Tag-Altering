using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DomainObjects
{
    public interface ITag
    {
        string FileName { get; }
        string FilePath { get; }

        bool UpdateTrack( int track );
    }
}
