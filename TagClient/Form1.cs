using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TagAltering;
using System.IO;

namespace TagClient
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
        }

        private void btnProcess_Click( object sender, EventArgs e ) {
            var folder = txtProcess.Text;

            if ( string.IsNullOrEmpty( folder ) ) {
                MessageBox.Show( "Please pick a folder" );
                return;
            }

            //Find only MP3s because the TagProcessor only has MP3 capabilities
            var filePaths = Directory.GetFiles( folder, "*.mp3" );

            if ( filePaths == null || filePaths.Count() <= 0 ) {
                MessageBox.Show( "There are no files in that folder" );
                return;
            }
            
            var tagProcessor = new TagProcessor(filePaths.ToList());

            var success = tagProcessor.UpdateTrackOrder( OrderProtocol.Name );

            if ( success ) {
                MessageBox.Show( "You have successfully updated the track ordering!" );
            }
            else {
                MessageBox.Show( "There was a problem updating the track ordering." );
            }
        }
    }
}
