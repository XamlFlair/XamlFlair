using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.Xaml.Media.Animation;

namespace XamlFlair.Extensions
{
	internal static class CompositorExtensions
	{
		private static readonly Dictionary<string, CompositionEasingFunction> _easings = new Dictionary<string, CompositionEasingFunction>();

		internal static CompositionEasingFunction CreateEasingFunction(this Compositor compositor, EasingType easingType, EasingMode mode)
		{
			var key = $"{easingType.ToString()}.{mode.ToString()}";

			if (_easings.TryGetValue(key, out var easingFunc))
			{
				return easingFunc;
			}

			CompositionEasingFunction newEasing;

			switch (easingType)
			{
				case EasingType.Back when mode == EasingMode.EaseIn:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.6f, -0.28f), new Vector2(0.735f, 0.045f));
					break;
				case EasingType.Back when mode == EasingMode.EaseOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.175f, 0.885f), new Vector2(0.32f, 1.275f));
					break;
				case EasingType.Back when mode == EasingMode.EaseInOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.68f, -0.55f), new Vector2(0.265f, 1.55f));
					break;

				case EasingType.Bounce when mode == EasingMode.EaseIn:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.93f, 0.7f), new Vector2(0.4f, -0.93f));
					break;
				case EasingType.Bounce when mode == EasingMode.EaseOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.58f, 1.93f), new Vector2(0.08f, 0.36f));
					break;
				case EasingType.Bounce when mode == EasingMode.EaseInOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.65f, -0.85f), new Vector2(0.35f, 1.85f));
					break;

				case EasingType.Circle when mode == EasingMode.EaseIn:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.6f, 0.04f), new Vector2(0.98f, 0.335f));
					break;
				case EasingType.Circle when mode == EasingMode.EaseOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.075f, 0.82f), new Vector2(0.165f, 1f));
					break;
				case EasingType.Circle when mode == EasingMode.EaseInOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.785f, 0.135f), new Vector2(0.15f, 0.86f));
					break;

				case EasingType.Cubic when mode == EasingMode.EaseIn:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.55f, 0.055f), new Vector2(0.675f, 0.19f));
					break;
				case EasingType.Cubic when mode == EasingMode.EaseOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.215f, 0.61f), new Vector2(0.355f, 1f));
					break;
				case EasingType.Cubic when mode == EasingMode.EaseInOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.645f, 0.045f), new Vector2(0.355f, 1f));
					break;

				case EasingType.Elastic when mode == EasingMode.EaseIn:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(1, 0.78f), new Vector2(.63f, -1.68f));
					break;
				case EasingType.Elastic when mode == EasingMode.EaseOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.37f, 2.68f), new Vector2(0f, 0.22f));
					break;
				case EasingType.Elastic when mode == EasingMode.EaseInOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.9f, -1.2f), new Vector2(0.1f, 2.2f));
					break;

				case EasingType.Quadratic when mode == EasingMode.EaseIn:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.55f, 0.085f), new Vector2(0.68f, 0.53f));
					break;
				case EasingType.Quadratic when mode == EasingMode.EaseOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.25f, 0.46f), new Vector2(0.45f, 0.94f));
					break;
				case EasingType.Quadratic when mode == EasingMode.EaseInOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.445f, 0.03f), new Vector2(0.515f, 0.955f));
					break;

				case EasingType.Quartic when mode == EasingMode.EaseIn:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.895f, 0.03f), new Vector2(0.685f, 0.22f));
					break;
				case EasingType.Quartic when mode == EasingMode.EaseOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.165f, 0.84f), new Vector2(0.44f, 1f));
					break;
				case EasingType.Quartic when mode == EasingMode.EaseInOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.77f, 0f), new Vector2(0.175f, 1f));
					break;

				case EasingType.Quintic when mode == EasingMode.EaseIn:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.755f, 0.05f), new Vector2(0.855f, 0.06f));
					break;
				case EasingType.Quintic when mode == EasingMode.EaseOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.23f, 1f), new Vector2(0.32f, 1f));
					break;
				case EasingType.Quintic when mode == EasingMode.EaseInOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.86f, 0f), new Vector2(0.07f, 1f));
					break;

				case EasingType.Sine when mode == EasingMode.EaseIn:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.47f, 0f), new Vector2(0.745f, 0.715f));
					break;
				case EasingType.Sine when mode == EasingMode.EaseOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.39f, 0.575f), new Vector2(0.565f, 1f));
					break;
				case EasingType.Sine when mode == EasingMode.EaseInOut:
					newEasing = compositor.CreateCubicBezierEasingFunction(new Vector2(0.445f, 0.05f), new Vector2(0.55f, 0.95f));
					break;

				default:
					newEasing = compositor.CreateLinearEasingFunction();
					break;
			}

			_easings.Add(key, newEasing);

			return newEasing;
		}
	}
}