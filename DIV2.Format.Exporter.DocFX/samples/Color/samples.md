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

