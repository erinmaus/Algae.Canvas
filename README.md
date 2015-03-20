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
* [Triangle.NET](https://triangle.codeplex.com/)

There's also two native dependencies:

* [Allegro 5.1](http://alleg.sourceforge.net/)
* [FreeType](http://www.freetype.org/)

Once the dependencies are rounded up, simple invoke Premake5.

# Content
To run the example program, you'll need some content. A link will be included
soon.

# License
The source for Algae.Canvas is released under the MIT license (view LICENSE for
more information).

# Credits
* This wouldn't have been possible without Charles Loop and Jim Blinn's paper
  "Resolution Independent Curve Rendering using Programmable Graphics Hardware".
* [A helpful Allegro.cc member](https://www.allegro.cc/forums/thread/614707/1005958#target)