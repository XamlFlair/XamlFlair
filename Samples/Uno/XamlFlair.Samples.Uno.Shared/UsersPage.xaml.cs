using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
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
	}
}
