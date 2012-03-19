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
        private IList<string> _FilePaths { get; set; }

        public TagProcessor( IList<string> filePaths ) {
            _OriginalFilePaths = filePaths;
            _FilePaths = filePaths;
        }

        public IList<string> GetFilePaths() {
            return _FilePaths;
        }

        public IList<ShowFile> OrderByFileName( ) {
            var showFiles = GetShowFiles();

            return showFiles.OrderBy( x => x.FileName ).ToList();
        }
        
        /// <summary>
        /// OrderProtocol Name Ordering Scheme
        /// </summary>
        public bool ProcessByFileName() {
            _FilePaths = OrderByFileName( ).Select( y => y.FullPath ).ToList();

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

        private IList<ShowFile> GetShowFiles() {
            var showFiles = new List<ShowFile>();
            
            foreach ( var path in _FilePaths ) {
                showFiles.Add( new ShowFile( path ) );
            }

            return showFiles;
        }
    }

    public enum OrderProtocol
    {
        Name = 0,
        Database = 1
    }
}
