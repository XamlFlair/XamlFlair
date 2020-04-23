using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using XamlFlair.Extensions;
using XamlFlair.Controls;
using Microsoft.Extensions.Logging;

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
			var primaryIsSet = (element.ReadLocalValue(PrimaryProperty) != DependencyProperty.UnsetValue);
			var primaryBindingIsSet = (element.ReadLocalValue(PrimaryBindingProperty) != DependencyProperty.UnsetValue);
			var secondaryIsSet = (element.ReadLocalValue(SecondaryProperty) != DependencyProperty.UnsetValue);
			var secondaryBindingIsSet = (element.ReadLocalValue(SecondaryBindingProperty) != DependencyProperty.UnsetValue);
			var combinedBindingIsSet = (element.ReadLocalValue(CombinedBindingProperty) != DependencyProperty.UnsetValue);
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
			if (isIterating && secondaryIsSet)
			{
				throw new ArgumentException($"Cannot set an animation for {nameof(SecondaryProperty)} when specifying values for {nameof(IterationCountProperty)} or {nameof(IterationBehaviorProperty)}.");
			}

			// Primary must be set first before specifying a value for Secondary.
			if (!primaryIsSet && secondaryIsSet)
			{
				throw new ArgumentException($"{nameof(PrimaryProperty)} must be set first before specifying a value for {nameof(SecondaryProperty)}.");
			}

			// PrimaryBinding or CombinedBinding was set wtihout a corresponding value for Primary.
			if ((primaryBindingIsSet || combinedBindingIsSet) && !primaryIsSet)
			{
				throw new ArgumentException($"{nameof(PrimaryBindingProperty)} or {nameof(CombinedBindingProperty)} was set wtihout a corresponding value for {nameof(PrimaryProperty)}.");
			}

			// Primary is missing a trigger by an event or binding.
			if (primaryIsSet
				&& !primaryBindingIsSet
				&& !combinedBindingIsSet
				&& primarySettings != null
				&& primarySettings?.Event == EventType.None)
			{
				throw new ArgumentException($"{nameof(PrimaryProperty)} is missing a trigger by an event or binding.");
			}

			// SecondaryBinding was set wtihout a corresponding value for Secondary.
			if ((secondaryBindingIsSet || combinedBindingIsSet) && !secondaryIsSet)
			{
				throw new ArgumentException($"{nameof(SecondaryBindingProperty)} or {nameof(CombinedBindingProperty)} was set wtihout a corresponding value for {nameof(SecondaryProperty)}.");
			}

			// Secondary is missing a trigger by an event or binding.
			if (secondaryIsSet
				&& !secondaryBindingIsSet
				&& !combinedBindingIsSet
				&& secondarySettings != null
				&& secondarySettings.Event == EventType.None)
			{
				throw new ArgumentException($"{nameof(SecondaryProperty)} is missing a trigger by an event or binding.");
			}

			// Cannot use StartWith without specifying a Primary animation.
			if (startWith != DependencyProperty.UnsetValue && !primaryIsSet)
			{
				throw new ArgumentException($"Cannot use {nameof(StartWithProperty)} without specifying a {nameof(PrimaryProperty)} animation.");
			}
		}

#if !__WPF__
		internal static void ValidateListViewBase(ListViewBase element)
		{
			// Skip validation if a debugger isn't attached
			if (!Debugger.IsAttached)
			{
				return;
			}

			if (element is ListViewBase lvb)
			{
				if (lvb.ItemsSource == null)
				{
					Logger?.LogWarning($"Cannot animate {nameof(lvb.ItemsSource)} items because ItemsSource is null.");
					return;
				}

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

#if HAS_UNO
				// LIMITATION (Uno-Only): ListViewBase item animations MUST contain FadeFrom in the Kind with a value of 0 for Opacity.
				if (!itemSettings.Kind.HasFlag(AnimationKind.FadeFrom) || itemSettings.Opacity != 0)
				{
					throw new ArgumentException($"LIMITATION: {nameof(ListViewBase)} item animations MUST contain {nameof(AnimationKind.FadeFrom)} in the {nameof(AnimationSettings.KindProperty)} with a value of 0 for {nameof(AnimationSettings.Opacity)}.");
				}
#endif

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
				if (lb.ItemsSource == null)
				{
					Logger?.LogWarning($"Cannot animate ListBox items because {nameof(lb.ItemsSource)} is null.");
					return;
				}

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