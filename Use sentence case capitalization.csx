// This script convert the first letter of tags value to uppercase ("the artist" -> "The artist")

using System;
using System.Linq;
using Metatogger.Data;

 // enter tags name to process => eg. { "TAGNAME1", "TAGNAME2", TagName.TrackNumber }
 // leave the array empty to process all tags => { }
string[] tagsToProcess = {  };

string UppercaseFirst(string tagValue)
{
	if (tagValue.Length == 0)
		return tagValue;

	char[] tagChar = tagValue.ToCharArray();
	tagChar[0] = Char.ToUpper(tagChar[0]);
	return new string(tagChar);
}

foreach (var file in files)
	foreach (var tag in file.GetAllTags().Where(kvp => tagsToProcess.Length == 0 || tagsToProcess.Contains(kvp.Key)))
		foreach (string tagValue in tag.Value)
			file.SetTagValue(tag.Key, tagValue, UppercaseFirst(tagValue));