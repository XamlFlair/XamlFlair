using Microsoft.Extensions.Logging;
using Serilog;
using System.Windows;

namespace XamlFlair.Samples.WPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			// Setup the Serilog logger
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.Debug()
				.CreateLogger();

			// Initalie the XamlFlair loggers using the LoggerFactory (with Serilog support)
			XamlFlair.Animations.InitializeLoggers(new LoggerFactory().AddSerilog());
		}
	}
}
