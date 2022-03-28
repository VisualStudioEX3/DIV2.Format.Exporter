# FPG class samples
This section shows common samples of how to create and manage **DIV Games Studio** graphic package files.

**DIV Games Studio** [FPG](xref:DIV2.Format.Exporter.FPG) file is a simple file package format designed to store 
multiple [MAP](xref:DIV2.Format.Exporter.MAP) images sharing a common [PAL](xref:DIV2.Format.Exporter.PAL) color 
palette, is nearly an atlas like we used on modern game engines to group various sprites in a single file. In
**DIV Games Studio** this feature ease the manage of animations and group of related sprites when need to manage 
in code.

>[!NOTE]
> The [FPG](xref:DIV2.Format.Exporter.FPG) files required that each [MAP](xref:DIV2.Format.Exporter.MAP) image has
> an unique **graph id** value, from 1 to 999. 
>
> Not is necessary that all **graph id**s are sequentially. You can create different series of ids or set jumps 
> between ids (e.g.: 1, 2, 3, 16, 24, 32, 48, 64, 96, 100, 101, 102, 200, 300, 301, 302, 600...).
> 
> By default, **DIV Games Studio** not sorted the [MAP](xref:DIV2.Format.Exporter.MAP) images by his **graph id**
> value (useful to quickly locate them in the built-in visor) but **DIV2.Format.Exporter** apply a sort when you
> save the [FPG](xref:DIV2.Format.Exporter.FPG) file to disk.

