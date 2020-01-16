using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XamlFlair.Extensions;

namespace XamlFlair.Controls
{
	public class AnimatedListView : ListView
	{
		private bool _isFirstItemContainerLoaded; // when first item container has been generated
		private ItemsStackPanel _virtualizedPanel;

		public AnimatedListView()
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

			// First populate our local variable for referencing ItemsStackPanel for the first time.
			if (!_isFirstItemContainerLoaded && _virtualizedPanel == null)
			{
				_virtualizedPanel = ItemsPanelRoot as ItemsStackPanel;
			}

			this.PrepareContainerForItemOverrideEx<ListViewItem>(
				element,
				() => (_virtualizedPanel.FirstVisibleIndex, _virtualizedPanel.LastVisibleIndex),
				ref _isFirstItemContainerLoaded);
		}
	}
}