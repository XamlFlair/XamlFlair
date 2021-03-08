using System;

namespace XamlFlair
{
	// NOTE: You can store a maximum of 64 different flags in an enumerated
	// type, because this enum type uses 8 bytes (64 bits) of storage

	[Flags]
	public enum AnimationKind : long
	{
		FadeTo = 1 << 0,
		FadeFrom = 1 << 1,
		RotateTo = 1 << 2,
		RotateFrom = 1 << 3,
		ScaleXTo = 1 << 4,
		ScaleYTo = 1 << 5,
		ScaleXFrom = 1 << 6,
		ScaleYFrom = 1 << 7,
		TranslateXTo = 1 << 8,
		TranslateYTo = 1 << 9,
		TranslateXFrom = 1 << 10,
		TranslateYFrom = 1 << 11,
#if __UWP__
		ScaleZTo = 1 << 12,
		ScaleZFrom = 1 << 13,
		TranslateZTo = 1 << 14,
		TranslateZFrom = 1 << 15,
		SaturateTo = 1 << 16,
		SaturateFrom = 1 << 17,
		TintTo = 1 << 18,
		TintFrom = 1 << 19,
		SwivelXTo = 1 << 20,
		SwivelXFrom = 1 << 21,
		SwivelYTo = 1 << 22,
		SwivelYFrom = 1 << 23,
#endif
		// Blur supported only on UWP and WPF
#if (__UWP__ && !HAS_UNO) || __WPF__
		BlurTo = 1 << 24,
		BlurFrom = 1 << 25,
#endif
		// ColorAnimation supported only on Uno and WPF (not on native UWP due to Composition-only implementations)
#if WINDOWS_UWP || HAS_UNO || __WPF__
		ColorTo = 1 << 26,
		ColorFrom = 1 << 27,
#endif
	}
}