using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TagAltering
{
    public class Tag_3v2 : Tag
    {
        private ID3.ID3v2 _Tag { get; set; }
        public string Track { get { return _Tag.TrackNumber; } }

        public Tag_3v2(string filePath) : base(filePath) {
            _Tag = _Info.ID3v2Info;

            if(!_Tag.HaveTag) throw new ArgumentException( "This file does not have a valid ID3v2 tag." );
        }

        public override bool UpdateTrack(int track){
            bool success = false;

            try {
                _Info.ID3v2Info.SetTextFrame( TRACK_TEXT_FRAME, track.ToString() );
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
