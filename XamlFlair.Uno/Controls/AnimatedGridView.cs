using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XamlFlair.Extensions;

namespace XamlFlair.Controls
{
	public partial class AnimatedGridView : GridView
	{
		private bool _isFirstItemContainerLoaded; // when first item container has been generated
		private ItemsWrapGrid _virtualizedPanel;

		public AnimatedGridView()
		{
			// Pass an action to reset the "container loaded" flag when the ItemsSource changes
			this.RegisterListEvents(() => _isFirstItemContainerLoaded = false);
		}

		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this.OnApplyTemplateEx();
		}

		protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
		{
			base.PrepareContainerForItemOverride(element, item);

			// First populate our local variable for referencing ItemsWrapGrid for the first time.
			if (!_isFirstItemContainerLoaded && _virtualizedPanel == null)
			{
				_virtualizedPanel = ItemsPanelRoot as ItemsWrapGrid;
			}

			this.PrepareContainerForItemOverrideEx<GridViewItem>(
				element,
				() => (_virtualizedPanel.FirstVisibleIndex, _virtualizedPanel.LastVisibleIndex),
				ref _isFirstItemContainerLoaded);
		}
	}
}