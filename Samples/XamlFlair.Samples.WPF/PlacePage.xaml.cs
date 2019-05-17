using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Blend.SampleData.SampleUsers;

namespace XamlFlair.Samples.WPF
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

		public PlacePage(PlacesItem place)
		{
			this.InitializeComponent();

			CurrentPlace = place;
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