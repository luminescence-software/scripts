// This script removes all leading and trailing white-space characters from the tag value (" The Artist   " -> "The Artist")

string NewValue(string oldValue) => oldValue.Trim();

foreach	(var file in files)
	foreach (var tag in file.GetAllTags())
		foreach (string tagValue in tag.Value)
			file.SetTagValue(tag.Key, tagValue, NewValue(tagValue));