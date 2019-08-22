using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using XamlFlair.UWP.Logging;

namespace XamlFlair
{
	public static class Layout
	{
		public static bool GetClipToBounds(DependencyObject obj) => (bool)obj.GetValue(ClipToBoundsProperty);

		public static void SetClipToBounds(DependencyObject obj, bool value) => obj.SetValue(ClipToBoundsProperty, value);

		/// <summary>
		/// Gets or sets a value indicating whether to clip the content of this element (or content coming from
		/// the child elements of this element) to fit into the size of the containing element.
		/// </summary>
		public static readonly DependencyProperty ClipToBoundsProperty =
			DependencyProperty.RegisterAttached(
				"ClipToBounds",
				typeof(bool),
				typeof(Layout),
				new PropertyMetadata(false, OnClipToBoundsChanged));

		private static void OnClipToBoundsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is FrameworkElement element)
			{
				element
					.Events()
					.SizeChanged
					.TakeUntil(element.Events().Unloaded)
					.Subscribe(
						args =>
						{
							var elem = args.Sender as FrameworkElement;

							elem.Clip = new RectangleGeometry()
							{
								Rect = new Rect(0, 0, elem.ActualWidth, elem.ActualHeight)
							};
						},
						ex => Animations.Logger.ErrorException($"Error on subscription to the {nameof(FrameworkElement.SizeChanged)} event.", ex)
					);
			}
		}
	}
}