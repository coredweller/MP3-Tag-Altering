using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mp3info;

namespace TagAltering
{
    /// <summary>
    /// Thin wrapper over the classes in the Tag folder
    /// </summary>
    public class Tag
    {
        private string _FileName { get; set; }
        private mp3info.mp3info _TagInfo { get; set; }

        public Tag( string fileName ) {
            _FileName = fileName;

            if ( string.IsNullOrEmpty( _FileName ) ) throw new ArgumentException( "There is no file name given" );

            _TagInfo = new mp3info.mp3info( _FileName );

            _TagInfo.ReadID3v1();
        }

        public bool UpdateTrack( int track ) {
            try {
                if ( !_TagInfo.hasID3v1 ) return false;

                var tag = _TagInfo.id3v1;
                tag.Track = track;
                tag.updateMP3Tag();

                return true;
            }
            catch ( Exception ex ) {
                return false;
            }
        }
    }
}
