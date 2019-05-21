# XamlFlair

The goal of the XamlFlair library is to ease the implementation of common animations and allow a developer to *easily* add a single or combined set of animations with just a few lines of Xaml.

## Install from Nuget

To install **XamlFlair**, run the following command in the **Package Manager Console**:

UWP:

```
Install-Package XamlFlair.UWP
```

WPF:

```
Install-Package XamlFlair.WPF
```

## Basic Concepts

The basic concept of XamlFlair is based on animations that are categorized as _From_ and _To_. Any UI element that consists of a _From_ animation will start with one or more arbitrary value(s), and complete in the original state. Any UI element that consists of a _From_ animation will start in its original state and animate to one or more arbitrary value(s).

Example of a _From_ animation (a UI element sliding into its original state):

![From animation](doc/gifs/From.gif)

Example of a _To_ animation (a UI element sliding away from its original state):

![To animation](doc/gifs/To.gif)

## Usage

To begin, you need to have the following xaml namespace reference:

```xml
xmlns:xf="using:XamlFlair"
```

From here on, it's simple a matter of setting an attached property to any `FrameworkElement` that needs an animation:

```xml
<Border xf:Animations.Primary="{StaticResource FadeIn}" />
```

> **Note**: If your `FrameworkElement` defines a `CompositeTransform` in your xaml, it will be altered during the animation process.

> **Note**: The use of `StaticResource` is to reference common animations, which is discussed in the next section.

### Base animation types

#### Fade

![Fade animation](doc/gifs/FadeIn.gif)

#### Translate

![Translation animation](doc/gifs/Translate.gif)

#### Scale

![Scale animation](doc/gifs/Scale.gif)

#### Rotate

![Rotation animation](doc/gifs/Rotate.gif)

> **Warning**: Be careful when animating `FadeOut`. Be aware that the element remains in the visual if the `Visibility` remains at `Visible`, therefore there may be cases where you'll need to manually manage `IsHitTestVisible` to allow the user to tap *through* the element.

The following lists some notable **default values** when working with XamlFlair:

* **Kind**: FadeTo
* **Duration**: 500
* **Easing**: Cubic
* **Easing Mode**: EaseOut
* **RenderTransformOrigin**: (0.5, 0.5)
* **Event**: Loaded

### Using a `ResourceDictionary` for base settings

All **common** animations should be placed in a global `ResourceDictionary` (ex: `Animations.xaml`) and used where needed throughout the app. The goal is to consolidate all the animations into one file with meaningful names so that any developer can understand exactly what animation is applied to a `FrameworkElement`.

```xml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:xf="using:XamlFlair">

    <x:Double x:Key="PositiveOffset">50</x:Double>
    <x:Double x:Key="NegativeOffset">-50</x:Double>
    <x:Double x:Key="SmallScaleFactor">0.75</x:Double>
    <x:Double x:Key="LargeScaleFactor">1.25</x:Double>

    <xf:AnimationSettings x:Key="FadeIn"
                          Opacity="0"
                          Kind="FadeFrom" />

    <xf:AnimationSettings x:Key="FadeOut"
                          Opacity="0"
                          Kind="FadeTo" />

    <xf:AnimationSettings x:Key="Expand"
                          Scale="{StaticResource LargeScaleFactor}"
                          Kind="ScaleTo" />

    <xf:AnimationSettings x:Key="SlideFromLeft"
                          OffsetX="{StaticResource NegativeOffset}"
                          Kind="TranslateFrom" />

    <xf:AnimationSettings x:Key="SlideFromTop"
                          OffsetY="{StaticResource NegativeOffset}"
                          Kind="TranslateFrom" />
    .
    .
    .
    </ResourceDictionary>
```

### Combining animations

Animations can be combined, and as previously mentioned, any of these *combined* composite animations that are common should be placed in the global `ResourceDictionary` (ex: `Animations.xaml`):

```xml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:xf="using:XamlFlair">
    .
    .
    .
    <xf:AnimationSettings x:Key="FadeInAndSlideFromRight"
                          OffsetX="{StaticResource PositiveOffset}"
                          Kind="FadeFrom,TranslateFrom" />

    <xf:AnimationSettings x:Key="FadeOutAndSlideToLeft"
                          OffsetX="{StaticResource NegativeOffset}"
                          Kind="FadeTo,TranslateTo" />

    <xf:AnimationSettings x:Key="FadeOutAndSlideToTop"
                          OffsetY="{StaticResource NegativeOffset}"
                          Kind="FadeTo,TranslateTo" />

    <xf:AnimationSettings x:Key="FadeInAndInflate"
                          Scale="{StaticResource SmallScaleFactor}"
                          Kind="FadeFrom,ScaleFrom" />

    <xf:AnimationSettings x:Key="FadeInAndSlideFromLeftAndGrow"
                          OffsetX="{StaticResource NegativeOffset}"
                          Scale="{StaticResource SmallScaleFactor}"
                          Kind="FadeFrom,TranslateFrom,ScaleFrom" />
    .
    .
    .
    </ResourceDictionary>
```

This demonstrates a combined animation of a `FadeFrom` and `TranslateFrom`:

![Fade and translation animation](doc/gifs/FadeInTranslate.gif)

This demonstrates a combined animation of a `FadeFrom`, `TranslateFrom`, `ScaleFrom`, and `RotateFrom`:

![Fade, translation, and rotation snimation](doc/gifs/FadeInTranslateRotateScale.gif)

### Overriding values

Primary animations can have their animation settings overridden directly on the `FrameworkElement`. This is commonly done to alter values for Delay and Duration so that we don't over-populate the `Animations.xaml` file with repeated resources. To achieve overriding, use the `Animate` markup extension paired with the `BasedOn` property:

```xml
<Border xf:Animations.Primary="{xf:Animate BasedOn={StaticResource ScaleFromBottom}, Delay=500}">
```

### Compound animation

A compound animation is simply a multi-step animation using the `CompoundSettings` class. Each inner animation executes once the previous one completes, hence they're sequential animations:

![Compound animation](doc/gifs/Compound.gif)

```xml
<xf:CompoundSettings x:Key="Progress">
    <xf:CompoundSettings.Sequence>
        <xf:AnimationSettings ScaleX="0"
                              RenderTransformOrigin="1,0.5"
                              Kind="ScaleTo" />
        <xf:AnimationSettings ScaleX="0"
                              RenderTransformOrigin="1,0.5"
                              Kind="ScaleFrom" />
        <xf:AnimationSettings ScaleX="0"
                              RenderTransformOrigin="0,0.5"
                              Kind="ScaleTo" />
        <xf:AnimationSettings ScaleX="0"
                              RenderTransformOrigin="0,0.5"
                              Kind="ScaleFrom" />
    </xf:CompoundSettings.Sequence>
</xf:CompoundSettings>
```

> **Note**: `CompoundSettings` support the `Event` property, which is discussed in the next section.

### Repeating animations

An animation can be repeated by using the `IterationBehavior` and `IterationCount` properties (default values of `Count` and `1` respectively).

**TODO**: INSERT GIF ...

The following demonstrates how to run an animation only 5 times:

```xml
<Border xf:Animations.Primary="{StaticResource FadeIn}"
        xf:Animations.IterationCount="5" />
```

The following demonstrates how to run an animation indefinitely:

```xml
<Border xf:Animations.Primary="{StaticResource FadeIn}"
        xf:Animations.IterationBehavior="Forever" />
```

Also note that it is also possible to repeat a Compound animation. For example, using the compound animation (named _Progress_) from the previous section:

```xml
<Border xf:Animations.Primary="{StaticResource Progress}"
        xf:Animations.IterationCount="5" />
```

> **Note**: When using repeating animations, you cannot set a `Secondary` animation on the element.

### Events and bindings

By default, all animations execute once the UI element fires its `Loaded` event. This behavior can be overridden by setting the `Event` property. `Event` can be one of the following values:

* Loaded (default value)
* None
* Visibility
* DataContextChanged
* PointerOver
* PointerExit
* GotFocus
* LostFocus

When specifying `None`, you will manually need to trigger your animations using the `PrimaryBinding` or `SecondaryBinding` properties. These properties are of type `bool` and expect a value of `True` in order to execute the corresponding animation. The following is an example of triggering an animation based off the `IsChecked` of the CheckBox control:

```xml
<CheckBox x:Name="SampleCheckBox"
          IsChecked="False" />

<Rectangle xf:Animations.PrimaryBinding="{Binding IsChecked, ElementName=SampleCheckBox}"
           xf:Animations.Primary="{xf:Animate BasedOn={StaticResource FadeIn}, Event=None}" />
```

The above animation will *only* execute when the `IsChecked` is `True`. If `None` was not specified for `Event`, the animation would then execute on `Loaded` *and* on the binding.

### Using the *StartWith* property

There will be cases when you will need your UI element to start in a specific state, for example, the element needs to be shrunk before its animation executes). This is achieved using the `StartWith` property: 

```xml
<Rectangle xf:Animations.Primary="{xf:Animate BasedOn={StaticResource ScaleFromBottom}, Delay=500}"
           xf:Animations.StartWith="{StaticResource ScaleFromBottom}" />
```

In the above example, since the element is scaling from the bottom, but with a delay, we need to _start_ in the _scaled_ position, so we use the `StartWith` property to set it in its initial state. What `StartWith` essentially does is setup the initial values on the element's CompositeTransform as soon as it has loaded.

### Logging animations

**TODO**: EXPLAIN HOW WITH A SERILOG EXAMPLE ...

To output the values of one or more animations, simply set `True` to the `EnableLogging` property on the target `FrameworkElement`:

```xml
<Rectangle xf:Animations.EnableLogging="True"
           xf:Animations.Primary="{StaticResource SlideFromLeft}" />
```

Doing so will provide you with the following similar console output (differs slightly for WPF):

    Element = Windows.UI.Xaml.Controls.Button
    Kind = FadeFrom, TranslateFrom
    TargetProperty = Translation
    Duration = 500
    Delay = 0
    Opacity = 0
    OffsetX = 0
    OffsetY = 50
    OffsetZ = 0
    ScaleX = 1
    ScaleY = 1
    ScaleZ = 1
    Rotation = 0
    Blur = 0
    RenderTransformOrigin = 0.5,0.5
    Easing = Cubic
    EasingMode = EaseOut

As each storyboard executes, it's kept in an internal list until it completes (or gets stopped). To output this internal list, temporarily add the following in your app startup code:

```c#
Animations.EnableActiveTimelinesLogging = true;
```

Doing so will provide you with the following similar console output:

    ---------- ALL ACTIVE TIMELINES --------
    Active timeline removed at 12:42:26:43222

    Element = Button,  Key = d69f826a-1978-4a4e-b516-4a6b0469238b,  ElementGuid = 195d8c13-1dd7-4fef-a7f3-fc78bdab1cd7
        State = Running,  IsSequence = False,  IsIterating = False,  IterationBehavior = Count,  IterationCount = 0
    ------------------------------------

    ---------- ACTIVE TIMELINE --------
    Guid d69f826a-1978-4a4e-b516-4a6b0469238b - Updated state to: Completed at 12:42:26:88616
    ------------------------------------

    ---------- ALL ACTIVE TIMELINES --------
    Active timeline removed at 12:42:26:89614

    NO ACTIVE TIMELINES!
    ------------------------------------

### `ListViewBase` (UWP) and `ListBox`-based (WPF) animations (WORK-IN-PROGRESS)

In order to properly implement item animations on list items, it was not enough to simply create attached properties against the ListViewBase (UWP) and ListBox (WPF) controls. Instead, inherited controls were created: `AnimatedListView` and `AnimatedGridView` for UWP, and `AnimatedListView` and `AnimatedListBox` for WPF, all available from the `XamlFlair.Controls` namespace:


**UWP namespace:**
```xml
xmlns:xfc="using:XamlFlair.Controls"
```

**WPF namespace:**
```xml
xmlns:xfc="clr-namespace:XamlFlair.Controls;assembly=XamlFlair.WPF"
```

Animating items in lists is slightly different than animating a typical UI element. The main reason for this difference is that the `Event` value on the corresponding `AnimationSettings` cannot be changed from its default value. Therefore the following is *invalid*:

```xml
<xfc:AnimatedListView ItemsSource="Binding SampleDataList"
                      xf:Animations:Items="{xf:Animate BasedOn={StaticResource FadeIn}, Event=Visibility}" />
```

List item animations, _by default_, animate based on the `Loaded` event of each *visible* item, and also based on an update to the `ItemsSource` property of the list control. In order to disable these default behaviors, the following two properties can be used independently:

```xml
<xfc:AnimatedListView ItemsSource="Binding SampleDataList"
                      xf:Animations.AnimateOnLoad="False"
                      ... />

<xfc:AnimatedListView ItemsSource="Binding SampleDataList"
                      xf:Animations.AnimateOnItemsSourceChange="False"
                      ... />
```

By default, item animations have a delay of **25 milliseconds** between each item. This value can be changed using the `InterElementDelay` property:

```xml
<xfc:AnimatedListView ItemsSource="Binding SampleDataList"
                      xf:Animations.InterElementDelay="50"
                      ... />
```

Just like `PrimaryBinding` and `SecondaryBinding`, item animations can be triggered by a binding with the use of the `ItemsBinding` property:

```xml
<xfc:AnimatedListView ItemsSource="Binding SampleDataList"
                      xf:Animations.AnimateOnLoad="False"
                      xf:Animations.ItemsBinding="{Binding MyViewModelProperty}"
                      xf:Animations:Items="{xf:Animate BasedOn={StaticResource FadeIn}, Event=Visibility}" />
```

> **Note (UWP ONLY)**: To avoid any flickers on item animations, there is currently a constraint in place: An `Items` animation **must** contain a `FadeFrom`.
