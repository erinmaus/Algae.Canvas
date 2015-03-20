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
	links { "System.dll", "System.XML.dll", "System.XML.Linq.dll", "OpenTK.dll" }

	filter "system:windows"
		defines { "WINDOWS" }

project "Algae.Canvas"
	language "C#"
	kind "SharedLib"
	files { "Source/Algae.Canvas/**.cs" }
	links { "System.dll", "System.XML.dll", "System.XML.Linq.dll", "Triangle.dll", "SharpFont" }
	dependson { "Algae" }

project "Algae.Test"
	language "C#"
	kind "WindowedApp"
	files { "Source/Algae.Test/**.cs" }
	links { "System.dll", "System.XML.dll", "System.XML.Linq.dll", "Triangle.dll", "SharpFont" }
	dependson { "Algae", "Algae.Canvas" }