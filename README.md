# Scripts repository for Metatogger

Scripts for Metatogger are written is C# and executed by Roslyn.

The coding experience is much better under Visual Studio Code (code completion, refactoring,...).

Here is a basic script template:

```cs
// Describe here what the script does

using System.Linq;
using Metatogger.Data;

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
```

Feel free to submit improvement or new script with "pull request".