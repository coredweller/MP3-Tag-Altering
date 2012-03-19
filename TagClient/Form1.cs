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
using Core.Services;
using Core.Infrastructure;

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

            var success = ProcessOrderProtocol( filePaths );

            DetermineSuccess( success );
        }

        /// <summary>
        /// Top level controller to decide which Order Protocol to follow
        /// </summary>
        /// <param name="filePaths">The file paths being used for this session</param>
        /// <returns>The status of the user's request</returns>
        private Success ProcessOrderProtocol( string[] filePaths ) {
            var protocol = OrderProtocol.Name;

            if ( rdoDatabase.Checked ) protocol = OrderProtocol.Database;

            var tagProcessor = new TagProcessor( filePaths.ToList() );

            //Based on the Order Protocol use a different ordering scheme TODO: Replace with strategy pattern
            switch ( protocol ) {
                case OrderProtocol.Name:
                    return tagProcessor.ProcessByFileName() == true ? Success.True : Success.False;
                case OrderProtocol.Database:
                    //If FilFiles failes then return a failure
                    if ( !FillFiles( tagProcessor ) ) return Success.False;

                    //Otherwise have the user continue
                    return Success.Continue;
            }

            return Success.False;
        }

        IList<DomainObjects.ShowFile> _ShowFiles;
        private bool FillFiles( TagProcessor tagProcessor ) {
            _ShowFiles = tagProcessor.GetShowFiles( tagProcessor.GetFilePaths() );

            if ( _ShowFiles == null || !_ShowFiles.Any() ) return false;

            lstFiles.Items.Clear();

            foreach ( var file in _ShowFiles ) {
                lstFiles.Items.Add( file.FileName );
            }

            return true;
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

            var showService = Ioc.GetInstance<IShowService>();
            var setList = showService.GetSetList( showDate );

            if ( setList == null ) {
                MessageBox.Show( "There is no show in the database for that date" );
                return;
            }

            lstDatabase.DataSource = setList.Select( x => x.SongName ).ToList();
        }

        private void btnMoveUp_Click( object sender, EventArgs e ) {
            //If there is nothing selected then get out of here
            if ( lstFiles.SelectedItem == null ) {
                MessageBox.Show( "Please select a file to move up" );
                return;
            }

            //If the first item is selected then do nothing because it cannot move up
            if ( lstFiles.SelectedIndex == 0 ) return;

            var index = lstFiles.SelectedIndex;
            var value = lstFiles.SelectedItem;

            //Have to do it by index in case the same song happens twice which is very common with Phish
            lstFiles.Items.RemoveAt( index );

            lstFiles.Items.Insert( index - 1, value );
        }

        private void btnMoveDown_Click( object sender, EventArgs e ) {

        }

        private void btnSetOrder_Click( object sender, EventArgs e ) {

        }

    }

    public enum Success
    {
        //The request has failed
        False = 0,

        //The request has succeeded
        True = 1,

        //There are more steps needed to determine whether it was a success yet.
        Continue = 2
    }
}
