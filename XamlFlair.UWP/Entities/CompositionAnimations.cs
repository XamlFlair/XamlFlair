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

		internal double Duration { get; set; } = AnimationSettings.DEFAULT_DURATION;

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

			var duration = Duration == AnimationSettings.DEFAULT_DURATION
				? Settings.Duration
				: Duration;

			_visual.Size = new Vector2((float)_element.ActualWidth, (float)_element.ActualHeight);
			_visual.CenterPoint = GetTransformCenter();

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

		protected Vector3 GetTransformCenter()
		{
			var centerX = (float)(_element.ActualWidth * Settings.TransformCenterPoint.X);
			var centerY = (float)(_element.ActualHeight * Settings.TransformCenterPoint.Y);

			return new Vector3(centerX, centerY, 0f);
		}
	}

	internal abstract class ScalarAnimationBase : AnimationBase
	{
		internal double To { get; set; }

		protected ScalarAnimationBase() { }

		protected ScalarAnimationBase(string targetProperty) => TargetProperty = targetProperty;

		internal override void Start(FrameworkElement element, bool isFrom = false)
		{
			if (this is TranslateXAnimation || this is TranslateYAnimation)
			{
				// The new way of handling translate animations (see Translation property section):
				// https://blogs.windows.com/buildingapps/2017/06/22/sweet-ui-made-possible-easy-windows-ui-windows-10-creators-update/
				ElementCompositionPreview.SetIsTranslationEnabled(element, true);
			}

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
			var saturate = isFrom ? values.saturate : AnimationSettings.DEFAULT_SATURATION;
			var tint = isFrom ? values.tint : AnimationSettings.DEFAULT_TINT;

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

			var factory = compositor.CreateEffectFactory(saturationEffect, new[]
			{
				TargetProperties.BlurEffectAmount,
				TargetProperties.TintEffectColor,
				TargetProperties.SaturationEffectAmount,
			});

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

			// Attach the effect on the element to retrieve later
			Animations.SetSprite(element, sprite);

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
					InitializeEffect(element, isFrom, () => (0f, AnimationSettings.DEFAULT_SATURATION, AnimationSettings.DEFAULT_TINT));

					sprite = Animations.GetSprite(element) as SpriteVisual;
				}

				var animation = CreateAnimationWithKeyFrame();

				animation.DelayTime = TimeSpan.FromMilliseconds(Settings.Delay);
				animation.Duration = TimeSpan.FromMilliseconds(duration);

				_animation = animation;

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

	//internal class SkewMatrixAnimation : ExpressionAnimationBase
	//{
	//	public SkewMatrixAnimation() : base("TransformMatrix")
	//	{
	//		InitializeAction = () =>
	//		{
	//			// Insert scalar properties into a PropertySet for animated matrix entries 
	//			_visual.Properties.InsertScalar("SkewX", 0f);
	//			_visual.Properties.InsertScalar("SkewY", 0f);
	//		};

	//		// Set reference paramter
	//		PostCreationAction = expressionAnim
	//			=> expressionAnim.SetReferenceParameter("visual", _visual);
	//	}
	//}
	
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