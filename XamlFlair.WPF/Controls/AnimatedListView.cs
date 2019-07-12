using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using XamlFlair.Extensions;
using XamlFlair.WPF.Logging;

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