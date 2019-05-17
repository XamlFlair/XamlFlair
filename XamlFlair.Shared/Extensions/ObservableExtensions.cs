using System;
using System.Reactive;
using System.Reactive.Linq;

namespace XamlFlair.Extensions
{
	internal static class ObservableExtensions
	{
		internal static IObservable<bool> WhereTrue(this IObservable<bool> observable)
			=> observable.Where(x => x);

		internal static IObservable<bool> WhereFalse(this IObservable<bool> observable)
			=> observable.Where(x => !x);

		internal static IObservable<T> WhereNotNull<T>(this IObservable<T> observable)
			where T : class
			=> observable.Where(x => x != null);

		internal static IObservable<Unit> SelectUnit<T>(this IObservable<T> observable)
			=> observable.Select(_ => Unit.Default);
	}
}