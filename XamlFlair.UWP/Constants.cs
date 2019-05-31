using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamlFlair
{
	internal static class Constants
	{
		internal static class TargetProperties
		{
			// NOTE: The new way of handling translate animations (old: "Offset", new: "Translation"):
			// https://github.com/Microsoft/WindowsCompositionSamples/issues/145#issuecomment-286603195
			// https://github.com/Microsoft/WindowsCompositionSamples/blob/5da7a5a8f8d5f0dee984b6170eb5527d0ce7d1c2/SampleGallery/Samples/SDK%2015063/OffsetStompingFix/OffsetStompingFix.xaml.cs#L50

			internal static string Opacity = nameof(Opacity);

			internal static string RotationAngleInDegrees = nameof(RotationAngleInDegrees);

			internal static string Blur = nameof(Blur);

			internal static string BlurAmount = $"{nameof(Blur)}.{nameof(BlurAmount)}";

			internal static string Translation = "Translation";

			internal static string TranslationX = "Translation.X";

			internal static string TranslationY = "Translation.Y";

			internal static string TranslationZ = "Translation.Z";

			internal static string ScaleX = "Scale.X";

			internal static string ScaleY = "Scale.Y";

			internal static string ScaleZ = "Scale.Z";
		}
	}
}