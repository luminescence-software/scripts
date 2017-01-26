// This script convert the first letter of tag value to uppercase ("the artist" -> "The artist")

using System;
using System.Text;

string NewValue(string oldValue)
{
	var sb = new StringBuilder(oldValue);
	if (sb.Length > 0) sb[0] = Char.ToUpper(sb[0]);
	return sb.ToString();
}

foreach (var file in files)
	foreach (var tag in file.GetAllTags())
		foreach (string tagValue in tag.Value)
			file.SetTagValue(tag.Key, tagValue, NewValue(tagValue));