using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using XamlFlair.WPF.Logging;

namespace XamlFlair.Extensions
{
	internal static class AnimationExtensions
	{
		private const short SCALE_INDEX = 0;
		private const short ROTATE_INDEX = 2;
		private const short TRANSLATE_INDEX = 3;

		// ====================
		// FADE
		// ====================

		internal static Storyboard FadeTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			// Since a previous animation can have a "hold" on the Opacity property, we must "release" it before
			// setting a new value. See the Remarks section of the AttachedProperty for more info.
			if (Animations.GetAllowOpacityReset(element))
			{
				element.BeginAnimation(UIElement.OpacityProperty, null);
			}

			element.ApplyAnimation(settings, element.Opacity, settings.Opacity, "Opacity", ref storyboard);

			return storyboard;
		}

		internal static Storyboard FadeFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			// Since a previous animation can have a "hold" on the Opacity property, we must "release" it before
			// setting a new value. See the Remarks section of the AttachedProperty for more info.
			if (Animations.GetAllowOpacityReset(element))
			{
				element.BeginAnimation(UIElement.OpacityProperty, null);
			}

			element.Opacity = settings.Opacity;

			element.ApplyAnimation(settings, settings.Opacity, 1, "Opacity", ref storyboard);

			return storyboard;
		}

		// ====================
		// TRANSLATE
		// ====================

		internal static Storyboard TranslateXTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as TransformGroup) ?? CreateTransformGroup();
			var translate = transform.Children[TRANSLATE_INDEX] as TranslateTransform;

			SetRenderTransform(element, settings, transform);

			element.ApplyAnimation(settings, translate.X, element.GetCalculatedOffsetX(settings.OffsetX), $"RenderTransform.Children[{TRANSLATE_INDEX}].X", ref storyboard);

			return storyboard;
		}

		internal static Storyboard TranslateYTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as TransformGroup) ?? CreateTransformGroup();
			var translate = transform.Children[TRANSLATE_INDEX] as TranslateTransform;

			SetRenderTransform(element, settings, transform);

			element.ApplyAnimation(settings, translate.Y, element.GetCalculatedOffsetY(settings.OffsetY), $"RenderTransform.Children[{TRANSLATE_INDEX}].Y", ref storyboard);

			return storyboard;
		}

		internal static Storyboard TranslateXFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as TransformGroup) ?? CreateTransformGroup();
			var translate = transform.Children[TRANSLATE_INDEX] as TranslateTransform;

			translate.X = element.GetCalculatedOffsetX(settings.OffsetX);

			SetRenderTransform(element, settings, transform);

			element.ApplyAnimation(settings, translate.X, 0, $"RenderTransform.Children[{TRANSLATE_INDEX}].X", ref storyboard);

			return storyboard;
		}

		internal static Storyboard TranslateYFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as TransformGroup) ?? CreateTransformGroup();
			var translate = transform.Children[TRANSLATE_INDEX] as TranslateTransform;

			translate.Y = element.GetCalculatedOffsetY(settings.OffsetY);

			SetRenderTransform(element, settings, transform);

			element.ApplyAnimation(settings, translate.Y, 0, $"RenderTransform.Children[{TRANSLATE_INDEX}].Y", ref storyboard);

			return storyboard;
		}

		// ====================
		// SCALE
		// ====================

		internal static Storyboard ScaleXTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as TransformGroup) ?? CreateTransformGroup();
			var scale = transform.Children[SCALE_INDEX] as ScaleTransform;

			SetRenderTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, scale.ScaleX, settings.ScaleX, $"RenderTransform.Children[{SCALE_INDEX}].ScaleX", ref storyboard);

			return storyboard;
		}

		internal static Storyboard ScaleYTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as TransformGroup) ?? CreateTransformGroup();
			var scale = transform.Children[SCALE_INDEX] as ScaleTransform;

			SetRenderTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, scale.ScaleY, settings.ScaleY, $"RenderTransform.Children[{SCALE_INDEX}].ScaleY", ref storyboard);

			return storyboard;
		}

		internal static Storyboard ScaleXFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as TransformGroup) ?? CreateTransformGroup();
			var scale = transform.Children[SCALE_INDEX] as ScaleTransform;

			scale.ScaleX = settings.ScaleX;

			SetRenderTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, settings.ScaleX, 1, $"RenderTransform.Children[{SCALE_INDEX}].ScaleX", ref storyboard);

			return storyboard;
		}

		internal static Storyboard ScaleYFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as TransformGroup) ?? CreateTransformGroup();
			var scale = transform.Children[SCALE_INDEX] as ScaleTransform;

			scale.ScaleY = settings.ScaleY;

			SetRenderTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, settings.ScaleY, 1, $"RenderTransform.Children[{SCALE_INDEX}].ScaleY", ref storyboard);

			return storyboard;
		}

		// ====================
		// ROTATE
		// ====================

		internal static Storyboard RotateTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as TransformGroup) ?? CreateTransformGroup();
			var rotate = transform.Children[ROTATE_INDEX] as RotateTransform;

			SetRenderTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, rotate.Angle, settings.Rotation, $"RenderTransform.Children[{ROTATE_INDEX}].Angle", ref storyboard);

			return storyboard;
		}

		internal static Storyboard RotateFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var transform = (element.RenderTransform as TransformGroup) ?? CreateTransformGroup();
			var rotate = transform.Children[ROTATE_INDEX] as RotateTransform;

			rotate.Angle = settings.Rotation;

			SetRenderTransform(element, settings, transform, updateTransformCenterPoint: true);

			element.ApplyAnimation(settings, settings.Rotation, 0, $"RenderTransform.Children[{ROTATE_INDEX}].Angle", ref storyboard);

			return storyboard;
		}

		// ====================
		// BLUR
		// ====================

		internal static Storyboard BlurTo(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			var effect = (element.Effect as BlurEffect) ?? new BlurEffect();
			element.Effect = effect;

			element.ApplyAnimation(settings, effect.Radius, settings.BlurRadius, "Effect.Radius", ref storyboard);

			return storyboard;
		}

		internal static Storyboard BlurFrom(this FrameworkElement element, AnimationSettings settings, ref Storyboard storyboard)
		{
			element.Effect = (element.Effect as BlurEffect) ?? new BlurEffect();

			element.ApplyAnimation(settings, settings.BlurRadius, 0, "Effect.Radius", ref storyboard);

			return storyboard;
		}

		private static void SetRenderTransform(FrameworkElement element, AnimationSettings settings, TransformGroup transform, bool updateTransformCenterPoint = false)
		{
			element.RenderTransform = transform;

			if (updateTransformCenterPoint)
			{
				element.RenderTransformOrigin = settings.TransformCenterPoint;
			}
		}

		private static void ApplyAnimation(this FrameworkElement element, AnimationSettings settings, double from, double to, string targetProperty, ref Storyboard storyboard)
		{
			var anim = new DoubleAnimation()
			{
				From = from,
				To = to,
				Duration = new Duration(TimeSpan.FromMilliseconds(settings.Duration)),
				BeginTime = TimeSpan.FromMilliseconds(settings.Delay),
				EasingFunction = settings.GetEase()
			};

			Storyboard.SetTarget(anim, element);
			Storyboard.SetTargetProperty(anim, new PropertyPath(targetProperty));

			storyboard.Children.Add(anim);

			// If the element is ListBoxItem-based, we must check the logging property on its parent ListBox
			if (element is ListBoxItem
				&& element.FindAscendant<ListBox>() is ListBox lb
				&& Animations.GetEnableLogging(lb))
			{
				// Log for a ListBoxItem
				element.LogAnimationInfo(targetProperty, anim, settings);
			}
			else if (Animations.GetEnableLogging(element))
			{
				// Log for a FrameworkElement
				element.LogAnimationInfo(targetProperty, anim, settings);
			}
		}

		internal static void ApplyInitialSettings(this FrameworkElement element, AnimationSettings settings)
		{
			var transform = (element.RenderTransform as TransformGroup) ?? CreateTransformGroup();
			var rotate = (transform.Children[ROTATE_INDEX] as RotateTransform);
			var scale = (transform.Children[SCALE_INDEX] as ScaleTransform);
			var translate = (transform.Children[TRANSLATE_INDEX] as TranslateTransform);

			rotate.Angle = settings.Rotation;
			scale.ScaleX = settings.ScaleX;
			scale.ScaleY = settings.ScaleY;
			translate.X = element.GetCalculatedOffsetX(settings.OffsetX);
			translate.Y = element.GetCalculatedOffsetY(settings.OffsetY);

			// Since a previous animation can have a "hold" on the Opacity property, we must "release" it before
			// setting a new value. See the Remarks section of the AttachedProperty for more info.
			if (Animations.GetAllowOpacityReset(element))
			{
				element.BeginAnimation(UIElement.OpacityProperty, null);
			}

			element.Opacity = settings.Opacity;

			element.RenderTransformOrigin = settings.TransformCenterPoint;
			element.RenderTransform = transform;

			var effect = (element.Effect as BlurEffect) ?? new BlurEffect()
			{
				Radius = settings.BlurRadius
			};
			element.Effect = effect;
		}

		private static TransformGroup CreateTransformGroup()
		{
			var group = new TransformGroup();

			group.Children.Add(new ScaleTransform(1, 1));
			group.Children.Add(new SkewTransform(0, 0));
			group.Children.Add(new RotateTransform(0));
			group.Children.Add(new TranslateTransform(0, 0));

			return group;
		}

		private static void LogAnimationInfo(this FrameworkElement element, string targetProperty, DoubleAnimation anim, AnimationSettings settings)
		{
			// Build the "element" output with Name + Type if Name exists, else just the Type
			var name = element.Name ?? string.Empty;
			var elementOutput = !string.IsNullOrEmpty(name)
				? $"{name} ({element.GetType()})"
				: element.GetType().ToString();

			var output =
				"\n---------- ANIMATION ----------\n" +
				$"Timestamp = {DateTimeOffset.Now.ToString("HH:mm:ss:fffff")} \n" +
				" Animation: \n" +
					$"	Element = {elementOutput} \n" +
					$"	TargetProperty = {targetProperty} \n" +
					$"	From = {anim.From} \n" +
					$"	To = {anim.To} \n" +
				" Settings: \n" +
					$"	Kind = {settings.Kind} \n" +
					$"	Duration = {settings.Duration} \n" +
					$"	Delay = {settings.Delay} \n" +
					$"	Opacity = {settings.Opacity} \n" +
					$"	OffsetX = {settings.OffsetX} \n" +
					$"	OffsetY = {settings.OffsetY} \n" +
					$"	ScaleX = {settings.ScaleX} \n" +
					$"	ScaleY = {settings.ScaleY} \n" +
					$"	Rotation = {settings.Rotation} \n" +
					$"	Blur = {settings.BlurRadius} \n" +
					$"	TransformCenterPoint = {settings.TransformCenterPoint} \n" +
					$"	Easing = {settings.Easing} \n" +
					$"	EasingMode = {settings.EasingMode} \n" +
				"------------------------------------";

			Animations.Logger.Debug(output);
		}
	}
}