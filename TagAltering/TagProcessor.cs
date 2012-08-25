using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainObjects;

namespace TagAltering
{
    public class TagProcessor
    {
        private IList<string> _OriginalFilePaths { get; set; }
        public IList<Tag_3v1> Files_3v1 { get; private set; }
        public IList<Tag_3v2> Files_3v2 { get; private set; }

        public TagProcessor( IList<string> filePaths ) {
            _OriginalFilePaths = filePaths;
            OrderByFileName();
        }
        
        /// <summary>
        /// OrderProtocol Name Ordering Scheme
        /// </summary>
        public bool Update3v1TrackOrdering(IList<Tag_3v1> files) {
            bool success = true;
            var count = 1;

            //Go through each track and update the Track accordingly
            foreach ( var file in files ) {
                success = success && file.UpdateTrack( count );
                count++;
            }

            return success;
        }

        /// <summary>
        /// OrderProtocol Name Ordering Scheme
        /// </summary>
        public bool Update3v2TrackOrdering(IList<Tag_3v2> files) {
            bool success = true;
            var count = 1;

            //Go through each track and update the Track accordingly
            foreach ( var file in files ) {
                success = success && file.UpdateTrack( count );
                count++;
            }

            return success;
        }

        private void OrderByFileName( ) {
            Files_3v1 = new List<Tag_3v1>();
            Files_3v2 = new List<Tag_3v2>();

            foreach ( var path in _OriginalFilePaths ) {
                Files_3v1.Add( new Tag_3v1( path ) );
                Files_3v2.Add( new Tag_3v2( path ) );
            }

            Files_3v1 = Files_3v1.OrderBy( x => x.FileName ).ToList();
            Files_3v2 = Files_3v2.OrderBy( x => x.FileName ).ToList();
        }

    }

    public enum OrderProtocol
    {
        Name = 0,
        Database = 1
    }
}
