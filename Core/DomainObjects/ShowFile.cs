using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainObjects
{
    public class ShowFile
    {
        public string FullPath { get; set; }
        public string FileName { get; set; }

        public ShowFile( string path ) {
            FullPath = path;

            var splits = path.Split( '\\' );

            if ( splits == null || !splits.Any() ) throw new ArgumentException( "ShowFiles can ONLY have valid full paths passed in!" );

            FileName = splits.Last();
        }
    }
}
