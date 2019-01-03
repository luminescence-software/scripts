// Describe here what the script does

using System.Linq;
using Metatogger.Data;

// Uncomment the following line to get better coding experience with VS Code, but DO NOT forget to comment it before running the script into host
//System.Collections.Generic.IEnumerable<Metatogger.Data.AudioFile> files = new System.Collections.Generic.List<Metatogger.Data.AudioFile>(); // list of checked audio files loaded in the workspace

 // enter tags name to process => eg. { "TAGNAME1", "TAGNAME2", TagName.TrackNumber }
 // leave the array empty to process all tags => { }
string[] tagsToProcess = {  };

string NewValue(string oldValue)
{
	return oldValue; // change here to return a custom tag value
}

foreach (var file in files)
	foreach (var tag in file.GetAllTags().Where(kvp => tagsToProcess.Length == 0 || tagsToProcess.Contains(kvp.Key)))
		foreach (string tagValue in tag.Value)
			file.SetTagValue(tag.Key, tagValue, NewValue(tagValue));


// The "files" variable contains the collection of audio files checked in Metatogger
// Dictionary<string, List<string>> AudioFile.GetAllTags() => returns all tags of an audio file
// List<string> AudioFile.GetValues(string tag) => returns all values of a tag
// string AudioFile.GetFirstValue(string tag) => returns first tag value for a tag
// AudioFile.SetTag(string tag, string value, bool overwrite = true, bool addIfExists = false) => add, overwrite or remove tag values (if value = null)
// AudioFile.SetTagValue(string tag, string oldValue, string newValue) => replaces the value of a tag by a new value