// Describe here what the script does

string NewValue(string oldValue)
{
	return oldValue; // change here to return a custom tag value
}

foreach (var file in files)
	foreach (var tag in file.GetAllTags())
		foreach (string tagValue in tag.Value)
			file.SetTagValue(tag.Key, tagValue, NewValue(tagValue));

/*
"files" is the collection of audio files loaded in Metatogger
GetAllTags() returns all tags of an audio file in a Dictionary<string, List<string>> variable
SetTagValue() replace the value of a tag by a new value (returned by the NewValue() function)
*/