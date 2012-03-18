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

            var success = ProcessFiles(filePaths);

            DetermineSuccess( success );
        }

        private Success ProcessFiles(string[] filePaths) {
            var tagProcessor = new TagProcessor( filePaths.ToList() );

            var protocol = OrderProtocol.Name;

            if ( rdoDatabase.Checked ) protocol = OrderProtocol.Database;
            
            //Based on the Order Protocol use a different ordering scheme TODO: Replace with strategy pattern
            switch ( protocol ) {
                case OrderProtocol.Name:
                    return tagProcessor.ProcessByFileName() == true ? Success.True : Success.False;
                case OrderProtocol.Database:
                    return Success.Continue;
                    break;
            }

            return Success.False;
        }

        private void DetermineSuccess( Success success ) {
            switch ( success ) {
                case Success.True:
                    MessageBox.Show( "You have successfully updated the track ordering!" );
                    break;
                case Success.False:
                    MessageBox.Show( "There was a problem updating the track ordering." );
                    break;
                case Success.Continue:
                    MessageBox.Show( "Enter a show date to match the show up with your files. " );
                    break;
            }
        }

        private void btnGetShow_Click( object sender, EventArgs e ) {
            DateTime showDate;
            if ( !DateTime.TryParse( txtShowDate.Text, out showDate ) ) MessageBox.Show( "Please enter a valid date to get the show." );


        }

    }

    public enum Success
    {
        False = 0,
        True = 1,
        Continue = 2
    }
}
