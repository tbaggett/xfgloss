#addin nuget:?package=Cake.Android.SdkManager

var TARGET = Argument ("target", Argument ("t", "Default"));
var VERSION = EnvironmentVariable ("APPVEYOR_BUILD_VERSION") ?? Argument("version", "0.0.9999");

var ANDROID_HOME = EnvironmentVariable ("ANDROID_HOME") ?? Argument ("android_home", "");

Task("Libraries").Does(()=>
{
	NuGetRestore ("./XFGloss.sln");
	MSBuild ("./XFGloss/XFGloss.csproj", c => {
		c.Configuration = "Release";
		c.MSBuildPlatform = Cake.Common.Tools.MSBuild.MSBuildPlatform.x86;
	});
	MSBuild ("./XFGloss.Droid/XFGloss.Droid.csproj", c => {
		c.Configuration = "Release";
		c.MSBuildPlatform = Cake.Common.Tools.MSBuild.MSBuildPlatform.x86;
	});
	MSBuild ("./XFGloss.iOS/XFGloss.iOS.csproj", c => {
		c.Configuration = "Release";
		c.MSBuildPlatform = Cake.Common.Tools.MSBuild.MSBuildPlatform.x86;
	});
});

Task ("NuGet")
	.IsDependentOn ("AndroidSDK")
	.IsDependentOn ("Libraries")
	.Does (() =>
{
    if(!DirectoryExists("./Build/nuget/"))
        CreateDirectory("./Build/nuget");
        
	NuGetPack ("./nuget/XFGloss.nuspec", new NuGetPackSettings { 
		Version = VERSION,
		Verbosity = NuGetVerbosity.Detailed,
		OutputDirectory = "./Build/nuget/",
		BasePath = "./"
	});	
});

Task ("AndroidSDK")
	.Does (() =>
{
	Information ("ANDROID_HOME: {0}", ANDROID_HOME);

	var androidSdkSettings = new AndroidSdkManagerToolSettings { 
		SdkRoot = ANDROID_HOME,
		SkipVersionCheck = true
	};

	try { AcceptLicenses (androidSdkSettings); } catch { }

	AndroidSdkManagerInstall (new [] { 
			"platforms;android-15",
			"platforms;android-23",
			"platforms;android-25",
			"platforms;android-26"
		}, androidSdkSettings);
});

//Build the component, which build samples, nugets, and libraries
Task ("Default").IsDependentOn("NuGet");

Task ("Clean").Does (() => 
{
	CleanDirectory ("./component/tools/");
	CleanDirectories ("./Build/");
	CleanDirectories ("./**/bin");
	CleanDirectories ("./**/obj");
});

RunTarget (TARGET);