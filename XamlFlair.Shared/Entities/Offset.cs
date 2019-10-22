using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

#if __WPF__
using System.Windows;
#else
using Windows.UI.Xaml;
#endif

namespace XamlFlair
{
#if __WPF__
	[TypeConverter(typeof(OffsetTypeConverter))]
	public class Offset
#else
	[Windows.Foundation.Metadata.CreateFromString(MethodName = "XamlFlair.Offset.ConvertToOffsetFactor")]
	public struct Offset
#endif
	{
		internal double OffsetFactor { get; set; }

		internal OffsetTarget Target { get; set; }

		public static Offset ConvertToOffsetFactor(string translation)
		{
			var offsetValue = translation.Trim();

			if (offsetValue.Equals("*", StringComparison.InvariantCulture))
			{
				return new Offset()
				{
					OffsetFactor = 1.0
				};
			}
			else if (offsetValue.EndsWith("*") && double.TryParse(offsetValue.TrimEnd('*'), out var result))
			{
				return new Offset()
				{
					OffsetFactor = result
				};
			}
			else if (double.TryParse(offsetValue, out var dbl))
			{
				return new Offset()
				{
					OffsetFactor = dbl
				};
			}

			throw new ArgumentException($"{nameof(Offset)} must be a double or a star-based value (ex: 150 or 0.75*).");
		}

		internal double GetCalculatedOffset(FrameworkElement element, OffsetTarget target)
		{
			var width = element.ActualWidth > 0 ? element.ActualWidth : element.Width;
			var height = element.ActualHeight > 0 ? element.ActualHeight : element.Height;

			return target == OffsetTarget.X
				? width * OffsetFactor
				: height * OffsetFactor;
		}

		#region Equality

		public bool Equals(Offset other)
		{
			if (Object.ReferenceEquals(null, other)) return false;  // Is null?
			if (Object.ReferenceEquals(this, other)) return true;   // Is the same object?

			return IsEqual(other);
		}

		private bool IsEqual(Offset obj)
		{
			return obj is Offset other
				&& other.OffsetFactor.Equals(OffsetFactor)
				&& other.Target.Equals(Target);
		}

#if __WPF__
		public bool Equals(Offset x, Offset y)
		{
			if (Object.ReferenceEquals(null, y)) return false;	// Is null?
			if (Object.ReferenceEquals(x, y)) return true;		// Is the same object?
			if (x.GetType() != y.GetType()) return false;		// Is the same type?

			return IsEqual((Offset)y);
		}

		public int GetHashCode(Offset obj)
			=> InternalGetHashCode();
#else
		public override bool Equals(object obj)
		{
			if (Object.ReferenceEquals(null, obj)) return false;    // Is null?
			if (Object.ReferenceEquals(this, obj)) return true;     // Is the same object?
			if (obj.GetType() != this.GetType()) return false;      // Is the same type?

			return IsEqual((Offset)obj);
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
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, OffsetFactor) ? OffsetFactor.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Target) ? Target.GetHashCode() : 0);
				return hash;
			}
		}

		public static bool operator ==(Offset obj, Offset other)
		{
			if (Object.ReferenceEquals(obj, other)) return true;
			if (Object.ReferenceEquals(null, obj)) return false;    // Ensure that "obj" isn't null

			return obj.Equals(other);
		}

		public static bool operator !=(Offset obj, Offset other) => !(obj == other);

		#endregion
	}

	public class OffsetTypeConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			var offsetValue = ((value as string) ?? string.Empty).Trim();

			if (offsetValue.EndsWith("*") && double.TryParse(offsetValue.TrimEnd('*'), out var result))
			{
				return new Offset()
				{
					OffsetFactor = result
				};
			}
			else if (double.TryParse(offsetValue, out var dbl))
			{
				return new Offset()
				{
					OffsetFactor = dbl
				};
			}

			throw new ArgumentException($"{nameof(Offset)} must be a double or a star-based value (ex: 150 or 0.75*).");
		}
	}
}
