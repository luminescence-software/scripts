// Describe here what the script does

//#load ".intellisense.csx" // you can temporarily uncomment this line under Visual Studio Code to get IntelliSense

string NewValue(string oldValue)
{
	return oldValue; // change here to return a custom tag value
}

foreach (var file in files)
	foreach (var tag in file.GetAllTags())
		foreach (string tagValue in tag.Value)
			file.SetTagValue(tag.Key, tagValue, NewValue(tagValue));


// The "files" variable contains the collection of audio files checked in Metatogger
// The AudioFile.GetAllTags() method returns all tags of an audio file in a Dictionary<string, List<string>> variable
// The AudioFile.SetTagValue() method replaces the value of a tag by a new value (returned by the NewValue() function)