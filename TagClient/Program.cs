using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//using Structu
//using StructureMap;
//using Core;
//using Core.Infrastructure;

namespace TagClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new Form1() );
        }

        //public static class IocBootstrap
        //{
        //    public static void SetupIoc() {
                
                ///LEFT OFF HERE

        //        //Ioc.InitializeWith(new DependencyResolverFactory(new DependencyResolver());

        //        //ObjectFactory.AssertConfigurationIsValid();
        //    }
        //}
    }
}
