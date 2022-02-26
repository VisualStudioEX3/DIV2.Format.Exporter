# MAP class samples
This section shows common samples of how to create and manage DIV Games Studio graphic maps.

A [MAP](xref:DIV2.Format.Exporter.MAP) object is the image format used by DIV Games Studio to represents a 
bitmap or texture in his engine. Is basically a simple uncompressed bitmap with a palette (a 
[PAL](xref:DIV2.Format.Exporter.PAL) data). One interesting feature of the [MAP](xref:DIV2.Format.Exporter.MAP) 
format is the [Control Point](xref:DIV2.Format.Exporter.ControlPoint) list.

A [Control Point](xref:DIV2.Format.Exporter.ControlPoint) is a coordinate, in [MAP](xref:DIV2.Format.Exporter.MAP)
bitmap space, that represents a "hot spot", like how works in the Windows Cursor images. This point coordinates are
relatives to the image bitmap but are translated to the screen space when you draw the 
[MAP](xref:DIV2.Format.Exporter.MAP) image.

This feature helps the programmer to define the logical center of the image, used to for draw, rotate and scale 
operations, but also, and is the main useful usage, to easily place other objects related with the 
[MAP](xref:DIV2.Format.Exporter.MAP) in screen (e.g. a ship engine particle or a bullet from a weapon barrel).

By default, all [MAP](xref:DIV2.Format.Exporter.MAP) images contains a default 
[Control Point](xref:DIV2.Format.Exporter.ControlPoint) that reference the center of the image. You can overwrite
the default point coordinates and create new ones as many you need, with a limit of 1000 points in total per 
[MAP](xref:DIV2.Format.Exporter.MAP)).

> [!NOTE]
> Only the first [Control Point](xref:DIV2.Format.Exporter.ControlPoint) is used as center of the image for draw, 
> rotate and scale operations.

The [MAP](xref:DIV2.Format.Exporter.MAP) format also contains other data like a **graph id** and a 
**description** fields. The **graph id** is used as index to reference a [MAP](xref:DIV2.Format.Exporter.MAP) 
image. The **description** is an optional field used to add a little description of the image.

> [!NOTE]
> The **graph id** value is not required for standalone [MAP](xref:DIV2.Format.Exporter.MAP) images (in this case, 
> DIV Games Studio assigned one when you load a [MAP](xref:DIV2.Format.Exporter.MAP) from a file).
> This field value only is required when you want to store the image in a [FPG](xref:DIV2.Format.Exporter.FPG) file.
