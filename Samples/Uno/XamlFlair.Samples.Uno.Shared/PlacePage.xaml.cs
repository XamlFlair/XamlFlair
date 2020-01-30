using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using XamlFlair.Samples.Uno.SampleData;

namespace XamlFlair.Samples.Uno
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

		public Place CurrentPlace
		{
			get => (Place)GetValue(CurrentPlaceProperty);
			set => SetValue(CurrentPlaceProperty, value);
		}

		public static readonly DependencyProperty CurrentPlaceProperty =
			DependencyProperty.Register(
				nameof(CurrentPlace),
				typeof(Place),
				typeof(PlacePage),
				new PropertyMetadata(null));


		public PlacePage()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			CurrentPlace = e.Parameter as Place;
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
