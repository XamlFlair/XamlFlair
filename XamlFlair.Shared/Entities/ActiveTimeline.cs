using System;
using System.Collections.Generic;
using System.Text;

#if __WPF__
using System.Windows;
#else
using Windows.UI.Xaml;
#endif

namespace XamlFlair
{
	internal partial class ActiveTimeline<T> : IEquatable<ActiveTimeline<T>>
		where T : DependencyObject
	{
		internal Guid ElementGuid { get; set; }

		internal T Timeline { get; set; }

		internal AnimationSettings Settings { get; set; }

		internal FrameworkElement Element { get; set; }

		internal AnimationState State { get; set; }

		internal IterationBehavior IterationBehavior { get; set; }

		internal int IterationCount { get; set; }

		internal bool IsSequence { get; set; }

		internal int SequenceOrder { get; set; }

		internal bool IsIterating { get => IterationCount > 0 || IterationBehavior == IterationBehavior.Forever; }

		#region Equality

		public override bool Equals(object obj)
		{
			if (Object.ReferenceEquals(null, obj)) return false;    // Is null?
			if (Object.ReferenceEquals(this, obj)) return true;     // Is the same object?
			if (obj.GetType() != this.GetType()) return false;      // Is the same type?

			return IsEqual((ActiveTimeline<T>)obj);
		}

		public bool Equals(ActiveTimeline<T> other)
		{
			if (Object.ReferenceEquals(null, other)) return false;  // Is null?
			if (Object.ReferenceEquals(this, other)) return true;   // Is the same object?

			return IsEqual(other);
		}

		private bool IsEqual(ActiveTimeline<T> obj)
		{
			return obj is ActiveTimeline<T> other
				&& other.ElementGuid.Equals(ElementGuid)
				&& other.Timeline.Equals(Timeline)
				&& other.Settings.Equals(Settings)
				&& other.Settings.Equals(Element)
				&& other.Settings.Equals(State)
				&& other.Settings.Equals(IterationBehavior)
				&& other.Settings.Equals(IterationCount)
				&& other.Settings.Equals(IsSequence)
				&& other.Settings.Equals(SequenceOrder)
				&& other.Settings.Equals(IsIterating);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				// Choose large primes to avoid hashing collisions
				const int HashingBase = (int)2166136261;
				const int HashingMultiplier = 16777619;

				int hash = HashingBase;
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, ElementGuid) ? ElementGuid.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Timeline) ? Timeline.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Settings) ? Settings.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Element) ? Element.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, State) ? State.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, IterationBehavior) ? IterationBehavior.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, IterationCount) ? IterationCount.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, IsSequence) ? IsSequence.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, SequenceOrder) ? SequenceOrder.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, IsIterating) ? IsIterating.GetHashCode() : 0);
				return hash;
			}
		}

		public static bool operator ==(ActiveTimeline<T> obj, ActiveTimeline<T> other)
		{
			if (Object.ReferenceEquals(obj, other)) return true;
			if (Object.ReferenceEquals(null, obj)) return false;    // Ensure that "obj" isn't null

			return obj.Equals(other);
		}

		public static bool operator !=(ActiveTimeline<T> obj, ActiveTimeline<T> other) => !(obj == other);

#endregion
	}
}