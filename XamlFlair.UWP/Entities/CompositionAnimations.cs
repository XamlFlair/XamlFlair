using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas.Effects;
using Windows.Graphics.Effects;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media.Animation;
using XamlFlair.Extensions;

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

		private double Initialize(FrameworkElement element)
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

		protected T StartEffectAnimation<T>(
			FrameworkElement element,
			IGraphicsEffect effect,
			Func<double, T> animationSetupFunc,
			Action<CompositionEffectBrush> brushSetupAction)
				where T : KeyFrameAnimation
		{
			var duration = Initialize(element);

			// Call a Func that will be used to setup the animation and create the keyframes, delay, etc.
			var animation = animationSetupFunc(duration);
			var effectFactory = Window.Current.Compositor.CreateEffectFactory(effect, new[] { TargetProperty });
			var brush = effectFactory.CreateBrush();

			// Call an Action that will be used to setup the CompositionEffectBrush object
			brushSetupAction(brush);

			brush.StartAnimation(TargetProperty, animation);

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
			var centerX = (float)(_element.ActualWidth * Settings.RenderTransformOrigin.X);
			var centerY = (float)(_element.ActualHeight * Settings.RenderTransformOrigin.Y);

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

	internal abstract class EffectAnimationBase : ScalarAnimationBase
	{
		private readonly IGraphicsEffect _effect;

		protected const string Backdrop = nameof(Backdrop);

		protected EffectAnimationBase(string targetProperty, IGraphicsEffect effect)
		{
			_effect = effect;
			TargetProperty = targetProperty;
		}

		internal override void Start(FrameworkElement element, bool isFrom = false)
		{
			_animation = base.StartEffectAnimation<ScalarKeyFrameAnimation>(element, _effect,
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
				},
				brush =>
				{
					var sprite = Window.Current.Compositor.CreateSpriteVisual();

					sprite.Brush = brush;
					sprite.Size = _visual.Size;

					ElementCompositionPreview.SetElementChildVisual(element, sprite);

					var destinationBrush = Window.Current.Compositor.CreateBackdropBrush();
					brush.SetSourceParameter(Backdrop, destinationBrush);
				});
		}
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
		public FadeAnimation() : base(Constants.TargetProperties.Opacity) { }
	}

	internal class TranslateXAnimation : ScalarAnimationBase
	{
		public TranslateXAnimation() : base(Constants.TargetProperties.TranslationX) { }
	}

	internal class TranslateYAnimation : ScalarAnimationBase
	{
		public TranslateYAnimation() : base(Constants.TargetProperties.TranslationY) { }
	}

	internal class TranslateZAnimation : ScalarAnimationBase
	{
		public TranslateZAnimation() : base(Constants.TargetProperties.TranslationZ) { }
	}

	internal class ScaleXAnimation : ScalarAnimationBase
	{
		public ScaleXAnimation() : base(Constants.TargetProperties.ScaleX) { }
	}

	internal class ScaleYAnimation : ScalarAnimationBase
	{
		public ScaleYAnimation() : base(Constants.TargetProperties.ScaleY) { }
	}

	internal class ScaleZAnimation : ScalarAnimationBase
	{
		public ScaleZAnimation() : base(Constants.TargetProperties.ScaleZ) { }
	}

	internal class RotateAnimation : ScalarAnimationBase
	{
		public RotateAnimation() : base(Constants.TargetProperties.RotationAngleinDegrees) { }
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

	internal class BlurAnimation : EffectAnimationBase
	{
		public BlurAnimation()
			: base(Constants.TargetProperties.BlurAmount,
				new GaussianBlurEffect()
				{
					Name = Constants.TargetProperties.Blur,
					Source = new CompositionEffectSourceParameter(Backdrop),
					BorderMode = EffectBorderMode.Hard,
				})
		{ }
	}
}