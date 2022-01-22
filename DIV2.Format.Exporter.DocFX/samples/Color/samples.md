# Color struct samples
This section shows common samples of how to work with colors to manage **DIV Games Studio** palettes and images.

Remember that **DIV Games Studio** palettes working in **DAC** format. This means that the **RGB** channel ranges are 0 to 63 instead of 0 to 255.

The **DIV Games Studio** files, [PAL](xref:DIV2.Format.Exporter.PAL), [MAP](xref:DIV2.Format.Exporter.MAP), [FPG](xref:DIV2.Format.Exporter.FPG) 
and **FNT** files, works in **DAC** format. If you try to set a full **RGB** [Color](xref:DIV2.Format.Exporter.Color) value for any related 
**DIV Games Studio** file operations, or even try to load a modified file with full **RGB** [Color](xref:DIV2.Format.Exporter.Color) values, you 
will gets an exception.

You can use full **RGB** [Color](xref:DIV2.Format.Exporter.Color) values when you need to export a [PAL](xref:DIV2.Format.Exporter.PAL) colors
or the bitmap data from a [MAP](xref:DIV2.Format.Exporter.MAP) to use in modern systems (e.g. to render a 16x16 [PAL](xref:DIV2.Format.Exporter.PAL) 
color matrix or the bitmap image from a [MAP](xref:DIV2.Format.Exporter.MAP) in a custom tool using the common RGB 24/32 bits system GUI, web 
based GUI, or a custom GUI in major environments like [Unity](https://unity.com/) editor custom tool).

## Create a color
The common way to create a new color value is using the main constructor:
[!code-csharp[Main](samples.cs?range=4)]
Also you can create an empty [Color](xref:DIV2.Format.Exporter.Color) value or pure black color (0, 0, 0) using the default non-parametrized 
consutructor and setup the color channel values later.

## Access color channel values
You can access, to write or read, the each [Color](xref:DIV2.Format.Exporter.Color) channel values: red, green and blue, using the struct fields:
[!code-csharp[Main](samples.cs?range=7-12)]

And also, like a vector structure, you can using a direct accessor by index position:
[!code-csharp[Main](samples.cs?range=15-20)]

## Check if the color is a valid DAC value
You can check if a [Color](xref:DIV2.Format.Exporter.Color) value is a **DAC** value using the following function:
[!code-csharp[Main](samples.cs?range=23)]

> [!NOTE]
> This function only checks if the color channels values are bounded in the **DAC** range [0..63].
> There is not way to ensure if a color value is a true **DAC** or full **RGB** value [0..255].

## Convert a DAC color to RGB equivalent
You can convert any **DAC** color to his equivalent **RGB** value using the following function. Remember that the value is a closer approximation
of the real **RGB** value:
[!code-csharp[Main](samples.cs?range=26)]

## Convert a RGB color to DAC equivalent
You can convert any **RGB** color to his equivalent **DAC** value using the following function. Remember that the value is a closer approximation
of the real **DAC** value:
[!code-csharp[Main](samples.cs?range=29)]

## Using an integer value as a Color struct value and viceversa
The [Color](xref:DIV2.Format.Exporter.Color) structure can be casted to integer values and viceversa.
This is useful in some scenaries where you need to implements algorithms that are more effective working with simple numbers than a structures, for
example, for sorting processes or similar actions.

You can cast an integer value as [Color](xref:DIV2.Format.Exporter.Color) simply assigning the value using the assignation operator:
[!code-csharp[Main](samples.cs?range=32-34)]

And viceversa, you can cast a [Color](xref:DIV2.Format.Exporter.Color) value to an integer value in the similar way:
[!code-csharp[Main](samples.cs?range=37-39)]
