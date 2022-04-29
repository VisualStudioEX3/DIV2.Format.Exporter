# FPG class samples
This section shows common samples of how to create and manage **DIV Games Studio** graphic package files.

**DIV Games Studio** [FPG](xref:DIV2.Format.Exporter.FPG) file is a simple file package format designed to store 
multiple [MAP](xref:DIV2.Format.Exporter.MAP) images sharing a common [PAL](xref:DIV2.Format.Exporter.PAL) color 
palette, is nearly an atlas like we used on modern game engines to group various sprites in a single file. In
**DIV Games Studio** this feature ease the manage of animations and group of related sprites when need to manage 
in code.

>[!NOTE]
> The [FPG](xref:DIV2.Format.Exporter.FPG) files required that each [MAP](xref:DIV2.Format.Exporter.MAP) image has
> an unique **graph id** value, from 1 to 999 (the [FPG](xref:DIV2.Format.Exporter.FPG) files are limited to 1000 
> [MAP](xref:DIV2.Format.Exporter.MAP) images).
>
> Not is necessary that all **graph id**s are sequentially. You can create different series of ids or set jumps 
> between ids (e.g.: 1, 2, 3, 16, 24, 32, 48, 64, 96, 100, 101, 102, 200, 300, 301, 302, 600...).
> 
> By default, **DIV Games Studio** not sorted the [MAP](xref:DIV2.Format.Exporter.MAP) images by his **graph id**
> value (that is useful to easy locates them in the built-in visor) but **DIV2.Format.Exporter** does sort them when 
> you save the [FPG](xref:DIV2.Format.Exporter.FPG) file to disk.

## Create a new FPG
This code shows how to create an empty [FPG](xref:DIV2.Format.Exporter.FPG) instance initialized with a specific
[PAL](xref:DIV2.Format.Exporter.PAL) instance:
[!code-csharp[Main](samples.cs?range=4-5)]

## Load a FPG file
This code shows how to load an existing [FPG](xref:DIV2.Format.Exporter.FPG) file:
[!code-csharp[Main](samples.cs?range=8)]

Also you can load a [FPG](xref:DIV2.Format.Exporter.FPG) file from a byte array:
[!code-csharp[Main](samples.cs?range=11-12)]

## Read a MAP image
This code shows how to read an existing [MAP](xref:DIV2.Format.Exporter.MAP) from a [FPG](xref:DIV2.Format.Exporter.FPG) 
instance using his index position:
[!code-csharp[Main](samples.cs?range=15)]

And also, you can use a foreach loop to read all [MAP](xref:DIV2.Format.Exporter.MAP)s:
[!code-csharp[Main](samples.cs?range=18-22)]

## Add a new MAP image
This code shows how to add a new [MAP](xref:DIV2.Format.Exporter.MAP) to the [FPG](xref:DIV2.Format.Exporter.FPG) 
instance:
[!code-csharp[Main](samples.cs?range=25-26)]

Also you can load a [MAP](xref:DIV2.Format.Exporter.MAP) file from a byte array and add them in the same way:
[!code-csharp[Main](samples.cs?range=29-30)]

Or simply load by filename:
[!code-csharp[Main](samples.cs?range=33)]

The [Add](xref:DIV2.Format.Exporter.FPG.Add(DIV2.Format.Exporter.MAP,System.String)) method, and his overloads, checks 
the graphId before add the [MAP](xref:DIV2.Format.Exporter.MAP) and thrown an exception if already exists a
[MAP](xref:DIV2.Format.Exporter.MAP) with the same graphId.

>[!NOTE]
> The [Add](xref:DIV2.Format.Exporter.FPG.Add(DIV2.Format.Exporter.MAP,System.String)) method, and his overloads, 
> always performs a color conversion if the palette is different from the [FPG](xref:DIV2.Format.Exporter.FPG) instance
> to ensure the [MAP](xref:DIV2.Format.Exporter.MAP) image shows properly with the current palette.

## Remove a MAP image
This code shows how to remove a [MAP](xref:DIV2.Format.Exporter.MAP) from a [FPG](xref:DIV2.Format.Exporter.FPG) 
instance using different ways:
[!code-csharp[Main](samples.cs?range=36-37)]
[!code-csharp[Main](samples.cs?range=40)]
[!code-csharp[Main](samples.cs?range=43)]

## Check if a FPG contains an specific MAP image
This code shows how to check if a [FPG](xref:DIV2.Format.Exporter.FPG) instance contains an specific 
[MAP](xref:DIV2.Format.Exporter.MAP):
[!code-csharp[Main](samples.cs?range=46-47)]
[!code-csharp[Main](samples.cs?range=50)]

## Replace an existing MAP image
This code shows how to replace [MAP](xref:DIV2.Format.Exporter.MAP) from a [FPG](xref:DIV2.Format.Exporter.FPG) 
instance:
[!code-csharp[Main](samples.cs?range=53-64)]

## Removes all MAP images
You can removes all [MAP](xref:DIV2.Format.Exporter.MAP)s from a [FPG](xref:DIV2.Format.Exporter.FPG) instance using 
the following call:
[!code-csharp[Main](samples.cs?range=67)]

## Save FPG to file
You can easily save your changes to a [MAP](xref:DIV2.Format.Exporter.MAP) file using the following call:
[!code-csharp[Main](samples.cs?range=70)]
