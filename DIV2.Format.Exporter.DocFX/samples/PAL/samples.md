# PAL samples
This section shows common samples of how to create and manage DIV Games Studio palettes.

> [!NOTE]
> Usually is not necessary to edit the Color Range tables in a palette to make usable the palette in games. We are skipped all operations related with them in this section.

## Create an empty palette
This code shows how to create an empty PAL instance and setup colors manually:
[!code-csharp[Main](samples.cs?range=4-9)]

Also, you can setup a Color array to initialize a new palette:
[!code-csharp[Main](samples.cs?range=12-14)]

## Load an existing palette
This code shows how to load a PAL file:
[!code-csharp[Main](samples.cs?range=17)]

Also you can load a PAL file from a byte array:
[!code-csharp[Main](samples.cs?range=20-21)]

## Create a palette from an image
You can create palettes from a image file. The process allows you to load an image file (JPEG, PNG, BMP, GIF, TGA and 8 bit PCX formats) and convert his colors to 8 bit DAC format.
[!code-csharp[Main](samples.cs?range=24)]

Also you can create the palette from a byte array with the content of the image file:
[!code-csharp[Main](samples.cs?range=27-28)]

You can perform a sorting color action when creates the palette from a image:
[!code-csharp[Main](samples.cs?range=31)]
[!code-csharp[Main](samples.cs?range=33-34)]
> [!NOTE]
> Not is a requirement to sort the colors in a palette but is recomended to ensure that the black color, if is present in the palette, be the first color in palete (index zero). DIV Games Studio draw operations usually uses the first color in a palette, the black, as transparent color for sprite masks.

## Extract palettes from MAP and FPG files
You can create the palette from the existing one from a MAP or FPG file:
[!code-csharp[Main](samples.cs?range=37-38)]

## Read colors
You can easily read each color of the palette using a direct accessor, like an array:
[!code-csharp[Main](samples.cs?range=41)]

And also, you can use a foreach loop to read all colors:
[!code-csharp[Main](samples.cs?range=44-47)]

## Write colors
You can easily write each color of the palette using a direct accessor, like an array:
[!code-csharp[Main](samples.cs?range=50)]

## Sort colors
You can manually perform a color sorting in your palettes. This action trying to sort the colors, from the black, or the darkest color in palette, to the white or the brightest color in the palette:
[!code-csharp[Main](samples.cs?range=53)]
> [!NOTE]
> Not is a requirement to sort the colors in a palette but is recomended to ensure that the black color, if is present in the palette, be the first color in palete (index zero). DIV Games Studio draw operations usually uses the first color in a palette, the black, as transparent color for sprite masks.

## Save palette to a file
You can easily save your palette to a PAL file using the following call:
[!code-csharp[Main](samples.cs?range=56)]
