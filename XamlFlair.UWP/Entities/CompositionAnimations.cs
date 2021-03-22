using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas.Effects;
using Windows.Graphics.Effects;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media.Animation;
using XamlFlair.Extensions;
using static XamlFlair.Constants;

namespace XamlFlair
{
	internal abstract class AnimationBase
	{
		protected Visual _visual;
		protected FrameworkElement _element;
		protected CompositionAnimation _animation;

		internal double Duration { get; set; } = DefaultSettings.Duration;

		internal AnimationSettings Settings { get; set; }

		internal string TargetProperty { get; set; }

		internal abstract void Start(FrameworkElement element, bool isFrom = false);

		internal void Stop()
		{
			_animation?.StopAnimation(TargetProperty);
			_animation?.Dispose();
			_animation = null;
		}

		protected double Initialize(FrameworkElement element)
		{
			if (_element == null)
			{
				_element = element;
			}

			if (_visual == null)
			{
				_visual = ElementCompositionPreview.GetElementVisual(_element);
			}

			var duration = Duration == DefaultSettings.Duration
				? Settings.Duration
				: Duration;

			_visual.Size = new Vector2((float)_element.ActualWidth, (float)_element.ActualHeight);
			_visual.CenterPoint = _element.GetTransformCenter(Settings);

			return duration;
		}

		protected T StartAnimation<T>(FrameworkElement element, Func<double, T> animationSetupAction)
			where T : KeyFrameAnimation
		{
			var duration = Initialize(element);

			// Call a Func that will be used to setup the animation and create the keyframes, delay, etc.
			var animation = animationSetupAction(duration);

			_visual.StartAnimation(TargetProperty, animation);

			return animation;
		}

		protected T StartExpressionAnimation<T>(FrameworkElement element, Func<T> expressionSetupAction)
			where T : CompositionAnimation
		{
			Initialize(element);

			// Call a Func that will be used to setup the animation and create the expression, etc.
			var expression = expressionSetupAction();

			_visual.StartAnimation(TargetProperty, expression);

			return expression;
		}
	}

	internal abstract class ScalarAnimationBase : AnimationBase
	{
		internal double To { get; set; }

		protected ScalarAnimationBase() { }

		protected ScalarAnimationBase(string targetProperty) => TargetProperty = targetProperty;

		internal override void Start(FrameworkElement element, bool isFrom = false)
		{
			_animation = base.StartAnimation<ScalarKeyFrameAnimation>(element,
				duration =>
				{
					// TODO: Can we directly set the values instead of animating (ONLY for "from" animations)...

					var scalarAnimation = Window.Current.Compositor.CreateScalarKeyFrameAnimation();

					// It's very important to use a Delay value of 0 for all "from" animations
					var delayTime = isFrom ? 0 : Settings.Delay;

					scalarAnimation.InsertKeyFrame(1.0f, (float)To, Window.Current.Compositor.CreateEasingFunction(Settings.Easing, Settings.EasingMode));
					scalarAnimation.DelayTime = TimeSpan.FromMilliseconds(delayTime);
					scalarAnimation.Duration = TimeSpan.FromMilliseconds(duration);

					return scalarAnimation;
				});
		}
	}

	// TODO: If not used anymore, remove it (or simply keep for any future need?)
	internal abstract class VectorAnimationBase : AnimationBase
	{
		internal Vector3 To { get; set; }

		protected VectorAnimationBase() { }

		protected VectorAnimationBase(string targetProperty) => TargetProperty = targetProperty;

		internal override void Start(FrameworkElement element, bool isFrom = false)
		{
			_animation = base.StartAnimation<Vector3KeyFrameAnimation>(element,
				duration =>
				{
					// TODO: Can we directly set the values instead of animating (ONLY for "from" animations)...

					var vectorAnimation = Window.Current.Compositor.CreateVector3KeyFrameAnimation();

					// It's very important to use a Delay value of 0 for all "from" animations
					var delayTime = isFrom ? 0 : Settings.Delay;

					vectorAnimation.InsertKeyFrame(1.0f, To, Window.Current.Compositor.CreateEasingFunction(Settings.Easing, Settings.EasingMode));
					vectorAnimation.DelayTime = TimeSpan.FromMilliseconds(delayTime);
					vectorAnimation.Duration = TimeSpan.FromMilliseconds(duration);

					return vectorAnimation;
				});
		}
	}

	internal abstract class EffectAnimationBase<T> : AnimationBase
	{
		internal T To { get; set; }

		protected EffectAnimationBase(string targetProperty) => TargetProperty = targetProperty;

		protected IGraphicsEffect InitializeEffect(
			FrameworkElement element,
			bool isFrom,
			Func<(double blur, double saturate, Color tint)> setValuesFunc)
		{
			/*
				(input) Backdrop -> GaussianBlur -> |
									 ColorSource -> | Blend -> Saturation (output)
			 */

			var compositor = _visual.Compositor;

			var values = setValuesFunc();
			var blur = isFrom ? values.blur : 0f;
			var saturate = isFrom ? values.saturate : DefaultSettings.Saturation;
			var tint = isFrom ? values.tint : DefaultSettings.Tint;

			var blurEffect = new GaussianBlurEffect()
			{
				Name = TargetProperties.BlurEffect,
				BlurAmount = (float)blur,
				BorderMode = EffectBorderMode.Hard,
				Optimization = EffectOptimization.Balanced,
				Source = new CompositionEffectSourceParameter(TargetProperties.BackDrop),
			};

			var colorEffect = new ColorSourceEffect
			{
				Name = TargetProperties.TintEffect,
			};

			var blendEffect = new BlendEffect
			{
				Background = blurEffect,
				Foreground = colorEffect,
				Mode = BlendEffectMode.Overlay,
			};

			var saturationEffect = new SaturationEffect
			{
				Name = TargetProperties.SaturationEffect,
				Source = blendEffect,
				Saturation = (float)saturate,
			};

			var targetProperties = new[]
			{
				TargetProperties.BlurEffectAmount,
				TargetProperties.TintEffectColor,
				TargetProperties.SaturationEffectAmount,
			};

			using (var factory = compositor.CreateEffectFactory(saturationEffect, targetProperties))
			{
				var brush = factory.CreateBrush();

				brush.SetSourceParameter(TargetProperties.BackDrop, compositor.CreateBackdropBrush());

				// Animatable properties
				brush.Properties.InsertScalar(TargetProperties.BlurEffectAmount, (float)blur);
				brush.Properties.InsertColor(TargetProperties.TintEffectColor, tint);
				brush.Properties.InsertScalar(TargetProperties.SaturationEffectAmount, (float)saturate);

				var sprite = compositor.CreateSpriteVisual();
				sprite.Brush = brush;
				sprite.Size = _visual.Size;
				ElementCompositionPreview.SetElementChildVisual(element, sprite);

				// Clear any previous applied effect
				var previousEffect = Animations.GetSprite(element);
				previousEffect?.Dispose();
				previousEffect = null;

				// Attach the effect on the element to retrieve later
				Animations.SetSprite(element, sprite);
			}

			return saturationEffect;
		}

		internal override void Start(FrameworkElement element, bool isFrom = false)
		{
			if (isFrom)
			{
				Initialize(element);
				InitializeEffect(element, isFrom, () => (Settings.BlurRadius, Settings.Saturation, Settings.Tint));
			}
			else
			{
				var duration = Initialize(element);
				var sprite = Animations.GetSprite(element) as SpriteVisual;

				if (sprite != null)
				{
					ElementCompositionPreview.SetElementChildVisual(element, sprite);
				}
				else
				{
					InitializeEffect(element, isFrom, () => (0f, DefaultSettings.Saturation, DefaultSettings.Tint));

					sprite = Animations.GetSprite(element) as SpriteVisual;
				}

				_animation = CreateAnimationWithKeyFrame();

				((KeyFrameAnimation)_animation).DelayTime = TimeSpan.FromMilliseconds(Settings.Delay);
				((KeyFrameAnimation)_animation).Duration = TimeSpan.FromMilliseconds(duration);

				sprite.Brush.StartAnimation(TargetProperty, _animation);
			}
		}

		protected abstract KeyFrameAnimation CreateAnimationWithKeyFrame();
	}

	internal abstract class ExpressionAnimationBase : AnimationBase
	{
		protected Action InitializeAction { get; set; }

		protected Action<ExpressionAnimation> PostCreationAction { get; set; }

		internal string Expression { get; set; }

		protected ExpressionAnimationBase(string targetProperty)
		{
			TargetProperty = targetProperty;
		}

		internal override void Start(FrameworkElement element, bool isFrom = false)
		{
			_animation = base.StartExpressionAnimation<ExpressionAnimation>(element,
				() =>
				{
					// Invoke an action to execute initalization code prior to
					// creation of the expression animation
					InitializeAction?.Invoke();

					var expression = Window.Current.Compositor.CreateExpressionAnimation(Expression);

					// Invoke an action to execute post-creation code of the expression animation
					PostCreationAction?.Invoke(expression);

					return expression;
				});
		}
	}

	internal abstract class PerspectiveAnimationBase : AnimationBase
	{
		internal double To { get; set; }

		internal Vector3 RotationAxis { get; private set; }

		internal float Depth { get; private set; }

		protected PerspectiveAnimationBase(string targetProperty, Vector3 rotationAxis, float depth)
		{
			TargetProperty = targetProperty;
			RotationAxis = rotationAxis;
			Depth = depth;
		}

		internal override void Start(FrameworkElement element, bool isFrom = false)
		{
			_animation = base.StartAnimation<ScalarKeyFrameAnimation>(element,
				duration =>
				{
					if (element.Parent == null)
					{
						// Return an empty animation as a fail-safe when no parent exists
						return ElementCompositionPreview
							.GetElementVisual(element)
							.Compositor
							.CreateScalarKeyFrameAnimation();
					}

					var parent = ElementCompositionPreview.GetElementVisual(element.Parent as FrameworkElement);

					var width = (float)element.ActualWidth;
					var height = (float)element.ActualHeight;
					var halfWidth = (float)(width / 2.0);
					var halfHeight = (float)(height / 2.0);

					// Initialize the Compositor
					var visual = ElementCompositionPreview.GetElementVisual(element);

					var projectionMatrix = new Matrix4x4(1, 0, 0, 0,
														 0, 1, 0, 0,
														 0, 0, 1, 1 / Depth,
														 0, 0, 0, 1);
					// To ensure that the rotation occurs through the center of the visual rather than the
					// left edge, pre-multiply the rotation matrix with a translation that logically shifts
					// the axis to the point of rotation, then restore the original location
					parent.TransformMatrix = Matrix4x4.CreateTranslation(-halfWidth, -halfHeight, 0) *
											projectionMatrix *
											Matrix4x4.CreateTranslation(halfWidth, halfHeight, 0);

					visual.RotationAxis = RotationAxis;

					var rotateAnimation = visual.Compositor.CreateScalarKeyFrameAnimation();

					// It's very important to use a Delay value of 0 for all "from" animations
					var delayTime = isFrom ? 0 : Settings.Delay;

					rotateAnimation.InsertKeyFrame(1.0f, (float)To, Window.Current.Compositor.CreateEasingFunction(Settings.Easing, Settings.EasingMode));
					rotateAnimation.DelayTime = TimeSpan.FromMilliseconds(delayTime);
					rotateAnimation.Duration = TimeSpan.FromMilliseconds(duration);

					return rotateAnimation;
				});
		}
	}

	internal class FadeAnimation : ScalarAnimationBase
	{
		public FadeAnimation() : base(TargetProperties.Opacity) { }
	}

	internal class TranslateXAnimation : ScalarAnimationBase
	{
		public TranslateXAnimation() : base(TargetProperties.TranslationX) { }
	}

	internal class TranslateYAnimation : ScalarAnimationBase
	{
		public TranslateYAnimation() : base(TargetProperties.TranslationY) { }
	}

	internal class TranslateZAnimation : ScalarAnimationBase
	{
		public TranslateZAnimation() : base(TargetProperties.TranslationZ) { }
	}

	internal class ScaleXAnimation : ScalarAnimationBase
	{
		public ScaleXAnimation() : base(TargetProperties.ScaleX) { }
	}

	internal class ScaleYAnimation : ScalarAnimationBase
	{
		public ScaleYAnimation() : base(TargetProperties.ScaleY) { }
	}

	internal class ScaleZAnimation : ScalarAnimationBase
	{
		public ScaleZAnimation() : base(TargetProperties.ScaleZ) { }
	}

	internal class RotateAnimation : ScalarAnimationBase
	{
		public RotateAnimation() : base(TargetProperties.RotationAngleInDegrees) { }
	}

	internal class SwivelXAnimation : PerspectiveAnimationBase
	{
		public SwivelXAnimation() : base(
			TargetProperties.RotationAngleInDegrees,
			rotationAxis: new Vector3(1, 0, 0),
			Perspective.Depth) { }
	}

	internal class SwivelYAnimation : PerspectiveAnimationBase
	{
		public SwivelYAnimation() : base(
			TargetProperties.RotationAngleInDegrees,
			rotationAxis: new Vector3(0, 1, 0),
			Perspective.Depth) { }
	}

	internal class BlurAnimation : EffectAnimationBase<double>
	{
		public BlurAnimation() : base(TargetProperties.BlurEffectAmount) { }

		protected override KeyFrameAnimation CreateAnimationWithKeyFrame()
		{
			var scalarAnimation = Window.Current.Compositor.CreateScalarKeyFrameAnimation();
			scalarAnimation.InsertKeyFrame(1.0f, (float)To, Window.Current.Compositor.CreateEasingFunction(Settings.Easing, Settings.EasingMode));

			return scalarAnimation;
		}
	}

	internal class SaturateAnimation : EffectAnimationBase<double>
	{
		public SaturateAnimation() : base(TargetProperties.SaturationEffectAmount) { }

		protected override KeyFrameAnimation CreateAnimationWithKeyFrame()
		{
			var scalarAnimation = Window.Current.Compositor.CreateScalarKeyFrameAnimation();
			scalarAnimation.InsertKeyFrame(1.0f, (float)To, Window.Current.Compositor.CreateEasingFunction(Settings.Easing, Settings.EasingMode));

			return scalarAnimation;
		}
	}

	internal class TintAnimation : EffectAnimationBase<Color>
	{
		public TintAnimation() : base(TargetProperties.TintEffectColor) { }

		protected override KeyFrameAnimation CreateAnimationWithKeyFrame()
		{
			var colorAnimation = Window.Current.Compositor.CreateColorKeyFrameAnimation();
			colorAnimation.InsertKeyFrame(1.0f, To, Window.Current.Compositor.CreateEasingFunction(Settings.Easing, Settings.EasingMode));

			return colorAnimation;
		}
	}
}