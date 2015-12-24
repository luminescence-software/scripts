// This script changes all tag values to lowercase ("The Artist" -> "the artist")

string NewValue(string oldValue) => oldValue.ToLower();

foreach	(var file in files)
	foreach (var tag in file.GetAllTags())
		foreach (string tagValue in tag.Value)
			file.SetTagValue(tag.Key, tagValue, NewValue(tagValue));