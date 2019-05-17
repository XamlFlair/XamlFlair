using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace XamlFlair.Samples.UWP
{
	public sealed partial class StartPage : Page
	{
		public StartPage()
		{
			this.InitializeComponent();
		}

		private void StartButton_Click(object sender, RoutedEventArgs e)
		{
			(Window.Current.Content as Frame)?.Navigate(typeof(UsersPage), null, new DrillInNavigationTransitionInfo());
		}
	}
}