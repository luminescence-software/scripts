// This script convert the first letter of all words in tag value to uppercase ("the artist" -> "The Artist")

using System;
using System.Text;

string NewValue(string oldValue)
{
	var sb = new StringBuilder(oldValue);
	if (sb.Length > 0) sb[0] = Char.ToUpper(sb[0]);
	
	for (int i = 1; i < sb.Length; i++)
	{
		if (Char.IsWhiteSpace(sb[i]) || sb[i] == '-')
		{
			if (sb[i+1] == '(') i++;
			i++;
			if (i >= sb.Length) break;
			sb[i] = Char.ToUpper(sb[i]);
			i--;
		}
	}
	
	return sb.ToString();
}

foreach	(var file in files)
	foreach (var tag in file.GetAllTags())
		foreach (string tagValue in tag.Value)
			file.SetTagValue(tag.Key, tagValue, NewValue(tagValue));