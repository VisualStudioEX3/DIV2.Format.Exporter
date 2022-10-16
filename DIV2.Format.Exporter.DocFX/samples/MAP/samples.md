# MAP class samples
This section shows common samples of how to create and manage **DIV Games Studio** graphic maps.

A [MAP](xref:DIV2.Format.Exporter.MAP) object is the image format used by **DIV Games Studio** to represents a 
bitmap or texture in his engine. Is basically a simple uncompressed bitmap with a palette (a 
[PAL](xref:DIV2.Format.Exporter.PAL) data). One interesting feature of the [MAP](xref:DIV2.Format.Exporter.MAP) 
format is the [Control Point](xref:DIV2.Format.Exporter.ControlPoint) list.

A [Control Point](xref:DIV2.Format.Exporter.ControlPoint) is a coordinate, in [MAP](xref:DIV2.Format.Exporter.MAP)
bitmap space, that represents a "hot spot", like how works in the Windows Cursor images. This point coordinates are
relatives to the image bitmap but are translated to the screen space when you draw the 
[MAP](xref:DIV2.Format.Exporter.MAP) image.

This feature helps the programmer to define the logical center of the image, used to for draw, rotate and scale 
operations, but also, and is the main useful usage, to easily place other objects related with the 
[MAP](xref:DIV2.Format.Exporter.MAP) position in screen (e.g. a ship engine particle or a bullet from a weapon barrel).

By default, all [MAP](xref:DIV2.Format.Exporter.MAP) images has a default 
[Control Point](xref:DIV2.Format.Exporter.ControlPoint) that reference the center of the image. You can overwrite
the default [Control Point](xref:DIV2.Format.Exporter.ControlPoint) coordinates and create new ones as many you need, 
with a limit of 1000 points in total.

> [!NOTE]
> Only the first [Control Point](xref:DIV2.Format.Exporter.ControlPoint) is used as center of the image for draw, 
> rotate and scale operations.

The [MAP](xref:DIV2.Format.Exporter.MAP) format also contains other data like a **graph id** and a 
**description** fields. The **graph id** is used as index to reference a [MAP](xref:DIV2.Format.Exporter.MAP) 
image. The **description** is an optional field used to add a little description of the image.

> [!NOTE]
> The **graph id** value is not required for standalone [MAP](xref:DIV2.Format.Exporter.MAP) images (in this case, 
> **DIV Games Studio** assigned one when you load a [MAP](xref:DIV2.Format.Exporter.MAP) from a file).
> This field value only is required when you want to store the image in a [FPG](xref:DIV2.Format.Exporter.FPG) file.

## Create a new MAP graphic
This code shows how to create an empty 64x128 [MAP](xref:DIV2.Format.Exporter.MAP) instance, with a **graph id** 
and a **description** values. You can omit these fields to use default values:
[!code-csharp[Main](samples.cs?range=4-5)]

## Load a MAP graphic
This code shows how to load an existing [MAP](xref:DIV2.Format.Exporter.MAP) file:
[!code-csharp[Main](samples.cs?range=8)]

Also you can load a [MAP](xref:DIV2.Format.Exporter.MAP) file from a [byte](xref:System.Byte) array:
[!code-csharp[Main](samples.cs?range=11-12)]

## Import a supported image format
This code shows how to import a supported image file (JPEG, PNG, BMP, GIF, TGA and PCX) as 
[MAP](xref:DIV2.Format.Exporter.MAP) image. The process converts all original colors to a 8 bits (256 colors) format:
[!code-csharp[Main](samples.cs?range=15)]

> [!WARNING]
> PCX import feature only supports 8 bits (256 colors) PCX images. If you try to import a PCX image with other BPP 
> format, you thrown an exception.

## Converts all colors to a specific palette
When you imports a supported image file, you can also specify a [PAL](xref:DIV2.Format.Exporter.PAL) instance to
convert the original colors to a specific colors:
[!code-csharp[Main](samples.cs?range=18-19)]

> [!NOTE]
> You can imports [MAP](xref:DIV2.Format.Exporter.MAP) files to apply color conversion.

## Write bitmap colors
You can write a palette color index from the bitmap using the pixel index:
[!code-csharp[Main](samples.cs?range=22)]

Also you can use the pixel coordinates to write it:
[!code-csharp[Main](samples.cs?range=25)]

If you need to write all bitmap data in one step you can use this way:
[!code-csharp[Main](samples.cs?range=28-31)]

> [!NOTE]
> Remember that the bitmap pixels store the color index in the assigned palette, a value from 0 to 255, instead 
> the RGB-DAC values.

## Read bitmap colors
You can read a palette color index from the bitmap using the pixel index:
[!code-csharp[Main](samples.cs?range=34)]

Also you can use the pixel coordinates to read it:
[!code-csharp[Main](samples.cs?range=37)]

Also you can read each pixel using a foreach loop:
[!code-csharp[Main](samples.cs?range=40-43)]

If you need to read all bitmap data you can use this way:
[!code-csharp[Main](samples.cs?range=46)]

## Get the RGB texture
If you need, for example, render the bitmap in a custom tool, that works with full RGB 24/32 bits, you can get a 
copy of the all colors in RGB format [0..255] using a simple call:
[!code-csharp[Main](samples.cs?range=49)]

## Clear all bitmap data
You can erase all bitmap data using this call. This method sets all pixels to zero, the first color of the palette
(usually expected black color for transparency):
[!code-csharp[Main](samples.cs?range=52)]

## Manage Control Points
The management of the [Control Point](xref:DIV2.Format.Exporter.ControlPoint)s list is the same as the 
[List\<T\>](xref:System.Collections.Generic.List`1) generic class.

You can read any point using his index in the list:
[!code-csharp[Main](samples.cs?range=55)]

Also you can read each one using a foreach loop:
[!code-csharp[Main](samples.cs?range=58-61)]

Use the **Add** method to add new point to the list:
[!code-csharp[Main](samples.cs?range=64)]

Use the **RemoveAt** method to remove a point using his index position:
[!code-csharp[Main](samples.cs?range=67)]

And use the **Clear** method to remove all points:
[!code-csharp[Main](samples.cs?range=70)]

> [!NOTE]
> Don't worry if you keep empty the [Control Point](xref:DIV2.Format.Exporter.ControlPoint)s list. By default,
> if not exists any point, **DIV Games Studio** created a default one with the center coordinates of the bitmap.

## Get or set the GraphID value
You can get or set the **graph id** value using the following property:
[!code-csharp[Main](samples.cs?range=73)]
[!code-csharp[Main](samples.cs?range=76)]

> [!NOTE]
> This value is only required when you want to store the [MAP](xref:DIV2.Format.Exporter.MAP) in a 
> [FPG](xref:DIV2.Format.Exporter.FPG) file. If you want to load the image from a file instead, this value is
> omitted and **DIV Games Studio** assigned one automatically. By default, this value is 1.

> [!WARNING]
> If you try to sets a value under 1 or over 999, you thrown an exception.

## Get or set the Description value
You can get or set the **description** value using the following property:
[!code-csharp[Main](samples.cs?range=79)]
[!code-csharp[Main](samples.cs?range=82)]

> [!NOTE]
> The **description** field has a limit of 32 characters. You can enter any [string](xref:System.String) value with more of 32 characters, 
> but when you save the [MAP](xref:DIV2.Format.Exporter.MAP) file, this field save only the first 32 characters.
> This field is optional.

## Save MAP graphic to a file
You can easily save your changes to a [MAP](xref:DIV2.Format.Exporter.MAP) file using the following call:
[!code-csharp[Main](samples.cs?range=85)]
