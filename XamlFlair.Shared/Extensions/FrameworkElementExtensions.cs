using System;
using System.Collections.Generic;
using System.Text;

#if __WPF__
using System.Windows;
#else
using Windows.UI.Xaml;
#endif

namespace XamlFlair.Extensions
{
	internal static class FrameworkElementExtensions
	{
		internal static IAnimationSettings GetSettings(
			this FrameworkElement element,
			SettingsTarget target,
			Func<FrameworkElement, IAnimationSettings> getPrimaryFunc = null,
			Func<FrameworkElement, IAnimationSettings> getSecondaryFunc = null,
			Func<FrameworkElement, AnimationSettings> getStartWithFunc = null)
		{
			IAnimationSettings settings = null;

			switch (target)
			{
				case SettingsTarget.Primary:
					settings = getPrimaryFunc(element);
					break;

				case SettingsTarget.Secondary:
					settings = getSecondaryFunc(element);
					break;

				case SettingsTarget.StartWith:
					settings = getStartWithFunc(element);
					break;
			}

			// Settings can be null if a Trigger is set before the associated element is loaded
			if (settings == null)
			{
				return null;
			}

			return settings;
		}

		internal static double GetCalculatedOffsetX(this FrameworkElement element, string translation)
			=> GetCalculatedOffset(
					translation,
					dblValue => element.ActualWidth * dblValue,
					nameof(AnimationSettings.OffsetX));

		internal static double GetCalculatedOffsetY(this FrameworkElement element, string translation)
			=> GetCalculatedOffset(
					translation,
					dblValue => element.ActualHeight * dblValue,
					nameof(AnimationSettings.OffsetY));
		

		private static double GetCalculatedOffset(string translation, Func<double, double> calculateFunc, string propertyName)
		{
			if (translation.EndsWith("*") && double.TryParse(translation.TrimEnd('*'), out var result))
			{
				// Pass the 'double' value retrieved from the string to have it calculate against ActualWidth or ActualHeight
				return calculateFunc(result);
			}
			else if (double.TryParse(translation, out var dbl))
			{
				return dbl;
			}

			throw new ArgumentException($"{propertyName} must be a double or a star-based value (ex: 150 or 0.75*).");
		}
	}
}