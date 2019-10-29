using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using XamlFlair.Extensions;

namespace XamlFlair
{
	public static partial class Animations
	{
		#region Attached Properties

		public static bool GetItemsBinding(ListViewBase obj) => (bool)obj.GetValue(ItemsBindingProperty);

		public static void SetItemsBinding(ListViewBase obj, bool value) => obj.SetValue(ItemsBindingProperty, value);

		public static readonly DependencyProperty ItemsBindingProperty =
			DependencyProperty.RegisterAttached(
				"ItemsBinding",
				typeof(bool),
				typeof(Animations),
				new PropertyMetadata(false, OnItemsBindingChanged));

		public static AnimationSettings GetItems(ListViewBase obj) => (AnimationSettings)obj.GetValue(ItemsProperty);

		public static void SetItems(ListViewBase obj, AnimationSettings value) => obj.SetValue(ItemsProperty, value);

		/// <summary>
		/// The List animation intended to run on List's Loaded event or a value change on ItemsBinding.
		/// </summary>
		public static readonly DependencyProperty ItemsProperty =
			DependencyProperty.RegisterAttached(
				"Items",
				typeof(AnimationSettings),
				typeof(Animations),
				new PropertyMetadata(null, OnItemsChanged));

		public static double GetInterElementDelay(ListViewBase obj) => (double)obj.GetValue(InterElementDelayProperty);

		public static void SetInterElementDelay(ListViewBase obj, double value) => obj.SetValue(InterElementDelayProperty, value);

		/// <summary>
		/// Specifies the animation delay to apply to every lvb item.
		/// </summary>
		/// <remarks>NOTE: Delay is being overwritten by InterElementDelay. Therefore, a lvb animation currently can't have an initial Delay.</remarks>
		public static readonly DependencyProperty InterElementDelayProperty =
			DependencyProperty.RegisterAttached(
				"InterElementDelay",
				typeof(double),
				typeof(Animations),
				new PropertyMetadata(AnimationSettings.DEFAULT_INTER_ELEMENT_DELAY));

		public static bool GetAnimateOnLoad(ListViewBase obj) => (bool)obj.GetValue(AnimateOnLoadProperty);

		public static void SetAnimateOnLoad(ListViewBase obj, bool value) => obj.SetValue(AnimateOnLoadProperty, value);

		/// <summary>
		/// Specifies if the item animations should animate when the list control has loaded
		/// </summary>
		public static readonly DependencyProperty AnimateOnLoadProperty =
			DependencyProperty.RegisterAttached(
				"AnimateOnLoad",
				typeof(bool),
				typeof(Animations),
				new PropertyMetadata(true));

		public static bool GetAnimateOnItemsSourceChange(ListViewBase obj) => (bool)obj.GetValue(AnimateOnItemsSourceChangeProperty);

		public static void SetAnimateOnItemsSourceChange(ListViewBase obj, bool value) => obj.SetValue(AnimateOnItemsSourceChangeProperty, value);

		/// <summary>
		/// Specifies if the item animations should re-animate when there is a change on ItemsSource
		/// </summary>
		public static readonly DependencyProperty AnimateOnItemsSourceChangeProperty =
			DependencyProperty.RegisterAttached(
				"AnimateOnItemsSourceChange",
				typeof(bool),
				typeof(Animations),
				new PropertyMetadata(true));

		// TODO: NOT WORKING... ListViewBase.ContainerFromIndex(...)
		// cannot fetch an item not within view
		// ------------------------------------
		//public static short GetVisibleThreshold(ListViewBase obj) => (short)obj.GetValue(VisibleThresholdProperty);

		//public static void SetVisibleThreshold(ListViewBase obj, short value) => obj.SetValue(VisibleThresholdProperty, value);

		///// <summary>
		///// Specifies the number of items out-of-view to include when animating
		///// </summary>
		//public static readonly DependencyProperty VisibleThresholdProperty =
		//	DependencyProperty.RegisterAttached(
		//		"VisibleThreshold",
		//		typeof(short),
		//		typeof(Animations),
		//		new PropertyMetadata((short)10));
		// ------------------------------------

		#endregion

		#region Attached Property Callbacks

		private static void OnItemsBindingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			// Prevent running animations in a Visual Designer
			if (IsInDesignMode(d))
			{
				return;
			}

			if (d is ListViewBase lvb
				&& lvb.Items.Count > 0
				&& e.NewValue is bool isAnimating
				&& isAnimating)
			{
				int firstVisibleIndex = -1, lastVisibleIndex = -1;

				if (lvb is ListView)
				{
					firstVisibleIndex = (lvb.ItemsPanelRoot as ItemsStackPanel)?.FirstVisibleIndex ?? -1;
					lastVisibleIndex = (lvb.ItemsPanelRoot as ItemsStackPanel)?.LastVisibleIndex ?? -1;
				}
				else if (lvb is GridView)
				{
					firstVisibleIndex = (lvb.ItemsPanelRoot as ItemsWrapGrid)?.FirstVisibleIndex ?? -1;
					lastVisibleIndex = (lvb.ItemsPanelRoot as ItemsWrapGrid)?.LastVisibleIndex ?? -1;
				}

				var settings = GetItems(lvb);

				lvb.AnimateVisibleItems<SelectorItem>(settings, firstVisibleIndex, lastVisibleIndex);
			}
		}

		private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is ListViewBase lvb)
			{
				if (GetIsInitialized(lvb))
				{
					return;
				}

				// Set IsInitialized to true to only run this code once per element
				SetIsInitialized(lvb, true);

				// Pass an action to reset the "container loaded" flag when the ItemsSource changes
				lvb.Initialize();
			}
		}

		#endregion
	}
}