# Algae.Canvas, a nifty vector graphics API for .NET
* API similar to HTML5 canvas
* Geometry only needs to be created once; later can be rendered at any size
* Retained path creation
* Multithreaded renderer
* Support for rendering single paths, groups, and clipping between paths and
  groups

# Building
Algae.Canvas depends on a light weight renderer built around OpenGL 3, among
others.

You'll need:

* Algae (included)
* [OpenTK](http://www.opentk.com/)
* [SharpFont](https://github.com/Robmaister/SharpFont)
* [Triangle.NET](https://triangle.codeplex.com/) (the latest source version is
  required)
* [SVG](https://github.com/vvvv/SVG) (if you are building Algae.Svg)

Note that SharpFont seems to still require some modifications to the vanilla
source, see: https://github.com/Robmaister/SharpFont/issues/45

OpenTK also seems to refuse to work with a context created by Allegro on any
platform I've tried. This requires a patch to the vanilla source as well.
However, I don't have a diff available for this yet.

There's also two native dependencies:

* [Allegro 5.1](http://alleg.sourceforge.net/)
* [FreeType](http://www.freetype.org/)

Once the dependencies are rounded up, simple invoke Premake5.

# Content
To run the example program, you'll need some content. The basic package,
including a default image, a font, and the shader can be extracted from the
prebuilt binary: http://aaronbolyard.com/file_download/1/algaecanvas-201503019.zip

The included program, Algae.Svg, can parse an SVG and spit out a file that can
be used with Algae.Test. Simply run Algae.Test with a different 'LVG' file as
the first argument to try other images.

# License
The source for Algae.Canvas is released under the MIT license (view LICENSE for
more information).

# Credits
* This wouldn't have been possible without Charles Loop and Jim Blinn's paper
  "Resolution Independent Curve Rendering using Programmable Graphics Hardware".
* [A helpful Allegro.cc member](https://www.allegro.cc/forums/thread/614707/1005958#target)