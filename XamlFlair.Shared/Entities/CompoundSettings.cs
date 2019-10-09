using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

#if __WPF__
using System.Windows;
#else
using Windows.UI.Xaml;
#endif

namespace XamlFlair
{
#if __WPF__
	[SuppressMessage("", "CS0660", Justification = "Unable to override Object.Equals since DependencyObject is sealed.")]
	[SuppressMessage("", "CS0661", Justification = "Unable to override Object.GetHashCode since DependencyObject is sealed.")]

	public class CompoundSettings : DependencyObject, IAnimationSettings, IEqualityComparer<CompoundSettings>
#else
	public partial class CompoundSettings : DependencyObject, IAnimationSettings, IEquatable<CompoundSettings>
#endif
	{
		public EventType Event
		{
			get => (EventType)GetValue(EventProperty);
			set => SetValue(EventProperty, value);
		}

		/// <summary>
		/// Specifies the event used to trigger the composite animation
		/// </summary>
		public static readonly DependencyProperty EventProperty =
			DependencyProperty.Register(
				nameof(Event),
				typeof(EventType),
				typeof(CompoundSettings),
				new PropertyMetadata(AnimationSettings.DEFAULT_EVENT));

		public List<AnimationSettings> Sequence
		{
			get => (List<AnimationSettings>)GetValue(SequenceProperty);
			set => SetValue(SequenceProperty, value);
		}

		/// <summary>
		/// Specifies the list of AnimationSettings used for a compound animation
		/// </summary>
		public static readonly DependencyProperty SequenceProperty =
			DependencyProperty.Register(
				nameof(Sequence),
				typeof(List<AnimationSettings>),
				typeof(CompoundSettings),
				new PropertyMetadata(new List<AnimationSettings>()));

		#region Equality

		public bool Equals(CompoundSettings other)
		{
			if (Object.ReferenceEquals(null, other)) return false;  // Is null?
			if (Object.ReferenceEquals(this, other)) return true;   // Is the same object?

			return IsEqual(other);
		}

		private bool IsEqual(CompoundSettings obj)
		{
			return obj is CompoundSettings other
				&& other.Event.Equals(Event)
				&& other.Sequence.SequenceEqual(Sequence);
		}

#if __WPF__
		public bool Equals(CompoundSettings x, CompoundSettings y)
		{
			if (Object.ReferenceEquals(null, y)) return false;	// Is null?
			if (Object.ReferenceEquals(x, y)) return true;		// Is the same object?
			if (x.GetType() != y.GetType()) return false;		// Is the same type?

			return IsEqual((CompoundSettings)y);
		}

		public int GetHashCode(CompoundSettings obj)
			=> InternalGetHashCode();
#else
		public override bool Equals(object obj)
		{
			if (Object.ReferenceEquals(null, obj)) return false;    // Is null?
			if (Object.ReferenceEquals(this, obj)) return true;     // Is the same object?
			if (obj.GetType() != this.GetType()) return false;      // Is the same type?

			return IsEqual((CompoundSettings)obj);
		}

		public override int GetHashCode()
			=> InternalGetHashCode();
#endif

		private int InternalGetHashCode()
		{
			unchecked
			{
				// Choose large primes to avoid hashing collisions
				const int HashingBase = (int)2166136261;
				const int HashingMultiplier = 16777619;

				int hash = HashingBase;
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Event) ? Event.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Sequence) ? Sequence.GetHashCode() : 0);
				return hash;
			}
		}

		public static bool operator ==(CompoundSettings obj, CompoundSettings other)
		{
			if (Object.ReferenceEquals(obj, other)) return true;
			if (Object.ReferenceEquals(null, obj)) return false;    // Ensure that "obj" isn't null

			return obj.Equals(other);
		}

		public static bool operator !=(CompoundSettings obj, CompoundSettings other) => !(obj == other);

#endregion
	}
}