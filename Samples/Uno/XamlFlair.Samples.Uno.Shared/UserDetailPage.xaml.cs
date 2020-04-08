using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using XamlFlair.Samples.Uno.SampleData;

namespace XamlFlair.Samples.Uno
{
	public sealed partial class UserDetailPage : Page
	{
		public User CurrentUser
		{
			get => (User)GetValue(CurrentUserProperty);
			set => SetValue(CurrentUserProperty, value);
		}

		public static readonly DependencyProperty CurrentUserProperty =
			DependencyProperty.Register(
				nameof(CurrentUser),
				typeof(User),
				typeof(UserDetailPage),
				new PropertyMetadata(null));

		public UserDetailPage()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);

			CurrentUser = e.Parameter as User;
		}

		private void PlacesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender is ListViewBase lvb && lvb.SelectedItem is Place place)
			{
				var selected = new SelectedDetails()
				{
					User = CurrentUser,
					Place = place
				};

				App.RootFrame?.Navigate(typeof(PlacePage), selected, new DrillInNavigationTransitionInfo());
			}
		}

		private void NavigateBackButton_Click(object sender, RoutedEventArgs e)
		{
			App.RootFrame?.Navigate(typeof(UsersPage), null, new DrillInNavigationTransitionInfo());
		}
	}
}
