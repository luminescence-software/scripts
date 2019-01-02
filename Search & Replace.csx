// This script replaces all occurrences of a specified string in tags value with another specified string

using System.Linq;
using Metatogger.Data;

// enter what you want to search in changeThis (cannot be left empty)
string changeThis = "";

// enter here what you want to enter (empty string or null will simply remove changeThis)
string toThat = "";

 // enter tags name to process => eg. { "TAGNAME1", "TAGNAME2", TagName.TrackNumber }
 // leave the array empty to process all tags => { }
string[] tagsToProcess = {  };

foreach (var file in files)
	foreach (var tag in file.GetAllTags().Where(kvp => tagsToProcess.Length == 0 || tagsToProcess.Contains(kvp.Key)))
		foreach (string tagValue in tag.Value)
			file.SetTagValue(tag.Key, tagValue, tagValue.Replace(changeThis, toThat));