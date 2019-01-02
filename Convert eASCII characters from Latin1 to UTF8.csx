// This script assumes that tags containing Extended ASCII characters
// are poorly stored as Latin1 and converts their encoding to UTF-8.

using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Metatogger.Data;

 // enter tags name to process => eg. { "TAGNAME1", "TAGNAME2", TagName.TrackNumber }
 // leave the array empty to process all tags => { }
string[] tagsToProcess = {  };

IEnumerable<char> NonASCII(IEnumerable<char> chars) => chars.Where(c => c > 127);
IEnumerable<char> NonEASCII(IEnumerable<char> chars) => chars.Where(c => c > 255);

string ConvertToUTF8(string oldValue)
{
	Encoding srcEncoding = Encoding.Default; 
	// if your tags have not been encoded with your system's default codepage,
	// you have to set the codepage that has been used, 
	// e.g. for Cyrillic character encoding:
	// srcEncoding = Encoding.GetEncoding(1251);

	byte[] oData = Encoding.GetEncoding("iso-8859-1").GetBytes(oldValue);
	byte[] nData = Encoding.Convert(srcEncoding, Encoding.UTF8, oData);
	return Encoding.UTF8.GetString(nData);
}

foreach	(var file in files)
	foreach (var tag in file.GetAllTags().Where(kvp => tagsToProcess.Length == 0 || tagsToProcess.Contains(kvp.Key)))
		foreach (string tagValue in tag.Value)
		{
			var na = NonASCII(tagValue);
			if(na.Any() && !NonEASCII(na).Any())
				file.SetTagValue(tag.Key, tagValue, ConvertToUTF8(tagValue));
		}