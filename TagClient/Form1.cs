﻿using System;
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
using Core.Infrastructure.Logging;
using Core.DomainObjects;

namespace TagClient
{
    public partial class Form1 : Form
    {
        private TagProcessor _TagProcessor;
        private LogWriter _Log = new LogWriter();

        public Form1() {
            InitializeComponent();
        }

        private void btnProcess_Click( object sender, EventArgs e ) {
            var folder = txtProcess.Text;

            if ( string.IsNullOrEmpty( folder ) ) {
                MessageBox.Show( "Please pick a folder" );
                return;
            }

            try {

                //Find only MP3s because the TagProcessor only has MP3 capabilities
                var filePaths = Directory.GetFiles( folder, "*.mp3" );

                if ( filePaths == null || filePaths.Count() <= 0 ) {
                    MessageBox.Show( "There are no files in that folder" );
                    return;
                }

                _TagProcessor = new TagProcessor( filePaths.ToList() );
                FillFiles();
            }
            catch ( Exception ex ) {
                _Log.WriteFatal( "There was an error creating the Tag Processor.  The files have an issue with their paths. WITH EXCEPTION:" + ex.Message );
                MessageBox.Show( "There is an issue with these files.  Please choose a valid folder of MP3 files." );
                return;
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

        private void btnSetOrder_Click( object sender, EventArgs e ) {
            var setList_3v1 = new List<Tag_3v1>();
            var setList_3v2 = new List<Tag_3v2>();

            bool set_3v1 = true;
            bool set_3v2 = true;

            var files_3v1 = _TagProcessor.Files_3v1;
            var files_3v2 = _TagProcessor.Files_3v2;

            if ( files_3v1 == null || !files_3v1.Any() ) set_3v1 = false;
            if ( files_3v2 == null || !files_3v2.Any() ) set_3v2 = false;

            foreach ( var fileName in lstFiles.Items ) {
                if ( set_3v1 ) {
                    setList_3v1.Add( files_3v1.SingleOrDefault( x => x.FileName == fileName.ToString() ) );
                }

                if ( set_3v2 ) {
                    setList_3v2.Add( files_3v2.SingleOrDefault( x => x.FileName == fileName.ToString() ) );
                }
            }

            var success = _TagProcessor.Update3v1TrackOrdering( setList_3v1 );
            var finalSuccess = success && _TagProcessor.Update3v2TrackOrdering( setList_3v2 );

            DetermineSuccess( finalSuccess );
        }

        private void FillFiles() {
            IEnumerable<string> files;

            if ( _TagProcessor.Files_3v2 == null || !_TagProcessor.Files_3v2.Any() ) {
                if ( _TagProcessor.Files_3v1 == null || !_TagProcessor.Files_3v1.Any() ) return;
                else files = _TagProcessor.Files_3v1.Select( x => x.FileName );
            }
            else {
                files = _TagProcessor.Files_3v2.Select( x => x.FileName );
            }

            lstFiles.Items.Clear();

            foreach ( var file in files ) {
                lstFiles.Items.Add( file );
            }
        }

        private void DetermineSuccess( bool success ) {
            if ( success )
                MessageBox.Show( "You have successfully updated the track ordering!" );
            else
                MessageBox.Show( "There was a problem updating the track ordering." );

        }

        #region Moving Files

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

        #endregion
        
        private void btnChooseFolder_Click( object sender, EventArgs e ) {
            if ( folderBrowserDialog.ShowDialog() == DialogResult.OK ) {
                txtProcess.Text = folderBrowserDialog.SelectedPath;
            }
        }

    }
}