using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using StructureMap;
using Core;
using Data;
using Core.Infrastructure;
using Core.Infrastructure.Logging;

namespace TagClient
{
    static class Program
    {
        private readonly static LogWriter _Log = new LogWriter();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            log4net.Config.XmlConfigurator.Configure();
            _Log.Write( "Before IOC Startup" );
            IocBootstrap.SetupIoc();
            _Log.Write( "After IOC Startup" );

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
