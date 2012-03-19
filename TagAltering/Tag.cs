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
        //private mp3info.mp3info _MP3Info { get; set; }
        private mp3info.ID3v1 _Tag { get; set; }

        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        public long FileSize { get; private set; }
        public long SongLength { get; private set; }


        public Tag( string filePath ) {
            if ( string.IsNullOrEmpty( filePath ) ) throw new ArgumentException( "There is no file path given" );

            FileName = ParseFileName( filePath );
            FilePath = filePath;

            var MP3Info = new mp3info.mp3info( FilePath );

            MP3Info.ReadID3v1();

            if ( !MP3Info.hasID3v1 ) throw new ArgumentException( "This file does not have a valid ID3v1 tag. This program requires that minimum level of identification." );

            _Tag = MP3Info.id3v1;
            FileSize = MP3Info.fileSize;
            SongLength = MP3Info.length;
        }

        private string ParseFileName( string path ) {
            var splits = path.Split( '\\' );

            if ( splits == null || !splits.Any() ) throw new ArgumentException( "ShowFiles can ONLY have valid full paths passed in!" );

            return splits.Last();
        }

        public bool UpdateTrack( int track ) {
            try {
                
                _Tag.Track = track;
                _Tag.updateMP3Tag();

                return true;
            }
            catch ( Exception ex ) {
                //THIS IS A HACK AROUND SOMEONE ELSES POORLY CRAFTED AND POORLY TESTED LIBRARY
                return false;
            }
        }
    }
}
