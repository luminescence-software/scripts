// This script removes all tags that contains specified value

string NewValue(string oldValue)
{
	string tagValueToRemove = ""; // enter here the tag value that you want to remove
	return oldValue != tagValueToRemove ? oldValue : null;
}

foreach	(var file in files)
	foreach (var tag in file.GetAllTags())
		foreach (string tagValue in tag.Value)
			file.SetTagValue(tag.Key, tagValue, NewValue(tagValue));