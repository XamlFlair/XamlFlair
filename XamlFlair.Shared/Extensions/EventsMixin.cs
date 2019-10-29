using System;
using System.Reactive;
using System.Reactive.Linq;

#if __WPF__
using System.Windows.Input;
#else
using Windows.Foundation;
using Windows.UI.Xaml.Input;
#endif

#if __WPF__
namespace System.Windows
#else
namespace Windows.UI.Xaml
#endif
{
	internal static class EventsMixin
	{
		internal static FrameworkElementEvents Events(this FrameworkElement element) => new FrameworkElementEvents(element);

		internal class FrameworkElementEvents
		{
			private readonly FrameworkElement _element;

			internal FrameworkElementEvents(FrameworkElement element) => _element = element;

			internal IObservable<EventPattern<RoutedEventArgs>> Loaded =>
				Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
					h => _element.Loaded += h,
					h => _element.Loaded -= h);

			internal IObservable<EventPattern<RoutedEventArgs>> Unloaded =>
				Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
					h => _element.Unloaded += h,
					h => _element.Unloaded -= h);

			internal IObservable<EventPattern<RoutedEventArgs>> LoadedUntilUnloaded =>
				Loaded
					.DistinctUntilChanged()
					.TakeUntil(Unloaded);

			internal IObservable<EventPattern<RoutedEventArgs>> GotFocus =>
				Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
					h => _element.GotFocus += h,
					h => _element.GotFocus -= h);

			internal IObservable<EventPattern<RoutedEventArgs>> LostFocus =>
				Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
					h => _element.LostFocus += h,
					h => _element.LostFocus -= h);

			internal IObservable<EventPattern<SizeChangedEventArgs>> SizeChanged =>
				Observable.FromEventPattern<SizeChangedEventHandler, SizeChangedEventArgs>(
					h => _element.SizeChanged += h,
					h => _element.SizeChanged -= h);
#if __WPF__
			internal IObservable<EventPattern<MouseEventArgs>> PointerEntered =>
				Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(
					h => _element.MouseEnter += h,
					h => _element.MouseEnter -= h);

			internal IObservable<EventPattern<MouseEventArgs>> PointerExited =>
				Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(
					h => _element.MouseLeave += h,
					h => _element.MouseLeave -= h);
#else
			internal IObservable<EventPattern<PointerRoutedEventArgs>> PointerEntered =>
				Observable.FromEventPattern<PointerEventHandler, PointerRoutedEventArgs>(
					h => _element.PointerEntered += h,
					h => _element.PointerEntered -= h);

			internal IObservable<EventPattern<PointerRoutedEventArgs>> PointerExited =>
				Observable.FromEventPattern<PointerEventHandler, PointerRoutedEventArgs>(
					h => _element.PointerExited += h,
					h => _element.PointerExited -= h);
#endif

// Specific section for DataContextChanged for each platform
// =========================================================
#if __WPF__
			internal IObservable<EventPattern<DependencyPropertyChangedEventArgs>> DataContextChanged =>
				Observable.FromEventPattern<DependencyPropertyChangedEventHandler, DependencyPropertyChangedEventArgs>(
					h => _element.DataContextChanged += h,
					h => _element.DataContextChanged -= h);
#elif __UWP__ || __UNO_UWP__
			internal IObservable<EventPattern<DataContextChangedEventArgs>> DataContextChanged =>
				Observable.FromEventPattern<TypedEventHandler<FrameworkElement, DataContextChangedEventArgs>, DataContextChangedEventArgs>(
					h => _element.DataContextChanged += h,
					h => _element.DataContextChanged -= h);
#elif __UNO__
			internal IObservable<EventPattern<DataContextChangedEventArgs>> DataContextChanged =>
				Observable.FromEventPattern<TypedEventHandler<DependencyObject, DataContextChangedEventArgs>, DataContextChangedEventArgs>(
					h => _element.DataContextChanged += h,
					h => _element.DataContextChanged -= h);
#endif
// =========================================================
		}
	}
}

#if __UWP__
namespace Windows.UI.Xaml.Controls
{
	internal static class EventsMixin
	{
		internal static ListViewBaseEvents Events(this ListViewBase element) => new ListViewBaseEvents(element);

		internal class ListViewBaseEvents
		{
			private readonly ListViewBase _element;

			internal ListViewBaseEvents(ListViewBase element) => _element = element;

			internal IObservable<EventPattern<ContainerContentChangingEventArgs>> ContainerContentChanging =>
				Observable.FromEventPattern<TypedEventHandler<ListViewBase, ContainerContentChangingEventArgs>, ContainerContentChangingEventArgs>(
					h => _element.ContainerContentChanging += h,
					h => _element.ContainerContentChanging -= h);
		}
	}
}
#endif

#if __WPF__
namespace System.Windows.Controls
{
	internal static class EventsMixin
	{
		internal static ItemContainerGeneratorEvents Events(this ItemContainerGenerator element) => new ItemContainerGeneratorEvents(element);

		internal class ItemContainerGeneratorEvents
		{
			private readonly ItemContainerGenerator _element;

			internal ItemContainerGeneratorEvents(ItemContainerGenerator element) => _element = element;

			internal IObservable<EventPattern<EventArgs>> ContainersGenerated =>
				Observable.FromEventPattern<EventHandler, EventArgs>(
					h => _element.StatusChanged += h,
					h => _element.StatusChanged -= h);
		}

		internal static ListBoxItemEvents Events(this ListBoxItem element) => new ListBoxItemEvents(element);

		internal class ListBoxItemEvents
		{
			private readonly ListBoxItem _element;

			internal ListBoxItemEvents(ListBoxItem element) => _element = element;

			internal IObservable<EventPattern<RoutedEventArgs>> Loaded =>
				Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
					h => _element.Loaded += h,
					h => _element.Loaded -= h);
		}
	}
}
#endif