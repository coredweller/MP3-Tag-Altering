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
    public class Tag_3v1 : Tag
    {
        private ID3.ID3v1Frame.ID3v1 _Tag { get; set; }

        //private mp3info.mp3info _MP3Info { get; set; }
        //private mp3info.ID3v1 _Tag { get; set; }

        public Tag_3v1( string filePath ) : base(filePath) {
            _Tag = _Info.ID3v1Info;

            if ( !_Tag.HaveTag) throw new ArgumentException( "This file does not have a valid ID3v1 tag. This program requires that minimum level of identification." );
        }

        public override bool UpdateTrack( int track ) {
            bool success = false;

            try {
                _Info.ID3v1Info.TrackNumber = Convert.ToByte(track);
                _Info.Save();
                success = true;
            }
            catch ( Exception ex ) {
                success = false;
            }

            return success;
        }
    }
}
