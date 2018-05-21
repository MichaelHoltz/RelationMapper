# RelationMapper
RelationMapper
**RelationMapper** is a .Net Windows Forms tool for viewing relationships using Microsoft Automatic Graph Layout

(Don't know the complete syntax for .md files at this time so this will be ugly for a while)


## Source Dependencies - for Debugging
[Automatic Graph Layout Source](https://github.com/Microsoft/automatic-graph-layout.git)
Modified version of - [The Movie Database Wrapper](https://github.com/Fishes/TMDbWrapper.git)
-If I knew how to fork a repository I would consider it for TMDbWrapper as I needed to make changes to get it to work in my environment as well as some out of date enumerations.

## NuGet Packages for normal use
**The Core Layout engine (Microsoft.MSAGL.dll)** - [NuGet package](https://www.nuget.org/packages/Microsoft.Msagl/)
**The Drawing module (Microsoft.MSAGL.Drawing.dll)** - [NuGet package](https://www.nuget.org/packages/Microsoft.Msagl.Drawing/)
**A Viewer control (Microsoft.MSAGL.GraphViewerGDIGraph.dll)** - [NuGet package](https://www.nuget.org/packages/Microsoft.Msagl.GraphViewerGDI/)
Newtonsoft.JSON.11.0.2 +
System.Net.Http.4.3.1 +

```chsarp
//Code Example
```

## Using RelationMapper
* open RelationMapSolution.sln and build the solution.
* Get API KEys from TMDb (and possibly GoogleAPI)