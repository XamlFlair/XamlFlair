using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Blend.SampleData.SampleUsers;
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
	public sealed partial class UsersPage : Page
	{
		public SampleUsers SampleUsers => App.SampleUsers;

		public UsersPage()
		{
			this.InitializeComponent();
		}

		private void PlacesGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender is ListViewBase lvb && lvb.SelectedItem is PlacesItem place)
			{
				(Window.Current.Content as Frame)?.Navigate(typeof(PlacePage), place, new DrillInNavigationTransitionInfo());
			}
		}
	}
}
