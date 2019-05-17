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
using Windows.UI.Xaml.Navigation;

namespace XamlFlair.Samples.UWP
{
	public sealed partial class PlacePage : Page
	{
		public bool IsPopupShown
		{
			get => (bool)GetValue(IsPopupShownProperty);
			set => SetValue(IsPopupShownProperty, value);
		}

		public static readonly DependencyProperty IsPopupShownProperty =
			DependencyProperty.Register(
				nameof(IsPopupShown),
				typeof(bool),
				typeof(PlacePage),
				new PropertyMetadata(false));

		public PlacesItem CurrentPlace
		{
			get => (PlacesItem)GetValue(CurrentPlaceProperty);
			set => SetValue(CurrentPlaceProperty, value);
		}

		public static readonly DependencyProperty CurrentPlaceProperty =
			DependencyProperty.Register(
				nameof(CurrentPlace),
				typeof(PlacesItem),
				typeof(PlacePage),
				new PropertyMetadata(null));

		public PlacePage()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			CurrentPlace = e.Parameter as PlacesItem;
		}

		private void ShowPopupButton_Click(object sender, RoutedEventArgs e)
		{
			IsPopupShown = true;
		}

        private void ClosePopupButton_Click(object sender, RoutedEventArgs e)
        {
            IsPopupShown = false;
        }
    }
}