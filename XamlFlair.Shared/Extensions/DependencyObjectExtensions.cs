using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

#if __WPF__
using System.Windows;
using System.Windows.Media;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
#endif

namespace XamlFlair.Extensions
{
	internal static class DependencyObjectExtensions
	{
		/// <summary>
		/// Find first descendant control of a specified type.
		/// </summary>
		/// <typeparam name="T">Type to search for.</typeparam>
		/// <param name="element">Parent element.</param>
		/// <returns>Descendant control or null if not found.</returns>
		internal static T FindDescendant<T>(this DependencyObject element)
			where T : UIElement
		{
			T retValue = null;
			var childrenCount = VisualTreeHelper.GetChildrenCount(element);

			for (var i = 0; i < childrenCount; i++)
			{
				var child = VisualTreeHelper.GetChild(element, i);
				var type = child as T;
				if (type != null)
				{
					retValue = type;
					break;
				}

				retValue = FindDescendant<T>(child);

				if (retValue != null)
				{
					break;
				}
			}

			return retValue;
		}

		/// <summary>
		/// Find first visual ascendant control of a specified type.
		/// </summary>
		/// <typeparam name="T">Type to search for.</typeparam>
		/// <param name="element">Child element.</param>
		/// <returns>Ascendant control or null if not found.</returns>
		internal static T FindAscendant<T>(this DependencyObject element)
			where T : UIElement
		{
			var parent = VisualTreeHelper.GetParent(element);

			if (parent == null)
			{
				return null;
			}

			if (parent is T)
			{
				return parent as T;
			}

			return parent.FindAscendant<T>();
		}

		/// <summary>
		/// Converts a DependencyProperty to an IObservable.
		/// </summary>
		/// <typeparam name="T">The Type of the component that owns the DependencyProperty.</typeparam>
		/// <param name="self">The element that owns the DependencyProperty.</param>
		/// <param name="dp">The DependencyProperty to "observe".</param>
		/// <returns>An Iobservable of the changes of the DependencyProperty.</returns>
		internal static IObservable<Unit> Observe<T>(this T self, DependencyProperty dp)
			where T : DependencyObject
		{
			return Observable.Create<Unit>(observer =>
			{
#if __WPF__
				EventHandler handler = (_, __) => observer.OnNext(Unit.Default);
				var descriptor = DependencyPropertyDescriptor.FromProperty(dp, typeof(T));
				descriptor.AddValueChanged(self, handler);

				return () => descriptor.RemoveValueChanged(self, handler);
#else
				DependencyPropertyChangedCallback handler = (_, __) => observer.OnNext(Unit.Default);
				var token = self.RegisterPropertyChangedCallback(dp, handler);

				return () => self.UnregisterPropertyChangedCallback(dp, token);
#endif
			});
		}
	}
}