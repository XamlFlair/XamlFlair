using System;
using System.Reactive.Disposables;
using System.Windows.Input;
using Microsoft.Extensions.Logging;

#if __WPF__
using System.Windows;
#else
using Windows.UI.Xaml;
using Windows.UI.Composition;
#endif

namespace XamlFlair
{
	public static partial class Animations
	{
		/// <summary>
		/// Activates logging for the list of active storyboards (outputs the list on every item add/remove)
		/// </summary>
		public static LogLevel EnableActiveTimelinesLogging { get; set; }

		#region Internal Attached Properties

		internal static bool GetIsInitialized(DependencyObject obj) => (bool)obj.GetValue(IsInitializedProperty);

		internal static void SetIsInitialized(DependencyObject obj, bool value) => obj.SetValue(IsInitializedProperty, value);

		/// <summary>
		/// Specifies that InitializeElement has executed for the corresponding FrameworkElement
		/// </summary>
		internal static readonly DependencyProperty IsInitializedProperty =
			DependencyProperty.RegisterAttached(
				"IsInitialized",
				typeof(bool),
				typeof(Animations),
				new PropertyMetadata(false));

		internal static Guid GetElementGuid(DependencyObject obj) => (Guid)obj.GetValue(ElementGuidProperty);

		internal static void SetElementGuid(DependencyObject obj, Guid value) => obj.SetValue(ElementGuidProperty, value);

		/// <summary>
		/// Guid attached to a FrameworkElement, used to identify them (used internally).
		/// </summary>
		internal static readonly DependencyProperty ElementGuidProperty =
			DependencyProperty.RegisterAttached(
				"ElementGuid",
				typeof(Guid),
				typeof(Animations),
				new PropertyMetadata(Guid.Empty));

		internal static Guid GetTimelineGuid(DependencyObject obj) => (Guid)obj.GetValue(TimelineGuidProperty);

		internal static void SetTimelineGuid(DependencyObject obj, Guid value) => obj.SetValue(TimelineGuidProperty, value);

		/// <summary>
		/// Guid attached to a FrameworkElement, used to identify them (used internally).
		/// </summary>
		internal static readonly DependencyProperty TimelineGuidProperty =
			DependencyProperty.RegisterAttached(
				"TimelineGuid",
				typeof(Guid),
				typeof(Animations),
				new PropertyMetadata(Guid.Empty));

#if __UWP__
		internal static SpriteVisual GetSprite(DependencyObject obj) => (SpriteVisual)obj.GetValue(SpriteProperty);

		internal static void SetSprite(DependencyObject obj, SpriteVisual value) => obj.SetValue(SpriteProperty, value);

		/// <summary>
		/// Composition Sprite attached to a FrameworkElement.
		/// </summary>
		internal static readonly DependencyProperty SpriteProperty =
			DependencyProperty.RegisterAttached(
				"Sprite",
				typeof(SpriteVisual),
				typeof(Animations),
				new PropertyMetadata(null));
#endif

		internal static CompositeDisposable GetDisposables(DependencyObject obj) => (CompositeDisposable)obj.GetValue(DisposablesProperty);

		internal static void SetDisposables(DependencyObject obj, CompositeDisposable value) => obj.SetValue(DisposablesProperty, value);

		/// <summary>
		///  Internal property to hold an element's disposables
		/// </summary>
		internal static readonly DependencyProperty DisposablesProperty =
			DependencyProperty.RegisterAttached(
				"Disposables",
				typeof(CompositeDisposable),
				typeof(Animations),
				new PropertyMetadata(null));

		#endregion

		#region Public Attached Properties

		public static LogLevel GetEnableLogging(DependencyObject obj) => (LogLevel)obj.GetValue(EnableLoggingProperty);

		public static void SetEnableLogging(DependencyObject obj, LogLevel value) => obj.SetValue(EnableLoggingProperty, value);

		/// <summary>
		/// Specifies if animation data should be logged to the console for debugging purposes.
		/// </summary>
		public static readonly DependencyProperty EnableLoggingProperty =
			DependencyProperty.RegisterAttached(
				"EnableLogging",
				typeof(LogLevel),
				typeof(Animations),
				new PropertyMetadata(LogLevel.None));

		public static DebugTarget GetEnableDebugging(DependencyObject obj) => (DebugTarget)obj.GetValue(EnableDebuggingProperty);

		public static void SetEnableDebugging(DependencyObject obj, DebugTarget value) => obj.SetValue(EnableDebuggingProperty, value);

		/// <summary>
		/// Specifies the target location to start debugging from for the corresponding animation (only when a debugger is attached)
		/// </summary>
		public static readonly DependencyProperty EnableDebuggingProperty =
			DependencyProperty.RegisterAttached(
				"EnableDebugging",
				typeof(DebugTarget),
				typeof(Animations),
				new PropertyMetadata(DebugTarget.None));

		public static bool GetCombinedBinding(DependencyObject obj) => (bool)obj.GetValue(CombinedBindingProperty);

		public static void SetCombinedBinding(DependencyObject obj, bool value) => obj.SetValue(CombinedBindingProperty, value);

		/// <summary>
		/// Triggers the Primary or Secondary animation when a True value is set (on either animation, not both at once).
		/// </summary>
		public static readonly DependencyProperty CombinedBindingProperty =
			DependencyProperty.RegisterAttached(
				"CombinedBinding",
				typeof(bool),
				typeof(Animations),
				new PropertyMetadata(false, OnCombinedBindingChanged));

		public static bool GetPrimaryBinding(DependencyObject obj) => (bool)obj.GetValue(PrimaryBindingProperty);

		public static void SetPrimaryBinding(DependencyObject obj, bool value) => obj.SetValue(PrimaryBindingProperty, value);

		/// <summary>
		/// Triggers the Primary animation when a True value is set.
		/// </summary>
		public static readonly DependencyProperty PrimaryBindingProperty =
			DependencyProperty.RegisterAttached(
				"PrimaryBinding",
				typeof(bool),
				typeof(Animations),
				new PropertyMetadata(false, OnPrimaryBindingChanged));

		public static IAnimationSettings GetPrimary(DependencyObject obj) => (IAnimationSettings)obj.GetValue(PrimaryProperty);

		public static void SetPrimary(DependencyObject obj, IAnimationSettings value) => obj.SetValue(PrimaryProperty, value);

		/// <summary>
		/// The Primary animation intended to run on FrameworkElement's Loaded event or a value change on PrimaryBinding.
		/// </summary>
		public static readonly DependencyProperty PrimaryProperty =
			DependencyProperty.RegisterAttached(
				"Primary",
				typeof(IAnimationSettings),
				typeof(Animations),
				new PropertyMetadata(null, OnPrimaryChanged));

		public static bool GetSecondaryBinding(DependencyObject obj) => (bool)obj.GetValue(SecondaryBindingProperty);

		public static void SetSecondaryBinding(DependencyObject obj, bool value) => obj.SetValue(SecondaryBindingProperty, value);

		/// <summary>
		/// Triggers the Secondary animation when a True value is set.
		/// </summary>
		public static readonly DependencyProperty SecondaryBindingProperty =
			DependencyProperty.RegisterAttached(
				"SecondaryBinding",
				typeof(bool),
				typeof(Animations),
				new PropertyMetadata(false, OnSecondaryBindingChanged));

		public static IAnimationSettings GetSecondary(DependencyObject obj) => (IAnimationSettings)obj.GetValue(SecondaryProperty);

		public static void SetSecondary(DependencyObject obj, IAnimationSettings value) => obj.SetValue(SecondaryProperty, value);

		/// <summary>
		/// The Secondary animation intended to run only through a value change on SecondaryBinding.
		/// </summary>
		public static readonly DependencyProperty SecondaryProperty =
			DependencyProperty.RegisterAttached(
				"Secondary",
				typeof(IAnimationSettings),
				typeof(Animations),
				new PropertyMetadata(null, OnSecondaryChanged));

		public static AnimationSettings GetStartWith(DependencyObject obj) => (AnimationSettings)obj.GetValue(StartWithProperty);

		public static void SetStartWith(DependencyObject obj, AnimationSettings value) => obj.SetValue(StartWithProperty, value);

		/// <summary>
		/// Initializes the element with the specified settings.
		/// </summary>
		public static readonly DependencyProperty StartWithProperty =
			DependencyProperty.RegisterAttached(
				"StartWith",
				typeof(AnimationSettings),
				typeof(Animations),
				new PropertyMetadata(null, OnStartWithChanged));

		public static int GetIterationCount(DependencyObject obj) => (int)obj.GetValue(IterationCountProperty);

		public static void SetIterationCount(DependencyObject obj, int value) => obj.SetValue(IterationCountProperty, value);

		public static readonly DependencyProperty IterationCountProperty =
			DependencyProperty.RegisterAttached(
				"IterationCount",
				typeof(int),
				typeof(Animations),
				new PropertyMetadata(1));

		public static IterationBehavior GetIterationBehavior(DependencyObject obj) => (IterationBehavior)obj.GetValue(IterationBehaviorProperty);

		public static void SetIterationBehavior(DependencyObject obj, IterationBehavior value) => obj.SetValue(IterationBehaviorProperty, value);

		/// <summary>
		/// Specifies the iteration behavior for the animation
		/// </summary>
		public static readonly DependencyProperty IterationBehaviorProperty =
			DependencyProperty.RegisterAttached(
				"IterationBehavior",
				typeof(IterationBehavior),
				typeof(Animations),
				new PropertyMetadata(IterationBehavior.Count));

		public static ICommand GetPrimaryCompletionCommand(DependencyObject obj) => (ICommand)obj.GetValue(PrimaryCompletionCommandProperty);

		public static void SetPrimaryCompletionCommand(DependencyObject obj, ICommand value) => obj.SetValue(PrimaryCompletionCommandProperty, value);

		/// <summary>
		/// Specifies a command that executes when the Primary animation completes
		/// </summary>
		public static readonly DependencyProperty PrimaryCompletionCommandProperty =
			DependencyProperty.RegisterAttached(
				"PrimaryCompletionCommand",
				typeof(ICommand),
				typeof(Animations),
				new PropertyMetadata(null));

		public static ICommand GetSecondaryCompletionCommand(DependencyObject obj) => (ICommand)obj.GetValue(SecondaryCompletionCommandProperty);

		public static void SetSecondaryCompletionCommand(DependencyObject obj, ICommand value) => obj.SetValue(SecondaryCompletionCommandProperty, value);

		/// <summary>
		/// Specifies a command that executes when the Secondary animation completes
		/// </summary>
		public static readonly DependencyProperty SecondaryCompletionCommandProperty =
			DependencyProperty.RegisterAttached(
				"SecondaryCompletionCommand",
				typeof(ICommand),
				typeof(Animations),
				new PropertyMetadata(null));

#if __WPF__
		public static bool GetAllowOpacityReset(DependencyObject obj) => (bool)obj.GetValue(AllowOpacityResetProperty);

		public static void SetAllowOpacityReset(DependencyObject obj, bool value) => obj.SetValue(AllowOpacityResetProperty, value);

		/// <summary>
		/// Allows the Opacity value to be reset after being animated (stops the animation from influencing the property).
		/// </summary>
		/// <remarks>
		/// https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/how-to-set-a-property-after-animating-it-with-a-storyboard?view=netframework-4.7.2#remove-an-animation-from-an-individual-property
		/// </remarks>
		public static readonly DependencyProperty AllowOpacityResetProperty =
			DependencyProperty.RegisterAttached(
				"AllowOpacityReset",
				typeof(bool),
				typeof(Animations),
				new PropertyMetadata(true));
#endif

		#endregion
	}
}