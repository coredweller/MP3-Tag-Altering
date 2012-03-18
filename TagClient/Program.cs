using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using StructureMap;
using Core;
using Data;
using Core.Infrastructure;

namespace TagClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {

            IocBootstrap.SetupIoc();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new Form1() );
        }

        public static class IocBootstrap
        {
            public static void SetupIoc() {
                
                ObjectFactory.Initialize( x =>
                    {
                        x.AddRegistry(new CoreRegistry());
                        x.AddRegistry(new DataRegistry());
                    });

                Ioc.InitializeWith(new DependencyResolverFactory(new DependencyResolver()));

                ObjectFactory.AssertConfigurationIsValid();
            }
        }
    }
}
