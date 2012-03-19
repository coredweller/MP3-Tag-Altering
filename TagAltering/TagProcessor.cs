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
        public IList<Tag> Files { get; private set; }

        public TagProcessor( IList<string> filePaths ) {
            _OriginalFilePaths = filePaths;
            Files = OrderByFileName();
        }
        
        /// <summary>
        /// OrderProtocol Name Ordering Scheme
        /// </summary>
        public bool UpdateTrackOrdering(IList<Tag> files) {
            bool success = true;
            var count = 1;

            //Go through each track and update the Track accordingly
            foreach ( var file in files ) {
                success = success && file.UpdateTrack( count );
                count++;
            }

            return success;
        }

        private IList<Tag> OrderByFileName( ) {
            var showFiles = new List<Tag>();
            
            foreach ( var path in _OriginalFilePaths ) {
                showFiles.Add( new Tag( path ) );
            }

            return showFiles.OrderBy( x => x.FileName ).ToList();
        }

    }

    public enum OrderProtocol
    {
        Name = 0,
        Database = 1
    }
}
