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
        /// Updates the order of the tracks to be in the correct linear order
        /// </summary>
        /// <param name="orderProtocol">The way to order the file paths before applying track ordering</param>
        /// <returns>Whether or not the operation succeeded</returns>
        public bool UpdateTrackOrder( OrderProtocol orderProtocol ) {

            //If there are no files then get out of here
            if ( _FilePaths == null || _FilePaths.Count <= 0 ) return false;

            //Based on the Order Protocol use a different ordering scheme TODO: Replace with strategy pattern
            switch ( orderProtocol ) {
                case OrderProtocol.Name:
                    return ProcessByFileName();
                case OrderProtocol.Database:
                    return ProcessByDatabase();
                default: return false;
            }
        }

        /// <summary>
        /// OrderProtocol Database Ordering Scheme
        /// </summary>
        /// <returns></returns>
        private bool ProcessByDatabase() {
            //LEFT OFF HERE


            return false;
        }

        /// <summary>
        /// OrderProtocol Name Ordering Scheme
        /// </summary>
        private bool ProcessByFileName() {
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
