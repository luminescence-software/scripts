// This script replaces all occurrences of a specified string in a tag value with another specified string

string NewValue(string oldValue)
{
	string changeThis = ""; // enter here what you want to search
	string toThat = "";     // enter here what you want to enter	
	return oldValue.Replace(changeThis, toThat);
}

foreach	(var file in files)
	foreach (var tag in file.GetAllTags())
		foreach (string tagValue in tag.Value)
			file.SetTagValue(tag.Key, tagValue, NewValue(tagValue));