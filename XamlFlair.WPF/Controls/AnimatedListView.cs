using System.Windows;
using System.Windows.Controls;
using XamlFlair.Extensions;

namespace XamlFlair.Controls
{
	public class AnimatedListView : ListView
	{
		private bool _isFirstItemContainerLoaded; // when first item container has been generated

		public AnimatedListView()
		{
			// Pass an action to reset the "container loaded" flag when the ItemsSource changes
			this.RegisterListEvents(() => _isFirstItemContainerLoaded = false);
		}

		protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
		{
			base.PrepareContainerForItemOverride(element, item);
			this.PrepareContainerForItemOverrideEx(element, ref _isFirstItemContainerLoaded);
		}
	}
}