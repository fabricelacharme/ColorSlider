# ColorSlider
A slider control in C#

![GitHub Logo](/gifs/colorslider.jpg)

ColorSlider is a trackbar control written in C# as a replacement of the standard trackbar proposed by Microsoft in Visual Studio
It is a free interpretation of the original code from Michal Brylka published in the site Code Project.
see https://www.codeproject.com/Articles/17395/Owner-drawn-trackbar-slider

The slider shape can be either paint or replaced by an image.  

Events:
ValueChanged
Scroll

Properties
ThumbSize                 The size of the thumb
ThumbCustomShape          Gets or sets the thumb custom shape. Use ThumbRect property to determine bounding rectangle.
ThumbRoundRectSize        Gets or sets the size of the thumb round rectangle edges.
BorderRoundRectSize       Gets or sets the size of the border round rect.

ThumbImage                 Gets or sets a specific image used to render the thumb.

Orientation               Gets or sets the orientation of the Slider(Horizontal or vertical)
DrawFocusRectangle        Gets or sets a value indicating whether to draw focus rectangle.
DrawSemitransparentThumb  Gets or sets a value indicating whether to draw semitransparent thumb.
MouseEffects              Gets or sets whether mouse entry and exit actions have impact on how control look.

Value                     Gets or sets the value of Slider
Minimum (0)               Gets or sets the minimum value.
Maximum (100)             Gets or sets the maximum value.
SmallChange               Gets or sets trackbar's small change. It affects how to behave when directional keys are pressed.
LargeChange               Gets or sets trackbar's large change. It affects how to behave when PageUp/PageDown keys are pressed.
MouseWheelBarPartitions   Gets or sets the mouse wheel bar partitions.

ThumbOuterColor           Gets or sets the thumb outer color.
ThumbInnerColor           Gets or sets the inner color of the thumb.
ThumbPenColor             Gets or sets the color of the thumb pen.

BarInnerColor             Gets or sets the inner color of the bar.
BarPenColorTop            Gets or sets the color of the top of the bar on the right of the thumb
BarPenColorBottom         Gets or sets the color of the bottom of the bar on the right of the thumb

BarPenColorElapsedTop     Gets or sets the color of the top of the bar on the left of the thumb
BarPenColorElapsedBottom  Gets or sets the color of the bottom of the bar on the left of the thumb
ElapsedInnerColor         Gets or sets the inner color of the bar on the left of the thumb.

TickColor                 Gets or sets the color of the graduations
TickStyle                 Gets or sets where to display the ticks (None, both top-left, bottom-right)
ScaleDivisions            Gets or sets the number of intervals between minimum and maximum
ScaleSubDivisions         Gets or sets the number of subdivisions between main divisions of graduation
ShowSmallScale            Shows or hides subdivisions of graduations.
ShowDivisionsText         Show or hide text value of main graduations.




# Author
Fabrice Lacharme
