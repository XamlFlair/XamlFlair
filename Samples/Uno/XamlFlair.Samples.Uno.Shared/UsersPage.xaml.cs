using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using XamlFlair.Samples.Uno.SampleData;

namespace XamlFlair.Samples.Uno
{
	public sealed partial class UsersPage : Page
	{
		public List<User> Users { get; } = SampleData.SampleData.Users;

		public UsersPage()
		{
			this.InitializeComponent();
		}

		private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender is ListViewBase lvb && lvb.SelectedItem is User user)
			{
				App.RootFrame?.Navigate(typeof(UserDetailPage), user, new DrillInNavigationTransitionInfo());
			}
		}
	}
}
