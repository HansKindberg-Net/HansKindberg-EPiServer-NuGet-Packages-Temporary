using System;
using System.IO;
using log4net;
using log4net.Config;

namespace HansKindberg.EPiServer.NuGet.Packages.Tests.WebApplication
{
	public class Global : System.Web.HttpApplication
	{
		#region Fields

		private static readonly ILog _log;

		#endregion

		#region Constructors

		static Global()
		{
			if(!LogManager.GetRepository().Configured)
				XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));

			_log = LogManager.GetLogger(typeof(Global));

			if(_log.IsInfoEnabled)
				_log.Info("Global - static constructor");
		}

		#endregion

		#region Methods

		protected void Application_Start(object sender, EventArgs e)
		{
			if(_log.IsInfoEnabled)
				_log.Info("Application start");
		}

		#endregion
	}
}