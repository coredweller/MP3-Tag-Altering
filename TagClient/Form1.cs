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

            var tagProcessor = new TagProcessor( filePaths.ToList() );
            FillFiles( tagProcessor );

            //DetermineSuccess( success );
        }

        IList<DomainObjects.ShowFile> _ShowFiles;
        private void FillFiles( TagProcessor tagProcessor ) {
            _ShowFiles = tagProcessor.OrderByFileName( );

            if ( _ShowFiles == null || !_ShowFiles.Any() ) return;

            lstFiles.Items.Clear();

            foreach ( var file in _ShowFiles ) {
                lstFiles.Items.Add( file.FileName );
            }
        }

        //private void DetermineSuccess( Success success ) {
        //    switch ( success ) {
        //        case Success.True:
        //            MessageBox.Show( "You have successfully updated the track ordering!" );
        //            break;
        //        case Success.False:
        //            MessageBox.Show( "There was a problem updating the track ordering." );
        //            break;
        //        case Success.Continue:
        //            MessageBox.Show( "Enter a show date to match the show up with your files. " );
        //            break;
        //    }
        //}

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
            MoveFile( true );
        }

        private void btnMoveDown_Click( object sender, EventArgs e ) {
            MoveFile( false );
        }

        private void MoveFile( bool up ) {

            //If there is nothing selected then get out of here
            if ( lstFiles.SelectedItem == null ) {
                MessageBox.Show( "Please select a file to move up" );
                return;
            }

            var index = lstFiles.SelectedIndex;
            int moddedIndex;

            if ( up ) {
                //If the direction to move is UP
                //   AND first item is selected then do nothing because it cannot move up
                if ( index == 0 ) return;
                moddedIndex = index - 1;
            }
            else {
                //If the direction to move is DOWN
                // AND the last item is selected then do nothing because it cannot move down
                if ( index == lstFiles.Items.Count - 1 ) return;
                moddedIndex = index + 1;
            }

            var value = lstFiles.SelectedItem;

            //Have to do it by index in case the same song happens twice which is very common with Phish
            lstFiles.Items.RemoveAt( index );
            //Then insert the same item at the index above or below its current location based on variable up
            lstFiles.Items.Insert( moddedIndex, value );
            lstFiles.SetSelected( moddedIndex, true );
        }

        private void btnSetOrder_Click( object sender, EventArgs e ) {

        }

    }

    //public enum Success
    //{
    //    //The request has failed
    //    False = 0,

    //    //The request has succeeded
    //    True = 1,

    //    //There are more steps needed to determine whether it was a success yet.
    //    Continue = 2
    //}
}
