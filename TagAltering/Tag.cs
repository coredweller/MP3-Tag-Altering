using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainObjects;

namespace TagAltering
{
    public abstract class Tag : ITag
    {
        protected ID3.ID3Info _Info { get; set; }
        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        
        protected const string TRACK_TEXT_FRAME = "TRCK";

        public Tag( string filePath ) {
            if ( string.IsNullOrEmpty( filePath ) ) throw new ArgumentException( "There is no file path given" );

            _Info = new ID3.ID3Info( filePath, true );

            FileName = ParseFileName( filePath );
            FilePath = filePath;
        }

        protected string ParseFileName( string path ) {
            var splits = path.Split( '\\' );

            if ( splits == null || !splits.Any() ) throw new ArgumentException( "ShowFiles can ONLY have valid full paths passed in!" );

            return splits.Last();
        }

        public abstract bool UpdateTrack( int track );
    }
}