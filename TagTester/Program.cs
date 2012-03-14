using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TagAltering;

namespace TagTester
{
    class Program
    {
        static void Main( string[] args ) {

            var tag = new Tag("C:\\Projects\\MP3 Tag Altering Project\\Tag Altering Library\\TagAltering\\TagAltering\\TestFiles\\Sample.mp3");
            tag.UpdateTrack( 11 );

            Console.ReadKey();
        }
    }
}
