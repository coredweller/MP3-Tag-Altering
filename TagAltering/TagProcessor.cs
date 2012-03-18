using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TagAltering
{
    public class TagProcessor
    {
        private IList<string> _OriginalFilePaths { get; set; }
        private IList<string> _FilePaths { get; set; }

        public TagProcessor( IList<string> filePaths ) {
            _OriginalFilePaths = filePaths;
            _FilePaths = filePaths;
        }

        
        /// <summary>
        /// OrderProtocol Name Ordering Scheme
        /// </summary>
        public bool ProcessByFileName() {
            var tuples = new List<Tuple<string, string>>();

            foreach ( var path in _FilePaths ) {
                var splits = path.Split( '\\' );
                var fileName = splits.Last();

                tuples.Add( new Tuple<string, string>( path, fileName ) );
            }

            _FilePaths = tuples.OrderBy( x => x.Item2 ).Select( y => y.Item1 ).ToList();

            bool success = true;
            var count = 1;

            //Go through each track and update the Track accordingly
            foreach ( var file in _FilePaths ) {
                var tag = new Tag( file );
                success = success && tag.UpdateTrack( count );
                count++;
            }

            return success;
        }
    }

    public enum OrderProtocol
    {
        Name = 0,
        Database = 1
    }
}
