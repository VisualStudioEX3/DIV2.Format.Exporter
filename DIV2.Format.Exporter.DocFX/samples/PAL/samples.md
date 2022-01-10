# PAL class samples
This section shows common samples of how to create and manage DIV Games Studio palettes.

> [!NOTE]
> Usually is not necessary to edit the [Color Range Tables](xref:DIV2.Format.Exporter.ColorRangeTable) in a palette to make it usable in games. 
> [Color Range Tables](xref:DIV2.Format.Exporter.ColorRangeTable) are used by the DIV Games Studio built-in Drawing Editor. 
> The default [Color Range Tables](xref:DIV2.Format.Exporter.ColorRangeTable) initialization performed by this framework is enough to use in this program.
> We are skipped all operations related with [Color Range Tables](xref:DIV2.Format.Exporter.ColorRangeTable) in palettes in this section.

## Create an empty palette
This code shows how to create an empty [PAL](xref:DIV2.Format.Exporter.PAL) 
instance and setup colors manually:
[!code-csharp[Main](samples.cs?range=4-9)]

Also you can setup a [Color](xref:DIV2.Format.Exporter.Color) array to 
initialize a new palette:
[!code-csharp[Main](samples.cs?range=12-14)]

## Load an existing palette
This code shows how to load a [PAL](xref:DIV2.Format.Exporter.PAL) file:
[!code-csharp[Main](samples.cs?range=17)]

Also you can load a [PAL](xref:DIV2.Format.Exporter.PAL) file from a byte array:
[!code-csharp[Main](samples.cs?range=20-21)]

## Create a palette from an image
You can create palettes from a image file. The process allows you to load an image file (JPEG, PNG, BMP, GIF, and TGA image formats, and also supported 
256 colors PCX files), extract all unique colors from the image, and convert them to 8 bit DAC format.
[!code-csharp[Main](samples.cs?range=24)]

Also you can create the palette from a byte array with the content of the image file:
[!code-csharp[Main](samples.cs?range=27-28)]

You can perform a sorting color action when creates the palette from a image:
[!code-csharp[Main](samples.cs?range=31)]
[!code-csharp[Main](samples.cs?range=33-34)]
> [!NOTE]
> Not is a requirement to sort the colors in a palette but is recomended to ensure that the black color, if is present in the palette, be the first color 
> (index zero). DIV Games Studio draw operations usually uses the first color in a palette, the black, as transparent color for sprite masks.

## Extract palettes from MAP and FPG files
You can create the palette from the existing one from a [MAP](xref:DIV2.Format.Exporter.MAP) or [FPG](xref:DIV2.Format.Exporter.FPG) file like you load 
a supported image file:
[!code-csharp[Main](samples.cs?range=37-38)]

## Read colors
You can easily read each color of the palette using a direct accessor, like an array:
[!code-csharp[Main](samples.cs?range=41)]

And also, you can use a foreach loop to read all colors:
[!code-csharp[Main](samples.cs?range=44-47)]

## Write colors
You can easily write each color of the palette using a direct accessor, like an array:
[!code-csharp[Main](samples.cs?range=50)]
> [!WARNING]
> Remember that DIV Games Studio palettes working in DAC format. This means that the RGB channel ranges are 0 to 63 instead of 0 to 255. If you trying to 
> set a value over 63 for any color channel, you thrown an exception.

## Sort colors
You can manually perform a color sorting in your palettes. This action trying to sort the colors, from the black, or the darkest color in palette, to the 
white or the brightest color in the palette:
[!code-csharp[Main](samples.cs?range=53)]
> [!NOTE]
> Not is a requirement to sort the colors in a palette but is recomended to ensure that the black color, if is present in the palette, be the first color 
> (index zero). DIV Games Studio draw operations usually uses the first color in a palette, the black, as transparent color for sprite masks.

## Save palette to a file
You can easily save your palette to a [PAL](xref:DIV2.Format.Exporter.PAL) file using the following call:
[!code-csharp[Main](samples.cs?range=56)]

## Get full RGB colors
If you need, for example, render the 16x16 palette color matrix in a custom tool, that works with full RGB 24/32 bits, you can get a copy of the all 
colors in RGB format [0..255] using a simple call:
[!code-csharp[Main](samples.cs?range=59)]
