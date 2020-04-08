using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace XamlFlair.Samples.Uno.Controls
{
	public sealed partial class PageHeader : BaseUserControl
	{
		public Visibility BackButtonVisibility
		{
			get => (Visibility)GetValue(BackButtonVisibilityProperty);
			set => SetValue(BackButtonVisibilityProperty, value);
		}

		public static readonly DependencyProperty BackButtonVisibilityProperty =
			DependencyProperty.Register(
				nameof(BackButtonVisibility),
				typeof(Visibility),
				typeof(PageHeader),
				new PropertyMetadata(Visibility.Visible));

		public event RoutedEventHandler Click;

		public PageHeader()
		{
			this.InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Click?.Invoke(sender, e);
		}
	}
}
