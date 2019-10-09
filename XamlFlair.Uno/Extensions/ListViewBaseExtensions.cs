using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Hosting;
using XamlFlair.Uno.Logging;

namespace XamlFlair.Extensions
{
	internal static class ListViewBaseExtensions
	{
		internal static void Initialize(this ListViewBase lvb)
		{
			lvb.Loaded += AnimatedListViewBase_Loaded;

			void AnimatedListViewBase_Loaded(object sender, RoutedEventArgs e)
			{
				lvb.Loaded -= AnimatedListViewBase_Loaded;

				// Perform validations on the ListViewBase
				Animations.Validate(lvb);

				// Perform validations on the ListViewBase items
				Animations.ValidateListViewBase(lvb);
			}
		}

		internal static void RegisterListEvents(this ListViewBase lvb, Action itemsSourceChangedAction)
		{
			// Observe the Unloaded of the control (including Error or Completed)
			var unloaded = (lvb as FrameworkElement).Events().Unloaded;

			lvb.Observe(ItemsControl.ItemsSourceProperty)
				.TakeUntil(unloaded)
				// Animate only if the attached property is true 
				.Where(_ => Animations.GetAnimateOnItemsSourceChange(lvb))
				.Subscribe(
				_ => itemsSourceChangedAction?.Invoke(),
				ex => Animations.Logger.ErrorException($"Error on subscription to changes of the {nameof(ListViewBase.ItemsSource)} property of {nameof(ListViewBase)}", ex));
		}

		internal static void OnApplyTemplateEx(this ListViewBase lvb)
		{
			// VERY IMPORTANT to clear any existing transitions in order to avoid item flickering 
			lvb.ItemContainerTransitions?.Clear();
		}

		internal static void PrepareContainerForItemOverrideEx<TItem>(this ListViewBase lvb, DependencyObject element, Func<(int firstVisibleIndex, int lastVisibleIndex)> getIndicesFunc, ref bool isFirstItemContainerLoaded)
			where TItem : SelectorItem
		{
			// Don't animate using this override if "on loaded" animations is false
			if (!Animations.GetAnimateOnLoad(lvb))
			{
				return;
			}

			// Exit the element is not a SelectorItem or if no animations are specified
			if (!(element is TItem item) || Animations.GetItems(lvb) == null)
			{
				return;
			}

			lvb.AnimateItems<TItem>(item, getIndicesFunc, ref isFirstItemContainerLoaded);
		}

		internal static void AnimateItems<TItem>(this ListViewBase lvb, SelectorItem item, Func<(int firstVisibleIndex, int lastVisibleIndex)> getIndicesFunc, ref bool isFirstItemContainerLoaded)
			where TItem : SelectorItem
		{
			var settings = Animations.GetItems(lvb);

			if (settings == null)
			{
				return;
			}

			if (!isFirstItemContainerLoaded)
			{
				isFirstItemContainerLoaded = true;

				item.Loaded += OnContainerLoaded;

				// At this point, the index values are all ready to use.
				void OnContainerLoaded(object _, RoutedEventArgs __)
				{
					item.Loaded -= OnContainerLoaded;

					var (firstVisibleIndex, lastVisibleIndex) = getIndicesFunc();

					AnimateVisibleItems<TItem>(lvb, settings, firstVisibleIndex, lastVisibleIndex);
				}
			}
		}

		internal static void AnimateVisibleItems<TItem>(this ListViewBase lvb, AnimationSettings settings, int firstVisibleIndex, int lastVisibleIndex)
			where TItem : SelectorItem
		{
			// Make sure to retrieve the GetInterElementDelay value
			var interElementDelay = Animations.GetInterElementDelay(lvb);
			var top = firstVisibleIndex;
			var bottom = lastVisibleIndex;

			for (var index = top; index <= bottom; index++)
			{
				if (lvb.ContainerFromIndex(index) is TItem item)
				{
					AnimateVisibleItem(lvb, item, settings, index, interElementDelay);
				}
			}
		}

		private static void AnimateVisibleItem(ListViewBase lvb, SelectorItem item, AnimationSettings settings, int index, double interElementDelay)
		{
			var scroller = lvb?.FindDescendant<ScrollViewer>();
			var indexFromVisibleTop = lvb.IndexFromContainer(item) - scroller.VerticalOffset;

			// Create a clone of 'settings'
			var itemSettings = new AnimationSettings().ApplyOverrides(settings);

			itemSettings.Delay += indexFromVisibleTop * interElementDelay;

			Animations.RunAnimation(item, settings: itemSettings);
		}
	}
}