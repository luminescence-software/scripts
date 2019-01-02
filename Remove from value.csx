// This script removes tags that contains specified value

using System.Linq;
using Metatogger.Data;

// enter here the tag value that you want to remove
string tagValueToRemove = "";

 // enter tags name to process => eg. { "TAGNAME1", "TAGNAME2", TagName.TrackNumber }
 // leave the array empty to process all tags => { }
string[] tagsToProcess = {  };

foreach (var file in files)
	foreach (var tag in file.GetAllTags().Where(kvp => tagsToProcess.Length == 0 || tagsToProcess.Contains(kvp.Key)))
		foreach (string tagValue in tag.Value)
			if (tagValue == tagValueToRemove)
				file.SetTagValue(tag.Key, tagValue, null);