#addin nuget:?package=Cake.Xamarin.Build&version=4.0.1
#addin nuget:?package=Cake.FileHelpers&version=3.0.0
#addin nuget:?package=Cake.Yaml&version=2.1.0
#addin nuget:?package=Cake.Json&version=3.0.1
#addin nuget:?package=Cake.XCode&version=4.0.0
#addin nuget:?package=Newtonsoft.Json&version=9.0.1
#addin nuget:?package=YamlDotNet&version=4.2.1
#addin nuget:?package=Xamarin.Nuget.Validator&version=1.1.1


var TARGET = Argument ("target", Argument ("t", Argument ("Target", "build")));

var GIT_PREVIOUS_COMMIT = EnvironmentVariable ("GIT_PREVIOUS_SUCCESSFUL_COMMIT") ?? Argument ("gitpreviouscommit", "");
var GIT_COMMIT = EnvironmentVariable ("GIT_COMMIT") ?? EnvironmentVariable ("GIT_CLONE_COMMIT_HASH") ?? Argument("gitcommit", "");
var GIT_BRANCH = EnvironmentVariable ("GIT_BRANCH") ?? EnvironmentVariable ("BITRISE_GIT_BRANCH") ?? "origin/master";
var GIT_PATH = EnvironmentVariable ("GIT_EXE") ?? (IsRunningOnWindows () ? "C:\\Program Files (x86)\\Git\\bin\\git.exe" : "git");

var BUILD_GROUPS = DeserializeYamlFromFile<List<BuildGroup>> ("./manifest.yaml");
var BUILD_NAMES = Argument ("names", Argument ("name", Argument ("n", "")))
	.Split (new [] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
var BUILD_TARGETS = Argument ("targets", Argument ("build-targets", Argument ("build-targets", Argument ("build", ""))))
	.Split (new [] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);


var FORCE_BUILD = Argument ("force", Argument ("forcebuild", Argument ("force-build", "false"))).ToLower ().Equals ("true");

var POD_REPO_UPDATE = Argument ("update", Argument ("repo-update", Argument ("pod-repo-update", false)));

// Print out environment variables to console
var ENV_VARS = EnvironmentVariables ();
Information ("Environment Variables: {0}", "");
foreach (var ev in ENV_VARS)
	Information ("\t{0} = {1}", ev.Key, ev.Value);

CakeStealer.CakeContext = Context;

// From Cake.Xamarin.Build, dumps out versions of things
LogSystemInfo ();

// Print out git commit info
Information ("Git Path: {0}", GIT_PATH);
Information ("Git Previous Commit: {0}", GIT_PREVIOUS_COMMIT);
Information ("Git Commit: {0}", GIT_COMMIT);
Information ("Force Build: {0}", FORCE_BUILD);

public enum PodRepoUpdate {
	NotRequired,
	Required,
	Forced
}

public class CakeStealer
{
	static public ICakeContext CakeContext { get; set; }
}

FilePath GetCakeToolPath ()
{
	var possibleExe = GetFiles ("./**/tools/Cake/Cake.exe").FirstOrDefault ();

	if (possibleExe != null)
		return possibleExe;
		
	var p = System.Diagnostics.Process.GetCurrentProcess ();	
	return new FilePath (p.Modules[0].FileName);
}

void BuildSingleGroup (BuildGroup buildGroup)
{
	foreach (var target in buildGroup.BuildTargets) {
		// TODO: Actually build this thing
		var cakeSettings = new CakeSettings { 
				ToolPath = GetCakeToolPath (),
				Arguments = new Dictionary<string, string> { { "target", target } },
				Verbosity = Verbosity.Diagnostic
			};

		// Run the script from the subfolder
		CakeExecuteScript (buildGroup.BuildScript, cakeSettings);
	}
}

IEnumerable<string> ExecuteProcess (string file, string args)
{
	IEnumerable<string> stdout;
	StartProcess (file, new ProcessSettings { 
		Arguments = args,
		RedirectStandardOutput = true }, 
		out stdout);

	return stdout;
}

void BuildGroups (List<BuildGroup> buildGroups, List<string> names, List<string> buildTargets, string gitPath, string gitBranch, string gitPreviousCommit, string gitCommit, bool forceBuild, PodRepoUpdate podRepoUpdate)
{
	bool runningOnMac = IsRunningOnUnix ();
	bool runningOnWin = IsRunningOnWindows ();

	var groupsToBuild = new List<BuildGroup> ();

	if (!forceBuild) {
		// If neither commit hash was found, we can't detect changes
		if (string.IsNullOrWhiteSpace (gitPreviousCommit) && string.IsNullOrWhiteSpace (gitCommit))
			throw new CakeException ("GIT Commit/Previous Commit hashes were invalid, can't find affected files");
		
		// Get all the changed files in this commit
		IEnumerable<string> changedFiles = new List<string> ();

		if (!string.IsNullOrWhiteSpace (gitPreviousCommit)) {
			// We have both commit hashes (previous and current) so do a diff on them
			changedFiles = ExecuteProcess (gitPath, "--no-pager diff --name-only " + gitPreviousCommit + " " + gitCommit);

			// // TODO: This should be fixed in cake 0.7.0, right now it may hang on output
			// StartProcess (gitPath, new ProcessSettings { 
			// 		Arguments = "--no-pager diff --name-only " + gitPreviousCommit + " " + gitCommit,
			// 		RedirectStandardOutput = true,
			// 	}, out changedFiles);
		} else {
			// We only have the current commit hash, so list files for this commit only
			changedFiles = ExecuteProcess (gitPath, "--no-pager show --pretty=\"format:\" --name-only " + gitCommit);

			// // TODO: This should be fixed in cake 0.7.0, right now it may hang on output
			// StartProcess (gitPath, new ProcessSettings { 
			// 		Arguments = "--no-pager show --pretty=\"format:\" --name-only " + gitCommit,
			// 		RedirectStandardOutput = true,
			// 	}, out changedFiles);
		}

		Information ("Changed Files:");
		foreach (var file in changedFiles) {
			Information ("\t{0}", file);

			if (podRepoUpdate == PodRepoUpdate.NotRequired && file.EndsWith ("Podfile"))
				podRepoUpdate = PodRepoUpdate.Required;

			foreach (var buildGroup in buildGroups) {
				// If ignore triggers for the platform this is running on, do not add the group even if the trigger is matched
				if ((buildGroup.IgnoreTriggersOnMac && runningOnMac) || (buildGroup.IgnoreTriggersOnWindows && runningOnWin))
					continue;
				
				foreach (var triggerPath in buildGroup.TriggerPaths) {
					if (file.StartsWith (triggerPath.ToString ())) {
						Information ("\t\tMatched: {0}", triggerPath);
						if (!groupsToBuild.Contains (buildGroup))
							groupsToBuild.Add (buildGroup);
						break;
					}
				}	
				foreach (var triggerFile in buildGroup.TriggerFiles) {
					if (file.Equals (triggerFile.ToString ())) {
						Information ("\t\tMatched: {0}", triggerFile);
						if (!groupsToBuild.Contains (buildGroup))
							groupsToBuild.Add (buildGroup);
						break;
					}
				}			
			}
		}
	} else {
		podRepoUpdate = PodRepoUpdate.Forced;
		Information ("Groups To Build: {0}", string.Join (", ", buildGroups));
		groupsToBuild.AddRange (buildGroups);
	}

	// If a filter was specified use it
	if (names != null && names.Any (n => !string.IsNullOrWhiteSpace (n))) {
		Information ("Only building groups: {0}", string.Join (",", names));
		groupsToBuild = groupsToBuild.Where (bg => names.Any (n => n.ToLower ().Trim () == bg.Name.ToLower ().Trim ())).ToList ();
	}
	
	if (groupsToBuild.Any ()) {

		// Logging about the jobs and what platforms they can run on
		Information ("Running On: Mac? {0}  Win? {1}", runningOnMac, runningOnWin);
		foreach (var gtb in groupsToBuild)
			Information ("{0} Build on: Mac? {1}  Win? {2}", gtb.Name, gtb.BuildOnMac, gtb.BuildOnWindows);

		groupsToBuild = groupsToBuild.Where (bg => (bg.BuildOnMac && runningOnMac) || (bg.BuildOnWindows && runningOnWin)).ToList ();

		
		// Replace build targets with custom specified build targets if any were specified
		if (buildTargets != null && buildTargets.Any (bt => !string.IsNullOrWhiteSpace (bt))) {
			foreach (var buildGroup in groupsToBuild) {
				buildGroup.BuildTargets.Clear ();
				buildGroup.BuildTargets.AddRange (buildTargets);				
			}	
		}

		if (podRepoUpdate != PodRepoUpdate.NotRequired) {
			string message = string.Empty;
			if (podRepoUpdate == PodRepoUpdate.Forced)
				message = "Forcing Cocoapods repo update...";
			else
				message = "A modified Podfile was found...";

			Information ("////////////////////////////////////////");
			Information ("// Pods Repo Update Started           //");
			Information ("////////////////////////////////////////");
			
			Information ($"{message}\nUpdating Cocoapods repo...");
			CocoaPodRepoUpdate ();

			Information ("////////////////////////////////////////");
			Information ("// Pods Repo Update Ended             //");
			Information ("////////////////////////////////////////");
		}
		
		if (!DirectoryExists ("./output/"))
			CreateDirectory ("./output/");
		
		// Write out some build information
		SerializeJsonToFile ("./output/buildinfo.json", new BuildInfo { 
			BuiltGroups = groupsToBuild,
			ManifestGroups = buildGroups
		});

		var groupsNameList = string.Join (", ", from gtb in groupsToBuild select gtb.Name);
		if (groupsNameList.Length > 100)
			groupsNameList = groupsNameList.Substring (0, 97) + "...";

		var branchInfo = "";
		if (gitBranch != "origin/master")
			branchInfo = " (" + gitBranch + ")";

		Information ("[BUILD_DESC]{0}{1}[/BUILD_DESC]", groupsNameList, branchInfo);

		// Information ("Updating Cocoapods Spec repo before building...");
		// CocoaPodRepoUpdate();

		foreach (var buildGroup in groupsToBuild) {
			Information ("Building {0} with Targets {1}", buildGroup.Name, string.Join (",", buildGroup.BuildTargets));

			BuildSingleGroup (buildGroup);
		}

	} else {
		Information ("No changed files affected any of the paths from the manifest.yaml! {0}", "Skipping Builds");
	}

	var artifacts = GetFiles ("./**/output/**/*");

	Information ("Found Artifacts ({0})", artifacts.Count ());
	foreach (var a in artifacts)
		Information ("{0}", a);

	var dlls = GetFiles ("./**/*.dll");

	Information ("Found DLL's ({0})", dlls.Count ());
	foreach (var d in dlls)
		Information ("{0}", d);
}


public class BuildInfo
{
	public List<BuildGroup> ManifestGroups { get; set; }
	public List<BuildGroup> BuiltGroups { get; set; }
}

public class BuildGroup 
{
	public BuildGroup () 
	{
		Name = string.Empty;
		TriggerPaths = new List<string> ();
		TriggerFiles = new List<string> ();
		WindowsBuildTargets = new List<string> ();
		MacBuildTargets = new List<string> ();
		IgnoreTriggersOnMac = false;
		IgnoreTriggersOnWindows = false;
	}

	public string Name { get; set; }
	public string BuildScript { get; set; }
	public List<string> TriggerPaths { get; set; }
	public List<string> TriggerFiles { get; set; }
	public bool IgnoreTriggersOnMac { get; set; }
	public bool IgnoreTriggersOnWindows { get; set; }
	public List<string> WindowsBuildTargets { get; set; }
	public List<string> MacBuildTargets { get; set; }
	
	public bool BuildOnWindows { get { return WindowsBuildTargets != null && WindowsBuildTargets.Any (); } }
	public bool BuildOnMac { get { return MacBuildTargets != null && MacBuildTargets.Any (); } }

	public List<string> BuildTargets {
		get {
			return CakeStealer.CakeContext.IsRunningOnWindows () ? WindowsBuildTargets : MacBuildTargets;
		}
	}

	public override string ToString ()
	{
		return Name;
	}
}

Task ("build").Does (() => 
{
	var buildTargets = new List<string> ();
	if (BUILD_TARGETS != null && BUILD_TARGETS.Any ())
		buildTargets.AddRange (BUILD_TARGETS);

	if (FileExists ("./output/buildinfo.json") && FORCE_BUILD) {
		Information ("Found {0} from Upstream project, overriding build group names...", "./output/buildinfo.json");

		GIT_PREVIOUS_COMMIT = "";
		GIT_COMMIT = "";

		var buildInfo = DeserializeJsonFromFile<BuildInfo> ("./output/buildinfo.json");
		BUILD_NAMES = (from bg in buildInfo.BuiltGroups select bg.Name).ToArray ();
		
		Information ("Overriding build group names to: {0}", string.Join (", ", BUILD_NAMES));
	}

	PodRepoUpdate podRepoUpdate = POD_REPO_UPDATE ? PodRepoUpdate.Forced : PodRepoUpdate.NotRequired;

	BuildGroups (BUILD_GROUPS, BUILD_NAMES.ToList (), buildTargets, GIT_PATH, GIT_BRANCH, GIT_PREVIOUS_COMMIT, GIT_COMMIT, FORCE_BUILD, podRepoUpdate);		
});

Task("nuget-validation")
	.Does(()=>
{
	//setup validation options
	var options = new Xamarin.Nuget.Validator.NugetValidatorOptions()
	{
		Copyright = "© Microsoft Corporation. All rights reserved.",
		Author = "Microsoft",
		Owner = "Microsoft",
		NeedsProjectUrl = true,
		NeedsLicenseUrl = true,
		ValidateRequireLicenseAcceptance = true,
		ValidPackageNamespace = new [] { "Xamarin", "Mono", "SkiaSharp", "HarfBuzzSharp", "mdoc" },
	};

	var nupkgFiles = GetFiles ("./**/output/*.nupkg");

	Information ("Found ({0}) Nuget's to validate", nupkgFiles.Count ());

	foreach (var nupkgFile in nupkgFiles)
	{
		Information ("Verifiying Metadata of {0}", nupkgFile.GetFilename ());

		var result = Xamarin.Nuget.Validator.NugetValidator.Validate(MakeAbsolute(nupkgFile).FullPath, options);

		if (!result.Success)
		{
			Information ("Metadata validation failed for: {0} \n\n", nupkgFile.GetFilename ());
			Information (string.Join("\n    ", result.ErrorMessages));
			throw new Exception ($"Invalid Metadata for: {nupkgFile.GetFilename ()}");

		}
		else
		{
			Information ("Metadata validation passed for: {0}", nupkgFile.GetFilename ());
		}
			
	}

});

RunTarget (TARGET);
