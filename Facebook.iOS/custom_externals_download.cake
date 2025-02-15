void DownloadAccountKit (Artifact artifact)
{
	CopyDirectory (string.Format ("./externals/{0}/Pods/{0}/{0}Strings.bundle", "AccountKit"), string.Format ("./externals/{0}Strings.bundle", "AccountKit"));
}

void DownloadAudienceNetwork (Artifact artifact)
{
	var id = artifact.Id;
	var url = $"https://origincache.facebook.com/developers/resources/?id={id}-{AUDIENCE_NETWORK_ARTIFACT.Version}.zip";
	var basePath = $"./externals/{id}";
	DownloadFile (url, $"{basePath}.zip", new Cake.Xamarin.Build.DownloadFileSettings { UserAgent = "curl/7.43.0" });
	Unzip ($"{basePath}.zip", $"{basePath}");
	CopyDirectory ($"{basePath}/Dynamic/{id}.framework", $"./externals/{id}.framework");
}
