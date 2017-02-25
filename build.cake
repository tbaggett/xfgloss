#addin "Cake.FileHelpers"

var TARGET = Argument ("target", Argument ("t", "Default"));

var version = EnvironmentVariable ("APPVEYOR_BUILD_VERSION") ?? Argument("version", "0.0.9999");

Task ("Default").Does (() =>
{
	NuGetRestore ("./XFGloss.sln");

//	DotNetBuild ("./XFGloss.sln", c => c.Configuration = "Release");
	DotNetBuild ("./XFGloss/XFGloss.csproj", c => c.Configuration = "Release");
	DotNetBuild ("./XFGloss.Droid/XFGloss.Droid.csproj", c => c.Configuration = "Release");
	DotNetBuild ("./XFGloss.iOS/XFGloss.iOS.csproj", c => c.Configuration = "Release");
});

Task ("NuGetPack")
	.IsDependentOn ("Default")
	.Does (() =>
{
	NuGetPack ("./XFGloss.nuspec", new NuGetPackSettings { 
		Version = version,
		Verbosity = NuGetVerbosity.Detailed,
		OutputDirectory = "./",
		BasePath = "./",
	});	
});


RunTarget (TARGET);
