using System;

namespace XamlFlair
{
	// NOTE: You can store a maximum of 32 different flags in an enumerated type,
	// because an enum type uses 4 bytes (32 bits) of storage

	[Flags]
	public enum AnimationKind
	{
#if __UWP__ || __WPF__
		BlurTo = 1 << 0,
		BlurFrom = 1 << 1,
#endif
		FadeTo = 1 << 2,
		FadeFrom = 1 << 3,
		RotateTo = 1 << 4,
		RotateFrom = 1 << 5,
		ScaleXTo = 1 << 6,
		ScaleYTo = 1 << 7,
		ScaleXFrom = 1 << 8,
		ScaleYFrom = 1 << 9,
		TranslateXTo = 1 << 10,
		TranslateYTo = 1 << 11,
		TranslateXFrom = 1 << 12,
		TranslateYFrom = 1 << 13,
#if __UWP__
		ScaleZTo = 1 << 14,
		ScaleZFrom = 1 << 15,
		TranslateZTo = 1 << 16,
		TranslateZFrom = 1 << 17,
		SaturateTo = 1 << 18,
		SaturateFrom = 1 << 19,
		TintTo = 1 << 20,
		TintFrom = 1 << 21,
#endif
	}
}