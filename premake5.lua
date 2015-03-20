solution "Algae.Canvas"
	configurations { "Debug", "Release" }
	platforms { "x86" }
	architecture "x32"
	location "Solution"

	filter "configurations:Debug"
		targetdir "Bin/Debug"

	filter "configurations:Release"
		targetdir "Bin/Release"

project "Algae"
	language "C#"
	kind "SharedLib"
	files { "Source/Algae/**.cs" }
	links { "System.dll", "System.Xml.dll", "System.Xml.Linq.dll", "OpenTK.dll" }

	if os.get() == "windows"
		defines { "WINDOWS" }

project "Algae.Canvas"
	language "C#"
	kind "SharedLib"
	files { "Source/Algae.Canvas/**.cs" }
	links { "System.dll", "System.Xml.dll", "System.Xml.Linq.dll", "Triangle.dll", "SharpFont" }
	dependson { "Algae" }

project "Algae.Test"
	language "C#"
	kind "WindowedApp"
	files { "Source/Algae.Test/**.cs" }
	links { "System.dll" }
	dependson { "Algae", "Algae.Canvas" }

project "Algae.Svg"
	language "C#"
	kind "ConsoleApp"
	files { "Source/Algae.Svg/**.cs" }
	links { "System.dll", "System.Xml.dll", "System.Xml.Linq.dll", "Svg.dll" }
	dependson { "Algae", "Algae.Canvas" }
