# ColorSlider
A slider/trackbar control in C#

![GitHub Logo](/gifs/colorslider.jpg)

ColorSlider is a slider/trackbar control written in C# (Windows Form )
This is an alternative/replacement to the standard Microsoft Visual Studio trackbar control which is not so flexible and lack basic functionalities.

The code is a free interpretation of the original code from Michal Brylka published in the site Code Project.

See https://www.codeproject.com/Articles/17395/Owner-drawn-trackbar-slider

The main enhancements brought by this control are
* a less surface
* the possibility to parametrize the shape of the thumb or to replace it by an image.
* subdivisions added between main divisions.
* the text value of the main divisions.
* many colors parametrization added (ticks, bar, elapsed) 


# Events:
* ValueChanged
* Scroll

Typical usage of ValueChanged event:

     private void colorSlider1_ValueChanged(object sender, EventArgs e)
     {
         label1.Text = colorSlider1.Value.ToString();
     }

# Properties

Thumb | signification
------------ | -------------
**Thumb**                 | ?
ThumbSize                 | The size of the thumb (Width, Height). allowing you to make circles or rectangles
ThumbCustomShape          | Gets or sets the thumb custom shape. Use ThumbRect property to determine bounding rectangle.
ThumbRoundRectSize        | Gets or sets the size of the thumb round rectangle edges.
BorderRoundRectSize       | Gets or sets the size of the border round rect.
DrawSemitransparentThumb  | Gets or sets a value indicating whether to draw semitransparent thumb.
ThumbImage                | Gets or sets a specific image used to render the thumb.
**Appearance**            | ?
Orientation               | Gets or sets the orientation of the Slider(Horizontal or vertical)
DrawFocusRectangle        | Gets or sets a value indicating whether to draw focus rectangle.
MouseEffects              | Gets or sets whether mouse entry and exit actions have impact on how control look.
**Values**                | ?
Value                     | Gets or sets the value of Slider
Minimum (0)               | Gets or sets the minimum value.
Maximum (100)             | Gets or sets the maximum value.
SmallChange               | Gets or sets trackbar's small change. It affects how to behave when directional keys are pressed.
LargeChange               | Gets or sets trackbar's large change. It affects how to behave when PageUp/PageDown keys are pressed.
MouseWheelBarPartitions   | Gets or sets the mouse wheel bar partitions.


Appearance | signification
------------ | -------------
Orientation               | Gets or sets the orientation of the Slider(Horizontal or vertical)
DrawFocusRectangle        | Gets or sets a value indicating whether to draw focus rectangle.
MouseEffects              | Gets or sets whether mouse entry and exit actions have impact on how control look.


Values | signification
------------ | -------------
Value                     | Gets or sets the value of Slider
Minimum (0)               | Gets or sets the minimum value.
Maximum (100)             | Gets or sets the maximum value.
SmallChange               | Gets or sets trackbar's small change. It affects how to behave when directional keys are pressed.
LargeChange               | Gets or sets trackbar's large change. It affects how to behave when PageUp/PageDown keys are pressed.
MouseWheelBarPartitions   | Gets or sets the mouse wheel bar partitions.


Colors | signification
------------ | -------------
ThumbOuterColor           | Gets or sets the thumb outer color.
ThumbInnerColor           | Gets or sets the inner color of the thumb.
ThumbPenColor             | Gets or sets the color of the thumb pen.
BarInnerColor             | Gets or sets the inner color of the bar.
BarPenColorTop            | Gets or sets the top color of the bar
BarPenColorBottom         | Gets or sets the bottom color of the bar
ElapsedPenColorTop        | Gets or sets the top color of the elapsed
ElapsedPenColorBottom     | Gets or sets the bottom color of the elapsed
ElapsedInnerColor         | Gets or sets the inner color of the elapsed.
TickColor                 | Gets or sets the color of the graduations


Preformated colors | signification
--- | ---
ColorSchema               | 3 predefined color schemas are proposed (red, green, blue). The colors can be changed manually and they overwrite them.


Ticks | signification
------------ | -------------
TickStyle                 | Gets or sets where to display the ticks (None, both top-left, bottom-right)
ScaleDivisions            | Gets or sets the number of intervals between minimum and maximum
ScaleSubDivisions         | Gets or sets the number of subdivisions between main divisions of graduation
ShowSmallScale            | Shows or hides subdivisions of graduations.
ShowDivisionsText         | Show or hide text value of main graduations.




# Author
Fabrice Lacharme
