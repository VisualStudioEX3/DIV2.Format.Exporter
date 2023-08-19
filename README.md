<h1 align="center">
<img src="https://github.com/VisualStudioEX3/VisualStudioEX3/blob/master/Shared/Images/div_games_studio/div2_logo/div2_logo.png" alt="DIV Games Studio 2 logo" width="512" />
<br>
DIV2.Format.Exporter</h1>

<h6 align="center">© Visual Studio EX3, José Miguel Sánchez Fernández - 2020 - 2022</h6>

## Multiplatform framework to converts modern graphic format images to native DIV Games Studio graphic formats: PAL, MAP and FPG files.

[![Build](https://github.com/VisualStudioEX3/DIV2.Format.Exporter/workflows/Build/badge.svg)](https://github.com/VisualStudioEX3/DIV2.Format.Exporter/actions)
[![GitHub](https://img.shields.io/github/license/VisualStudioEX3/DIV2.Format.Exporter?color=yellow)](https://opensource.org/licenses/MIT)
[![GitHub release (latest by date)](https://img.shields.io/github/v/release/VisualStudioEX3/DIV2.Format.Exporter?color=green)](https://github.com/VisualStudioEX3/DIV2.Format.Exporter/releases/)
[![Nuget](https://img.shields.io/nuget/v/DIV2.Format.Exporter?logo=nuget&label=NuGet)](https://www.nuget.org/packages/DIV2.Format.Exporter/)

# Introduction
**DIV2.Format.Exporter** is writen in **C#** using **.NET Standard 2.1** and using [SixLabors ImageSharp](https://github.com/SixLabors/ImageSharp) library for image and palette conversions. This library is designed initially to works in [Unity](https://unity.com/) editor as content creation tool for [StarFighter](https://github.com/VisualStudioEX3/StarFighter) project, but is possible to using from other implementations (e.g. a CLI program or other engines or frameworks compatible with .NET).

> **Warning**
> This tool is not designed to work with DIV Games Studio forks like [Fenix Project](https://web.archive.org/web/20071012230137/http://fenix.divsite.net/), [eDivc](https://github.com/vroman/edivc), CDiv, [Div GO](https://www.divgo.net/), [Gemix Studio](http://www.gemixstudio.com/), [Bennu GD](https://www.bennugd.org/), or [PixTudio](https://pixtudio.org/).

# Documentation
You have available online API documentation for reference and small code samples to learn how to use this library: 
* [API Documentation](https://visualstudioex3.github.io/DIV2.Format.Exporter/api/DIV2.Format.Exporter.html)
* [Samples](https://visualstudioex3.github.io/DIV2.Format.Exporter/samples/intro.html)

# Features
* Multiplatform: Windows, Linux and Mac support.
* Image formats supported: JPEG, PNG, BMP, GIF, TGA and 8 bit PCX.
* PAL files:
  * Import and export PAL files.
  * Full access to all features: color palette and color ranges.
  * Create PAL files from images.
  * Extract PAL from MAP and FPG files.
  * Optional color sorting.
  * Allow conversions between DAC [0..63] and RGB formats [0..255].
* MAP files:
  * Import and export MAP files.
  * Import images as MAP files.
  * Import MAP files for palette conversion.
  * Full features for editing: color palette, graph id, description, control points and full read/write access to bitmap data.
  * Export as full RGB bitmap array to allow render MAP file in modern systems.
* FPG files:
  * Import and export FPG files.
  * Full access to all features: color palette, full access to MAP list and their MAP metadata.
  * Automatic MAP list sorting using graph id value.
  * Import images as MAP files, with palette conversion if is needed.
* Compatible with Unity editor (see the [Unity Editor dependencies](#unity-editor-dependencies)).
  
# TODO
- Update project to .NET 7.
- Prepare repository to be able imported from Unity editor as Git package with all required dependencies.
- Implement support for FNT files.

# Unity Editor dependencies
**Unity** .NET backend supports ***.NET Standard 2.0*** but not the full subset of libraries. In order to uses **DIV2.Format.Exporter** library in **Unity Editor** you need to add this libraries to ***Unity Assets folder*** in your project.

This is the list of dependencies used and their source links from **NuGet** repositories:
- [![Nuget](https://img.shields.io/nuget/v/System.Buffers?logo=nuget&label=NuGet:%20System.Buffers)](https://www.nuget.org/packages/System.Buffers/)
- [![Nuget](https://img.shields.io/nuget/v/System.Memory?logo=nuget&label=NuGet:%20System.Memory)](https://www.nuget.org/packages/System.Memory/)
- [![Nuget](https://img.shields.io/nuget/v/System.Numerics.Vectors?logo=nuget&label=NuGet:%20System.Numerics.Vectors)](https://www.nuget.org/packages/System.Numerics.Vectors/)
- [![Nuget](https://img.shields.io/nuget/v/System.Runtime.CompilerServices.Unsafe?logo=nuget&label=NuGet:%20System.Runtime.CompilerServices.Unsafe)](https://www.nuget.org/packages/System.Runtime.CompilerServices.Unsafe/)

> **Note**
> You can extract the DLL files from NuGet packages opening it as Zip files. The DLL files are located in the `lib` folder.

# What is DIV Games Studio?

###### Wikipedia page: https://es.wikipedia.org/wiki/DIV_Games_Studio

Maybe was one of the first game engines for the public. **DIV Games Studio** is a complete solution to develop games for **MS-DOS** and published in 1997 (DIV1) and 1998 (DIV2). 

Is a full windows graphic environment with tools for creation and editing 2D graphics (with a complete drawing suit), particle FX, character animations, font character sets, sounds effects and a complete language programming with a syntax between **Pascal** and **C**, including an integrated debugger and a full complete documentation with a lot of tutorials and samples. 

This engine allow to develop common 2D games with a full of advanced graphic features, and pseudo 3D games using the [Mode7](https://en.wikipedia.org/wiki/Mode_7) and later, with DIV2, the Mode8 (3D feature like the original Doom).

**DIV Games Studio** was very popular at the end of ninetys and early 2000. Was the start point of an entire generation of game developers of nowdays. During the years, the community was develop a multiple forks like [Fenix Project](https://web.archive.org/web/20071012230137/http://fenix.divsite.net/) (with multiple flavours), [eDivc](https://github.com/vroman/edivc), CDiv, [Div GO](https://www.divgo.net/), [Gemix Studio](http://www.gemixstudio.com/), [Bennu GD](https://www.bennugd.org/), or [PixTudio](https://pixtudio.org/).

Currently exists 2 projects to bring it to live again:
* [Div DX / DIV Games Studio 3](https://github.com/DIVGAMES/DIV-Games-Studio) - A port of DIV Games Studio 2 to modern systems (running on Windows, Linux and Mac natively) but keeping the all original features of DIV Games Studio 2. One of the interested features, including the fix of most of the existing bugs on original DIV2, is the posibility of export the games natively to multiple systems, including Android, HTML5 and some consoles. This project has still in beta and seems to be abandoned since 2016.
* [DIV Games Studio 2.02](https://github.com/vii1/DIV) - A reconstruction and fixing of the original DIV Games Studio 2 (v 2.01) for MS-DOS. This is an active project today where the developers want to fix the multiple bugs in the language programming and engine, improve the tools, and, maybe in a future, create a version for Amiga OS.

**DIV Games Studio** if fully functional on [DOSBox](https://www.dosbox.com/). You can download **DIV Games Studio 2** ISO from [Archive.org](https://archive.org/details/div-games-studio-2) as abandoneware.

![DIV Games Studio 2 screenshots](https://github.com/VisualStudioEX3/VisualStudioEX3/blob/develop/Shared/Images/div_games_studio/div2_screen_mosaic.png)
