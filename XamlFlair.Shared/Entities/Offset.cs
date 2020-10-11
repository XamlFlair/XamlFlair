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
	public partial class Offset
#endif
	{
		internal static Offset Empty = new Offset();

		public double OffsetFactor { get; set; }

		public double OffsetValue { get; set; }

		internal OffsetTarget Target { get; set; }

		public static Offset ConvertToOffsetFactor(string translation)
		{
			return Offset.Create(translation.Trim());
		}

		internal double GetCalculatedOffset(FrameworkElement element, OffsetTarget target)
		{
			// Make sure that an offset value is used
			// if an offset factor wasn't specified
			if (OffsetFactor == 0 && (OffsetValue > 0 || OffsetValue < 0))
			{
				return OffsetValue;
			}

			var width = element.ActualWidth > 0
				? element.ActualWidth
				: element.Width > 0
					? element.Width
					: 0;

			var height = element.ActualHeight > 0
				? element.ActualHeight
				: element.Height > 0
					? element.Height
					: 0;

			return target == OffsetTarget.X
				? width * OffsetFactor
				: height * OffsetFactor;
		}

		internal static Offset Create(string offsetValue)
		{
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
					OffsetValue = dbl
				};
			}

			throw new ArgumentException($"{nameof(Offset)} must be a double or a star-based value (ex: 150 or 0.75*).");
		}

		public override string ToString()
		{
			if (OffsetValue > 0 || OffsetValue < 0)
			{
				return OffsetValue.ToString();
			}

			return OffsetFactor.ToString();
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
				&& other.OffsetValue.Equals(OffsetValue)
				&& other.OffsetFactor.Equals(OffsetFactor)
				&& other.Target.Equals(Target);
		}

#if __WPF__
		public bool Equals(Offset x, Offset y)
		{
			if (Object.ReferenceEquals(null, y)) return false;  // Is null?
			if (Object.ReferenceEquals(x, y)) return true;      // Is the same object?
			if (x.GetType() != y.GetType()) return false;       // Is the same type?

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
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, OffsetValue) ? OffsetValue.GetHashCode() : 0);
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

			return Offset.Create(offsetValue);
		}
	}
}