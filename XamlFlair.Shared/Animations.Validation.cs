using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using XamlFlair.Extensions;
using XamlFlair.Controls;

#if __WPF__
using System.Windows;
using System.Windows.Controls;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#endif

namespace XamlFlair
{
	public static partial class Animations
	{
		internal static void Validate(FrameworkElement element)
		{
			// Skip validation if a debugger isn't attached
			if (!Debugger.IsAttached)
			{
				return;
			}

			var startWith = element.ReadLocalValue(StartWithProperty);
			var startWithSettings = GetStartWith(element);
			var primary = element.ReadLocalValue(PrimaryProperty);
			var primaryBinding = element.ReadLocalValue(PrimaryBindingProperty);
			var secondary = element.ReadLocalValue(SecondaryProperty);
			var secondaryBinding = element.ReadLocalValue(SecondaryBindingProperty);
			var isIterating = GetIterationBehavior(element) == IterationBehavior.Forever || GetIterationCount(element) > 1;

			AnimationSettings primarySettings = null;
			CompoundSettings primaryCompoundSettings = null;
			AnimationSettings secondarySettings = null;
			CompoundSettings secondaryCompoundSettings = null;

			if (GetPrimary(element) is CompoundSettings compoundPrimary)
			{
				primaryCompoundSettings = compoundPrimary;
			}
			else
			{
				primarySettings = GetPrimary(element) as AnimationSettings;
			}

			if (GetSecondary(element) is CompoundSettings compoundSecondary)
			{
				secondaryCompoundSettings = compoundSecondary;
			}
			else
			{
				secondarySettings = GetSecondary(element) as AnimationSettings;
			}

			// Cannot set an animation for Secondary when specifying values for IterationCount or IterationBehavior.
			if (isIterating && secondary != DependencyProperty.UnsetValue)
			{
				throw new ArgumentException($"Cannot set an animation for {nameof(SecondaryProperty)} when specifying values for {nameof(IterationCountProperty)} or {nameof(IterationBehaviorProperty)}.");
			}

			// Primary must be set first before specifying a value for Secondary.
			if (primary == DependencyProperty.UnsetValue
				&& secondary != DependencyProperty.UnsetValue)
			{
				throw new ArgumentException($"{nameof(PrimaryProperty)} must be set first before specifying a value for {nameof(SecondaryProperty)}.");
			}

			// PrimaryBinding was set wtihout a corresponding value for Primary.
			if (primaryBinding != DependencyProperty.UnsetValue
				&& primary == DependencyProperty.UnsetValue)
			{
				throw new ArgumentException($"{nameof(PrimaryBindingProperty)} was set wtihout a corresponding value for {nameof(PrimaryProperty)}.");
			}

			// Primary is missing a trigger by an event or binding.
			if (primary != DependencyProperty.UnsetValue
				&& primaryBinding == DependencyProperty.UnsetValue
				&& primarySettings != null
				&& primarySettings?.Event == EventType.None)
			{
				throw new ArgumentException($"{nameof(PrimaryProperty)} is missing a trigger by an event or binding.");
			}

			// SecondaryBinding was set wtihout a corresponding value for Secondary.
			if (secondaryBinding != DependencyProperty.UnsetValue
				&& secondary == DependencyProperty.UnsetValue)
			{
				throw new ArgumentException($"{nameof(SecondaryBindingProperty)} was set wtihout a corresponding value for {nameof(SecondaryProperty)}.");
			}

			// Secondary is missing a trigger by an event or binding.
			if (secondary != DependencyProperty.UnsetValue
				&& secondaryBinding == DependencyProperty.UnsetValue
				&& secondarySettings != null
				&& secondarySettings.Event == EventType.None)
			{
				throw new ArgumentException($"{nameof(SecondaryProperty)} is missing a trigger by an event or binding.");
			}

			// Cannot use StartWith without specifying a Primaryanimation.
			if (startWith != DependencyProperty.UnsetValue && primary == DependencyProperty.UnsetValue)
			{
				throw new ArgumentException($"Cannot use {nameof(StartWithProperty)} without specifying a {nameof(PrimaryProperty)} animation.");
			}
		}

#if __UWP__ || __UNO__
		internal static void ValidateListViewBase(ListViewBase element)
		{
			// Skip validation if a debugger isn't attached
			if (!Debugger.IsAttached)
			{
				return;
			}

			if (element is ListViewBase lvb)
			{
				Panel panel = null;

				if (lvb is ListView lv)
				{
					panel = lv.ItemsPanelRoot as ItemsStackPanel;
				}
				else if (lvb is GridView gv)
				{
					panel = gv.ItemsPanelRoot as ItemsWrapGrid;
				}

				// Animations will only work if the ItemsPanel is an ItemsStackPanel for the AnimatedListView or ItemsWrapGrid for the AnimatedGridView.
				if (panel == null)
				{
					throw new ArgumentException($"Animations will only work if the {nameof(ListViewBase.ItemsPanel)} is an {nameof(ItemsStackPanel)} for the {nameof(AnimatedListView)} or {nameof(ItemsWrapGrid)} for the {nameof(AnimatedGridView)}.");
				}

				var itemSettings = GetItems(lvb);

				// ItemsProperty can only be set on a AnimatedListView or AnimatedGridView.
				if (itemSettings != null && !(lvb is AnimatedListView) && !(lvb is AnimatedGridView))
				{
					throw new ArgumentException($"{nameof(ItemsProperty)} can only be set on a {nameof(AnimatedListView)} or {nameof(AnimatedGridView)}.");
				}

				// Don't set a value for the Event property, is it disregarded for ListViewBase item animations.
				if (itemSettings.Event != AnimationSettings.DEFAULT_EVENT)
				{
					throw new ArgumentException($"Don't set a value for the {nameof(itemSettings.Event)} property, is it disregarded for {nameof(ListViewBase)} item animations.");
				}
			}
		}
#endif

#if __WPF__
		internal static void ValidateListBox(ListBox element)
		{
			// Skip validation if a debugger isn't attached
			if (!Debugger.IsAttached)
			{
				return;
			}

			if (element is ListBox lb)
			{
				var itemSettings = GetItems(lb);

				// ItemsProperty can only be set on a AnimatedListBox or AnimatedListView.
				if (itemSettings != null && !(lb is AnimatedListBox) && !(lb is AnimatedListView))
				{
					throw new ArgumentException($"{nameof(ItemsProperty)} can only be set on a {nameof(AnimatedListBox)} or {nameof(AnimatedListView)}.");
				}

				// ListBox item animations will only work if the inner ScrollViewer has CanContentScroll = true
				if (lb?.FindDescendant<ScrollViewer>() is ScrollViewer scroller && !scroller.CanContentScroll)
				{
					throw new ArgumentException($"{nameof(ListBox)} item animations will only work if the inner {nameof(ScrollViewer)} has {nameof(scroller.CanContentScroll)} = true");
				}

				// Don't set a value for the Event property, is it disregarded for ListBox item animations.
				if (itemSettings != null && itemSettings.Event != AnimationSettings.DEFAULT_EVENT)
				{
					throw new ArgumentException($"Don't set a value for the {nameof(itemSettings.Event)} property, is it disregarded for {nameof(ListBox)} item animations.");
				}
			}
		}
#endif
	}
}