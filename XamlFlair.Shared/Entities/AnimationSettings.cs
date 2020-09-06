using System;

#if __WPF__
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
#else
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
#endif

namespace XamlFlair
{
	// When adding a new property:
	//	- Update equality code at the bottom of this class
	//	- Update AnimationSettingsExtensions.cs
	//	- Update Animate.cs

#if __WPF__
	[SuppressMessage("", "CS0660", Justification = "Unable to override Object.Equals since DependencyObject is sealed.")]
	[SuppressMessage("", "CS0661", Justification = "Unable to override Object.GetHashCode since DependencyObject is sealed.")]

	public class AnimationSettings : DependencyObject, IAnimationSettings, IEqualityComparer<AnimationSettings>
#else
	public partial class AnimationSettings : DependencyObject, IAnimationSettings, IEquatable<AnimationSettings>
#endif
	{
		public AnimationKind Kind
		{
			get => (AnimationKind)GetValue(KindProperty);
			set => SetValue(KindProperty, value);
		}

		/// <summary>
		/// Specifies the animation types to include in the composite animation
		/// </summary>
		public static readonly DependencyProperty KindProperty =
			DependencyProperty.Register(
				nameof(Kind),
				typeof(AnimationKind),
				typeof(AnimationSettings),
				new PropertyMetadata(DefaultSettings.Kind));

		public double Duration
		{
			get => (double)GetValue(DurationProperty);
			set => SetValue(DurationProperty, value);
		}

		/// <summary>
		/// Specifies the duration of the composite animation
		/// </summary>
		public static readonly DependencyProperty DurationProperty =
			DependencyProperty.Register(
				nameof(Duration),
				typeof(double),
				typeof(AnimationSettings),
				new PropertyMetadata(DefaultSettings.Duration));

		public double Delay
		{
			get => (double)GetValue(DelayProperty);
			set => SetValue(DelayProperty, value);
		}

		/// <summary>
		/// Specifies the delay of the composite animation
		/// </summary>
		public static readonly DependencyProperty DelayProperty =
			DependencyProperty.Register(
				nameof(Delay),
				typeof(double),
				typeof(AnimationSettings),
				new PropertyMetadata(0d));

		public double Opacity
		{
			get => (double)GetValue(OpacityProperty);
			set => SetValue(OpacityProperty, value);
		}

		/// <summary>
		/// Specifies the target opacity of the composite animation
		/// </summary>
		public static readonly DependencyProperty OpacityProperty =
			DependencyProperty.Register(
				nameof(Opacity),
				typeof(double),
				typeof(AnimationSettings),
				new PropertyMetadata(1d));

		public Offset OffsetX
		{
			get => (Offset)GetValue(OffsetXProperty);
			set => SetValue(OffsetXProperty, value);
		}

		/// <summary>
		/// Specifies the target x-offset of the composite animation
		/// </summary>
		/// <remarks>
		/// OffsetX must be a double or a star-based value (ex: 150 or 0.75*)
		/// </remarks>
		public static readonly DependencyProperty OffsetXProperty =
			DependencyProperty.Register(
				nameof(OffsetX),
				typeof(Offset),
				typeof(AnimationSettings),
				new PropertyMetadata(Offset.Empty));

		public Offset OffsetY
		{
			get => (Offset)GetValue(OffsetYProperty);
			set => SetValue(OffsetYProperty, value);
		}

		/// <summary>
		/// Specifies the target y-offset of the composite animation
		/// </summary>
		/// <remarks>
		/// OffsetY must be a double or a star-based value (ex: 150 or 0.75*)
		/// </remarks>
		public static readonly DependencyProperty OffsetYProperty =
			DependencyProperty.Register(
				nameof(OffsetY),
				typeof(Offset),
				typeof(AnimationSettings),
				new PropertyMetadata(Offset.Empty));

#if __UWP__
		public double OffsetZ
		{
			get => (double)GetValue(OffsetZProperty);
			set => SetValue(OffsetZProperty, value);
		}

		/// <summary>
		/// Specifies the target z-offset of the composite animation
		/// </summary>
		public static readonly DependencyProperty OffsetZProperty =
			DependencyProperty.Register(
				nameof(OffsetZ),
				typeof(double),
				typeof(AnimationSettings),
				new PropertyMetadata(0d));
#endif

		public double ScaleX
		{
			get => (double)GetValue(ScaleXProperty);
			set => SetValue(ScaleXProperty, value);
		}

		/// <summary>
		/// Specifies the target x-scaling of the composite animation
		/// </summary>
		public static readonly DependencyProperty ScaleXProperty =
			DependencyProperty.Register(
				nameof(ScaleX),
				typeof(double),
				typeof(AnimationSettings),
				new PropertyMetadata(1d));

		public double ScaleY
		{
			get => (double)GetValue(ScaleYProperty);
			set => SetValue(ScaleYProperty, value);
		}

		/// <summary>
		/// Specifies the target y-scaling of the composite animation
		/// </summary>
		public static readonly DependencyProperty ScaleYProperty =
			DependencyProperty.Register(
				nameof(ScaleY),
				typeof(double),
				typeof(AnimationSettings),
				new PropertyMetadata(1d));

#if __UWP__
		public double ScaleZ
		{
			get => (double)GetValue(ScaleZProperty);
			set => SetValue(ScaleZProperty, value);
		}

		/// <summary>
		/// Specifies the target z-scaling of the composite animation
		/// </summary>
		public static readonly DependencyProperty ScaleZProperty =
			DependencyProperty.Register(
				nameof(ScaleZ),
				typeof(double),
				typeof(AnimationSettings),
				new PropertyMetadata(1d));
#endif

		public double Rotation
		{
			get => (double)GetValue(RotationProperty);
			set => SetValue(RotationProperty, value);
		}

		/// <summary>
		/// Specifies the target rotation (in degrees) of the composite animation
		/// </summary>
		public static readonly DependencyProperty RotationProperty =
			DependencyProperty.Register(
				nameof(Rotation),
				typeof(double),
				typeof(AnimationSettings),
				new PropertyMetadata(0d));

// ColorAnimation supported only on Uno and WPF (not on native UWP due to Composition-only implementations)
#if WINDOWS_UWP || HAS_UNO || __WPF__
		public Color Color
		{
			get => (Color)GetValue(ColorProperty);
			set => SetValue(ColorProperty, value);
		}

		/// <summary>
		/// Specifies the target color of the composite animation
		/// </summary>
		public static readonly DependencyProperty ColorProperty =
			DependencyProperty.Register(
				nameof(Color),
				typeof(Color),
				typeof(AnimationSettings),
				new PropertyMetadata(Colors.Transparent));

		public ColorTarget ColorOn
		{
			get => (ColorTarget)GetValue(ColorOnProperty);
			set => SetValue(ColorOnProperty, value);
		}

		/// <summary>
		/// Specifies the target property for a color animation
		/// </summary>
		public static readonly DependencyProperty ColorOnProperty =
			DependencyProperty.Register(
				nameof(ColorOn),
				typeof(ColorTarget),
				typeof(AnimationSettings),
				new PropertyMetadata(ColorTarget.Background));
#endif

		// Blur not supported on Uno
#if !HAS_UNO
		public double BlurRadius
		{
			get => (double)GetValue(BlurRadiusProperty);
			set => SetValue(BlurRadiusProperty, value);
		}

		/// <summary>
		/// Specifies the blur amount of the composite animation
		/// </summary>
		public static readonly DependencyProperty BlurRadiusProperty =
			DependencyProperty.Register(
				nameof(BlurRadius),
				typeof(double),
				typeof(AnimationSettings),
				new PropertyMetadata(0d));
#endif

#if __UWP__
		public double Saturation
		{
			get => (double)GetValue(SaturationProperty);
			set => SetValue(SaturationProperty, value);
		}

		/// <summary>
		/// Specifies the saturation amount of the composite animation
		/// </summary>
		public static readonly DependencyProperty SaturationProperty =
			DependencyProperty.Register(
				nameof(Saturation),
				typeof(double),
				typeof(AnimationSettings),
				new PropertyMetadata(DefaultSettings.Saturation));

		public Color Tint
		{
			get => (Color)GetValue(TintProperty);
			set => SetValue(TintProperty, value);
		}

		/// <summary>
		/// Specifies the tint color of the composite animation
		/// </summary>
		public static readonly DependencyProperty TintProperty =
			DependencyProperty.Register(
				nameof(Tint),
				typeof(Color),
				typeof(AnimationSettings),
				new PropertyMetadata(DefaultSettings.Tint));
#endif

		public Point TransformCenterPoint
		{
			get => (Point)GetValue(TransformCenterPointProperty);
			set => SetValue(TransformCenterPointProperty, value);
		}

		/// <summary>
		/// Specifies the center point of the element's transform
		/// </summary>
		public static readonly DependencyProperty TransformCenterPointProperty =
			DependencyProperty.Register(
				nameof(TransformCenterPoint),
				typeof(Point),
				typeof(AnimationSettings),
				new PropertyMetadata(DefaultSettings.TransformCenterPoint));

#if __WPF__
		public TransformationType TransformOn
		{
			get => (TransformationType)GetValue(TransformOnProperty);
			set => SetValue(TransformOnProperty, value);
		}

		/// <summary>
		/// Specifies the transformation type to use (render or layout)
		/// </summary>
		public static readonly DependencyProperty TransformOnProperty =
			DependencyProperty.Register(
				nameof(TransformOn),
				typeof(TransformationType),
				typeof(AnimationSettings),
				new PropertyMetadata(DefaultSettings.TransformOn));
#endif

		public EasingType Easing
		{
			get => (EasingType)GetValue(EasingProperty);
			set => SetValue(EasingProperty, value);
		}

		/// <summary>
		/// Specifies the easing of the composite animation
		/// </summary>
		public static readonly DependencyProperty EasingProperty =
			DependencyProperty.Register(
				nameof(Easing),
				typeof(EasingType),
				typeof(AnimationSettings),
				new PropertyMetadata(DefaultSettings.Easing));

		public EasingMode EasingMode
		{
			get => (EasingMode)GetValue(EasingModeProperty);
			set => SetValue(EasingModeProperty, value);
		}

		/// <summary>
		/// Specifies the easing's mode of the composite animation
		/// </summary>
		/// <remarks>
		/// This property is disregarded for controls based on ListViewBase (UWP) or ListBox (WPF)
		/// </remarks>
		public static readonly DependencyProperty EasingModeProperty =
			DependencyProperty.Register(
				nameof(EasingMode),
				typeof(EasingMode),
				typeof(AnimationSettings),
				new PropertyMetadata(DefaultSettings.Mode));

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
				typeof(AnimationSettings),
				new PropertyMetadata(DefaultSettings.Event));

		// TODO: Determine if really needed to have AnimationSettings
		// as a DependencyObject. Here are the properties listed
		// as regular CLR poroperties
		// -----------------------------------------
		/*
		public AnimationKind Kind { get; set; } = DefaultSettings.Kind;

		public double Duration { get; set; } = DefaultSettings.Duration;

		public double Delay { get; set; } = 0d;

		public double Opacity { get; set; } = 1d;

		public Offset OffsetX { get; set; } = new Offset() { OffsetValue = 0 };

		public Offset OffsetY { get; set; } = new Offset() { OffsetValue = 0 };

#if __UWP__
		public double OffsetZ { get; set; } = 0d;
#endif

		public double ScaleX { get; set; } = 1d;

		public double ScaleY { get; set; } = 1d;

#if __UWP__
		public double ScaleZ { get; set; } = 1d;
#endif

		public double Rotation { get; set; } = 0d;

// ColorAnimation supported only on Uno and WPF (not on native UWP due to Composition-only implementations)
#if WINDOWS_UWP || HAS_UNO || __WPF__
		public Color Color { get; set; } = Colors.Transparent;

		public ColorTarget ColorOn { get; set; } = ColorTarget.Background;
#endif

		// Blur not supported on Uno
#if !HAS_UNO
		public double BlurRadius { get; set; } = 0d;
#endif

#if __UWP__
		public double Saturation { get; set; } = DefaultSettings.Saturation;

		public Color Tint { get; set; } = DefaultSettings.Tint;
#endif

		public Point TransformCenterPoint { get; set; } = DefaultSettings.TransformCenterPoint;

#if __WPF__
		public TransformationType TransformOn { get; set; } = DefaultSettings.TransformOn;
#endif

		public EasingType Easing { get; set; } = DefaultSettings.Easing;

		public EasingMode EasingMode { get; set; } = DefaultSettings.Mode;

		public EventType Event { get; set; } = DefaultSettings.Event;
		*/
		// -----------------------------------------

		#region Equality

		public bool Equals(AnimationSettings other)
		{
			if (Object.ReferenceEquals(null, other)) return false;  // Is null?
			if (Object.ReferenceEquals(this, other)) return true;   // Is the same object?

			return IsEqual(other);
		}

		private bool IsEqual(AnimationSettings obj)
		{
			return obj is AnimationSettings other
				&& other.Kind.Equals(Kind)
				&& other.Duration.Equals(Duration)
				&& other.Delay.Equals(Delay)
				&& other.Opacity.Equals(Opacity)
				&& other.OffsetX.Equals(OffsetX)
				&& other.OffsetY.Equals(OffsetY)
				&& other.ScaleX.Equals(ScaleX)
				&& other.ScaleY.Equals(ScaleY)
				&& other.Rotation.Equals(Rotation)
				&& other.TransformCenterPoint.Equals(TransformCenterPoint)
// ColorAnimation supported only on Uno and WPF (not on native UWP due to Composition-only implementations)
#if WINDOWS_UWP || HAS_UNO || __WPF__
				&& other.Color.Equals(Color)
				&& other.ColorOn.Equals(ColorOn)
#endif
#if !HAS_UNO
				&& other.BlurRadius.Equals(BlurRadius)
#endif
				&& other.Easing.Equals(Easing)
				&& other.EasingMode.Equals(EasingMode)
#if __WPF__
				&& other.TransformOn.Equals(TransformOn)
#endif
#if __UWP__
				&& other.OffsetZ.Equals(OffsetZ)
				&& other.ScaleZ.Equals(ScaleZ)
				&& other.Saturation.Equals(Saturation)
				&& other.Tint.Equals(Tint)
#endif
				&& other.Event.Equals(Event);
		}

#if __WPF__
		public bool Equals(AnimationSettings x, AnimationSettings y)
		{
			if (Object.ReferenceEquals(null, y)) return false;  // Is null?
			if (Object.ReferenceEquals(x, y)) return true;      // Is the same object?
			if (x.GetType() != y.GetType()) return false;       // Is the same type?

			return IsEqual((AnimationSettings)y);
		}

		public int GetHashCode(AnimationSettings obj)
			=> InternalGetHashCode();
#else
		public override bool Equals(object obj)
		{
			if (Object.ReferenceEquals(null, obj)) return false;    // Is null?
			if (Object.ReferenceEquals(this, obj)) return true;     // Is the same object?
			if (obj.GetType() != this.GetType()) return false;      // Is the same type?

			return IsEqual((AnimationSettings)obj);
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
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Kind) ? Kind.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Duration) ? Duration.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Delay) ? Delay.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Opacity) ? Opacity.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, OffsetX) ? OffsetX.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, OffsetY) ? OffsetY.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, ScaleX) ? ScaleX.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, ScaleY) ? ScaleY.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Rotation) ? Rotation.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, TransformCenterPoint) ? TransformCenterPoint.GetHashCode() : 0);
// ColorAnimation supported only on Uno and WPF (not on native UWP due to Composition-only implementations)
#if WINDOWS_UWP || HAS_UNO || __WPF__
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Color) ? Color.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, ColorOn) ? ColorOn.GetHashCode() : 0);
#endif
#if !HAS_UNO
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, BlurRadius) ? BlurRadius.GetHashCode() : 0);
#endif
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Easing) ? Easing.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, EasingMode) ? EasingMode.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Event) ? Event.GetHashCode() : 0);
#if __WPF__
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, TransformOn) ? TransformOn.GetHashCode() : 0);
#endif
#if __UWP__
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, OffsetZ) ? OffsetZ.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, ScaleZ) ? ScaleZ.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Saturation) ? Saturation.GetHashCode() : 0);
				hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Tint) ? Tint.GetHashCode() : 0);
#endif
				return hash;
			}
		}

		public static bool operator ==(AnimationSettings obj, AnimationSettings other)
		{
			if (Object.ReferenceEquals(obj, other)) return true;
			if (Object.ReferenceEquals(null, obj)) return false;    // Ensure that "obj" isn't null

			return obj.Equals(other);
		}

		public static bool operator !=(AnimationSettings obj, AnimationSettings other) => !(obj == other);

		#endregion
	}
}